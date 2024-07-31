using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using KzA.DNS.ResourceRecord;
using KzA.DNS.Zone;
using Xunit.Abstractions;

namespace KzA.DNS.Test;

public class UnitTest1(ITestOutputHelper output)
{
    private readonly ITestOutputHelper output = output;

    [Fact]
    public void TestZoneFileGen()
    {
        var zone = new PrimaryZone()
        {
            Name = "contoso.org",
        };
        zone.SOA.MNAME = "ns.contoso.org.";
        zone.SOA.RNAME = "hostmaster.contoso.org.";
        zone.SOA.SERIAL = 240722;
        zone.SOA.REFRESH = 900;
        zone.SOA.RETRY = 600;
        zone.SOA.EXPIRE = 86400;
        zone.SOA.MINIMUM = 3600;

        zone.Records.Add(new NS()
        {
            Name = "@",
            HostName = "ns.contoso.org.",
        });
        zone.Records.Add(new A()
        {
            Name = "ns",
            Data = IPAddress.Parse("10.1.1.1"),
        });
        zone.Records.Add(new AAAA()
        {
            Name = "ns",
            Data = IPAddress.Parse("fd45:1234:abcd:1::1"),
        });
        zone.Records.Add(new A()
        {
            Name = "dc",
            Data = IPAddress.Parse("10.10.1.1"),
        });
        zone.Records.Add(new AAAA()
        {
            Name = "dc",
            Data = IPAddress.Parse("fd45:1234:abcd:10::1"),
        });
        zone.Records.Add(new CNAME()
        {
            Name = "dns",
            HostName = "dc.contoso.org.",
        });
        zone.Records.Add(new SRV()
        {
            Name = "_ldap._tcp",
            Priority = 0,
            Weight = 100,
            Port = 389,
            Target = "dc.contoso.org.",
        });
        zone.Records.Add(new SRV()
        {
            Name = "_ldap._tcp.sites.Mars",
            Priority = 0,
            Weight = 100,
            Port = 389,
            Target = "dc.contoso.org.",
        });
        zone.Records.Add(new A()
        {
            Name = "mail",
            Data = IPAddress.Parse("10.1.2.1"),
            TTL = 60,
        });
        zone.Records.Add(new MX()
        {
            Name = "@",
            Preference = 10,
            Host = "mail.contoso.org.",
        });
        zone.Records.Add(new TXT()
        {
            Name = "@",
            Txt = "v=spf1 ip4:192.0.2.0/24 ip4:198.51.100.123 ip6:2620:0:860::/46 a -all",
        });
        zone.Records.Add(new SVCB()
        {
            Name = "_ssh._tcp",
            Priority = 1,
            Target = "srv.contoso.org.",
            Params =
                {
                    {"port", "12345" },
                    {"alpn", "sshv2" }
                }
        });
        zone.Records.Add(new HTTPS()
        {
            Name = "www",
            Priority = 1,
            Target = "srv.contoso.org.",
            Params =
                {
                    {"port", "54321" },
                    {"alpn", "\"h3,h2\"" },
                    {"ipv4hint", "10.10.10.10,10.20.10.10" },
                }
        });
        var wks = new ArbitraryRecord()
        {
            Name = "host",
            Data = "1.2.3.1 TCP (smtp netstat http https)"
        };
        wks.ModifyType(RRType.WKS);
        zone.Records.Add(wks);
        var hi = new ArbitraryRecord()
        {
            Name = "host",
            Data = "\"Intel Xeon Platium 8272CL\" \"Windows Server 2025 Datacenter\""
        };
        hi.ModifyType(RRType.HINFO);
        zone.Records.Add(hi);

        var zonefile = zone.ToZoneFile(true);
        var outfile = Path.GetFullPath("../../zonefile.dns");  // KzA.DNS.Test/bin/zonefile.dns
        File.WriteAllText(outfile, zonefile);
        if (OperatingSystem.IsWindows())
        {
            var drv = char.ToLower(outfile[0]);
            var wsloutfile = "/mnt/" + drv + outfile[2..].Replace('\\', '/');
            CheckZoneFileWithWslNamed(wsloutfile);
        }
        else
        {
            CheckZoneFileWithNamed(outfile);
        }
    }

    // Validate zonefile with isc-bind9 named-checkzone in WSL
    void CheckZoneFileWithWslNamed(string wslPathToZoneFile)
    {
        var wslprocinfo = new ProcessStartInfo()
        {
            FileName = "wsl.exe",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        wslprocinfo.ArgumentList.Add("named-checkzone");
        wslprocinfo.ArgumentList.Add("contoso.org");
        wslprocinfo.ArgumentList.Add(wslPathToZoneFile);

        var wslproc = Process.Start(wslprocinfo)!;
        wslproc.WaitForExit();
        var wslout = wslproc.StandardOutput.ReadToEnd();
        var wslerr = wslproc.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(wslout))
        {
            output.WriteLine("stdout");
            output.WriteLine(wslout);
        }
        if (!string.IsNullOrEmpty(wslerr))
        {
            output.WriteLine("stderr");
            output.WriteLine(wslerr);
        }
        Assert.True(wslproc.ExitCode == 0);
    }

    // Validate zonefile with isc-bind9 named-checkzone directly
    // Should work with named-checkzone.exe on Windows as well
    void CheckZoneFileWithNamed(string pathToZoneFile)
    {
        var procinfo = new ProcessStartInfo()
        {
            FileName = "named-checkzone",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        procinfo.ArgumentList.Add("contoso.org");
        procinfo.ArgumentList.Add(pathToZoneFile);

        var proc = Process.Start(procinfo)!;
        proc.WaitForExit();
        var stdout = proc.StandardOutput.ReadToEnd();
        var stderr = proc.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(stdout))
        {
            output.WriteLine("stdout");
            output.WriteLine(stdout);
        }
        if (!string.IsNullOrEmpty(stderr))
        {
            output.WriteLine("stderr");
            output.WriteLine(stderr);
        }
        Assert.True(proc.ExitCode == 0);
    }
}