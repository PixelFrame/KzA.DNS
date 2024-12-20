﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS
{
    #region Common Params
    //https://www.iana.org/assignments/dns-parameters/dns-parameters.xhtml

    [Flags]
    public enum HeaderFlags
    {
        QR = 0x8000,
        AA = 0x0400,
        TC = 0x0200,
        RD = 0x0100,
        RA = 0x0080,
        Z = 0x0040,
        AD = 0x0020,
        CD = 0x0010,
    }

    public enum OpCode : byte
    {
        Query = 0,
        IQuery = 1,
        Status = 2,
        Notify = 4,
        Update = 5,
        DSO = 6,
    }

    public enum RCODE : byte
    {
        NoError = 0,
        FormErr = 1,
        ServFail = 2,
        NXDomain = 3,
        NotImp = 4,
        Refused = 5,
        YXDomain = 6,
        YXRRSet = 7,
        NXRRSet = 8,
        NotAuth = 9,
        NotZone = 10,
        DSOTYPENI = 11,
        BADSIG = 16,
        BADKEY = 17,
        BADTIME = 18,
        BADMODE = 19,
        BADNAME = 20,
        BADALG = 21,
        BADTRUNC = 22,
        BADCOOKIE = 23,
    }

    public enum RRType : ushort
    {
        Reserved0 = 0,
        A = 1,
        NS = 2,
        MD = 3,
        MF = 4,
        CNAME = 5,
        SOA = 6,
        MB = 7,
        MG = 8,
        MR = 9,
        NULL = 10,
        WKS = 11,
        PTR = 12,
        HINFO = 13,
        MINFO = 14,
        MX = 15,
        TXT = 16,
        RP = 17,
        AFSDB = 18,
        X25 = 19,
        ISDN = 20,
        RT = 21,
        NSAP = 22,
        NSAP_PTR = 23,
        SIG = 24,
        KEY = 25,
        PX = 26,
        GPOS = 27,
        AAAA = 28,
        LOC = 29,
        NXT = 30,
        EID = 31,
        NIMLOC = 32,
        SRV = 33,
        ATMA = 34,
        NAPTR = 35,
        KX = 36,
        CERT = 37,
        A6 = 38,
        DNAME = 39,
        SINK = 40,
        OPT = 41,
        APL = 42,
        DS = 43,
        SSHFP = 44,
        IPSECKEY = 45,
        RRSIG = 46,
        NSEC = 47,
        DNSKEY = 48,
        DHCID = 49,
        NSEC3 = 50,
        NSEC3PARAM = 51,
        TLSA = 52,
        SMIMEA = 53,
        HIP = 55,
        NINFO = 56,
        RKEY = 57,
        TALINK = 58,
        CDS = 59,
        CDNSKEY = 60,
        OPENPGPKEY = 61,
        CSYNC = 62,
        ZONEMD = 63,
        SVCB = 64,
        HTTPS = 65,
        SPF = 99,
        UINFO = 100,
        UID = 101,
        GID = 102,
        UNSPEC = 103,
        NID = 104,
        L32 = 105,
        L64 = 106,
        LP = 107,
        EUI48 = 108,
        EUI64 = 109,
        NXNAME = 128,
        TKEY = 249,
        TSIG = 250,
        IXFR = 251,
        AXFR = 252,
        MAILB = 253,
        MAILA = 254,
        Wildcard = 255,
        Any = 255,
        URI = 256,
        CAA = 257,
        AVC = 258,
        DOA = 259,
        AMTRELAY = 260,
        RESINFO = 261,
        WALLET = 262,
        TA = 32768,
        DLV = 32769,
        Reserved = 65535
    }

    public enum Class : ushort
    {
        Reserved = 0,
        IN = 1,
        Internet = 1,
        Chaos = 3,
        CH = 3,
        Hesiod = 4,
        HS = 4,
        NONE = 254,
        ANY = 255,
    }

    //https://datatracker.ietf.org/doc/html/rfc2930#section-2.5

    public enum TKeyMode : ushort
    {
        Reserved = 0,
        ServerAssignment = 1,
        DH = 2,
        GSSAPI = 3,
        ResolverAssignment = 4,
        KeyDeletion = 5,
    }

    #endregion

    #region SVCB
    //https://www.iana.org/assignments/dns-svcb/dns-svcb.xhtml

    public enum SvcbParamKeys : ushort
    {
        mandatory = 0,
        alph = 1,
        no_default_alpn = 2,
        port = 3,
        ipv4hint = 4,
        ech = 5,
        ipv6hint = 6,
        dohpath = 7,
        ohttp = 8,
        tls_supported_groups = 9,
    }

    #endregion

    #region DNSSEC
    //https://www.iana.org/assignments/dnskey-flags/dnskey-flags.xhtml

    [Flags]
    public enum DnsSecDnsKeyFlags : ushort
    {
        ZONE = 0x0100,
        REVOKE = 0x0080,
        SEP = 0x0001,
    }

    //https://www.iana.org/assignments/dns-sec-alg-numbers/dns-sec-alg-numbers.xhtml

    public enum DnsSecAlgorithm : byte
    {
        DELETE = 0,
        RSAMD5 = 1,
        DH = 2,
        DSA = 3,
        Reserved4 = 4,
        RSASHA1 = 5,
        DSA_NSEC3_SHA1 = 6,
        RSASHA1_NSEC3_SHA1 = 7,
        RASSHA256 = 8,
        Reserved9 = 9,
        RASSHA512 = 10,
        Reserved11 = 11,
        ECC_GOST = 12,
        ECDSAP256SHA256 = 13,
        ECDSAP384SHA384 = 14,
        ED25519 = 15,
        ED448 = 16,
        SM2SM3 = 17,
        ECC_GOST12 = 23,
        INDIRECT = 252,
        PRIVATEDNS = 253,
        PRIVATEOID = 254,
        Reserved255 = 255,
    }

    //https://www.iana.org/assignments/ds-rr-types/ds-rr-types.xhtml

    public enum DnsSecDsDigestAlgorithm : byte
    {
        Reserved = 0,
        SHA1 = 1,
        SHA256 = 2,
        GOST_R_3411_94 = 3,
        SHA384 = 4,
        GPST_R_3411_2012 = 5,
        SM3 = 6,
    }

    //https://www.iana.org/assignments/dnssec-nsec3-parameters/dnssec-nsec3-parameters.xhtml

    [Flags]
    public enum DnsSecNSec3Flags : byte
    {
        Opt_Out = 0x1,
    }

    [Flags]
    public enum DnsSecNSec3ParamFlags : byte
    {
        Reserved = 0x1,
    }

    public enum DnsSecNSec3HashAlgorithm : byte
    {
        Reserved = 0,
        SHA1 = 1,
    }

    #endregion
}
