using System.Net;
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
            Data = "ns.contoso.org.",
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
        zone.Records.Add(new SRV()
        {
            Name = "_ldap._tcp",
            Data = new()
            {
                Priority = 0,
                Weight = 100,
                Port = 389,
                Target = "dc.contoso.org.",
            }
        });
        zone.Records.Add(new SRV()
        {
            Name = "_ldap._tcp.sites.Mars",
            Data = new()
            {
                Priority = 0,
                Weight = 100,
                Port = 389,
                Target = "dc.contoso.org.",
            }
        });
        zone.Records.Add(new CNAME()
        {
            Name = "mail",
            Data = "dc.contoso.org.",
            TTL = 60,
        });
        zone.Records.Add(new MX()
        {
            Name = "@",
            Data = new()
            {
                Preference = 10,
                Host = "mail.contoso.org.",
            }
        });
        zone.Records.Add(new TXT()
        {
            Name = "@",
            Txt = "v=spf1 ip4:192.0.2.0/24 ip4:198.51.100.123 ip6:2620:0:860::/46 a -all",
        });

        var zonefile = zone.ToZoneFile();
        File.WriteAllText("../../zonefile.dns", zonefile);
    }
}