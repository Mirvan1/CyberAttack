namespace CyberAttack.API.DTOs;

public class SSLAnalyzerResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public SslSummary sslSummary { get; set; }
}

public class SslLabsAnalyzeRequest
{
    public string host { get; set; }
    public bool all { get; set; } = true;
    public bool fromCache { get; set; } = false;
}

public class SslLabsStatus
{
    public string status { get; set; }         // DNS, IN_PROGRESS, READY, ERROR
    public string statusMessage { get; set; }
}

// public class SslLabsReport
//{
//    public string Host { get; set; }
//    public int Port { get; set; }
//    public string Protocol { get; set; }
//    public bool IsPublic { get; set; }
//    public string Status { get; set; }
//    public long StartTime { get; set; }
//    public long TestTime { get; set; }
//    public string EngineVersion { get; set; }
//    public string CriteriaVersion { get; set; }
//    public List<Endpoint> Endpoints { get; set; }
// }

//public class Endpoint
//{
//    public string IpAddress { get; set; }
//    public string ServerName { get; set; }
//    public string StatusMessage { get; set; }
//    public string Grade { get; set; }
//    public string GradeTrustIgnored { get; set; }
//    public bool HasWarnings { get; set; }
//    public bool IsExceptional { get; set; }
//    public int Progress { get; set; }
//    public int Duration { get; set; }
//    public int Delegation { get; set; }
//    public Details Details { get; set; }
//}

//public class Details
//{
//    public long HostStartTime { get; set; }

//}




public class SslLabsReport
{
    public string host { get; set; }
    public int port { get; set; }
    public string protocol { get; set; }
    public bool isPublic { get; set; }
    public string status { get; set; }
    public long startTime { get; set; }
    public long testTime { get; set; }
    public string engineVersion { get; set; }
    public string criteriaVersion { get; set; }
    public Endpoint[] endpoints { get; set; }
    public Cert[] certs { get; set; }
}

public class Endpoint
{
    public string ipAddress { get; set; }
    public string serverName { get; set; }
    public string statusMessage { get; set; }
    public string grade { get; set; }
    public string gradeTrustIgnored { get; set; }
    public bool hasWarnings { get; set; }
    public bool isExceptional { get; set; }
    public int progress { get; set; }
    public int duration { get; set; }
    public int delegation { get; set; }
    public Details details { get; set; }
}

public class Details
{
    public long hostStartTime { get; set; }
    public Certchain[] certChains { get; set; }
    public Protocol[] protocols { get; set; }
    public Suite[] suites { get; set; }
    public Namedgroups namedGroups { get; set; }
    public string serverSignature { get; set; }
    public bool prefixDelegation { get; set; }
    public bool nonPrefixDelegation { get; set; }
    public bool vulnBeast { get; set; }
    public int renegSupport { get; set; }
    public int sessionResumption { get; set; }
    public int compressionMethods { get; set; }
    public bool supportsNpn { get; set; }
    public bool supportsAlpn { get; set; }
    public string alpnProtocols { get; set; }
    public int sessionTickets { get; set; }
    public bool ocspStapling { get; set; }
    public bool sniRequired { get; set; }
    public int httpStatusCode { get; set; }
    public bool supportsRc4 { get; set; }
    public bool rc4WithModern { get; set; }
    public bool rc4Only { get; set; }
    public int forwardSecrecy { get; set; }
    public bool supportsAead { get; set; }
    public int protocolIntolerance { get; set; }
    public int miscIntolerance { get; set; }
    public Sims sims { get; set; }
    public bool heartbleed { get; set; }
    public bool heartbeat { get; set; }
    public int openSslCcs { get; set; }
    public int openSSLLuckyMinus20 { get; set; }
    public int ticketbleed { get; set; }
    public int bleichenbacher { get; set; }
    public bool poodle { get; set; }
    public int poodleTls { get; set; }
    public bool fallbackScsv { get; set; }
    public bool freak { get; set; }
    public int hasSct { get; set; }
    public bool ecdhParameterReuse { get; set; }
    public bool logjam { get; set; }
    public Hstspolicy hstsPolicy { get; set; }
    public Hstspreload[] hstsPreloads { get; set; }
    public Hpkppolicy hpkpPolicy { get; set; }
    public Hpkpropolicy hpkpRoPolicy { get; set; }
    public Staticpkppolicy staticPkpPolicy { get; set; }
    public Httptransaction[] httpTransactions { get; set; }
    public bool implementsTLS13MandatoryCS { get; set; }
    public int zeroRTTEnabled { get; set; }
    public int zombiePoodle { get; set; }
    public int goldenDoodle { get; set; }
    public bool supportsCBC { get; set; }
    public int zeroLengthPaddingOracle { get; set; }
    public int sleepingPoodle { get; set; }
}

public class Namedgroups
{
    public List[] list { get; set; }
}

public class List
{
    public int id { get; set; }
    public string name { get; set; }
    public int bits { get; set; }
    public string namedGroupType { get; set; }
}

public class Sims
{
    public Result[] results { get; set; }
}

public class Result
{
    public Client client { get; set; }
    public int errorCode { get; set; }
    public string errorMessage { get; set; }
    public int attempts { get; set; }
    public string certChainId { get; set; }
    public int protocolId { get; set; }
    public int suiteId { get; set; }
    public string suiteName { get; set; }
    public string kxType { get; set; }
    public int kxStrength { get; set; }
    public int namedGroupBits { get; set; }
    public int namedGroupId { get; set; }
    public string namedGroupName { get; set; }
    public string keyAlg { get; set; }
    public int keySize { get; set; }
    public string sigAlg { get; set; }
    public int alertType { get; set; }
    public int alertCode { get; set; }
}

public class Client
{
    public int id { get; set; }
    public string name { get; set; }
    public string version { get; set; }
    public bool isReference { get; set; }
    public string platform { get; set; }
}

public class Hstspolicy
{
    public int LONG_MAX_AGE { get; set; }
    public string status { get; set; }
    public Directives directives { get; set; }
}

public class Directives
{
}

public class Hpkppolicy
{
    public string status { get; set; }
    public object[] pins { get; set; }
    public object[] matchedPins { get; set; }
    public object[] directives { get; set; }
}

public class Hpkpropolicy
{
    public string status { get; set; }
    public object[] pins { get; set; }
    public object[] matchedPins { get; set; }
    public object[] directives { get; set; }
}

public class Staticpkppolicy
{
    public string status { get; set; }
    public object[] pins { get; set; }
    public object[] matchedPins { get; set; }
    public object[] forbiddenPins { get; set; }
    public object[] matchedForbiddenPins { get; set; }
}

public class Certchain
{
    public string id { get; set; }
    public string[] certIds { get; set; }
    public Trustpath[] trustPaths { get; set; }
    public int issues { get; set; }
    public bool noSni { get; set; }
}

public class Trustpath
{
    public string[] certIds { get; set; }
    public Trust[] trust { get; set; }
}

public class Trust
{
    public string rootStore { get; set; }
    public bool isTrusted { get; set; }
}

public class Protocol
{
    public int id { get; set; }
    public string name { get; set; }
    public string version { get; set; }
}

public class Suite
{
    public int protocol { get; set; }
    public List1[] list { get; set; }
    public bool preference { get; set; }
}

public class List1
{
    public int id { get; set; }
    public string name { get; set; }
    public int cipherStrength { get; set; }
    public string kxType { get; set; }
    public int kxStrength { get; set; }
    public int namedGroupBits { get; set; }
    public int namedGroupId { get; set; }
    public string namedGroupName { get; set; }
}

public class Hstspreload
{
    public string source { get; set; }
    public string hostname { get; set; }
    public string status { get; set; }
    public long sourceTime { get; set; }
}

public class Httptransaction
{
    public string requestUrl { get; set; }
    public int statusCode { get; set; }
    public string requestLine { get; set; }
    public string[] requestHeaders { get; set; }
    public string responseLine { get; set; }
    public string[] responseHeadersRaw { get; set; }
    public Responseheader[] responseHeaders { get; set; }
    public bool fragileServer { get; set; }
}

public class Responseheader
{
    public string name { get; set; }
    public string value { get; set; }
}

public class Cert
{
    public string id { get; set; }
    public string subject { get; set; }
    public string serialNumber { get; set; }
    public string[] commonNames { get; set; }
    public string[] altNames { get; set; }
    public long notBefore { get; set; }
    public long notAfter { get; set; }
    public string issuerSubject { get; set; }
    public string sigAlg { get; set; }
    public int revocationInfo { get; set; }
    public string[] crlURIs { get; set; }
    public int revocationStatus { get; set; }
    public int crlRevocationStatus { get; set; }
    public int ocspRevocationStatus { get; set; }
    public bool dnsCaa { get; set; }
    public bool mustStaple { get; set; }
    public int sgc { get; set; }
    public int issues { get; set; }
    public bool sct { get; set; }
    public string sha1Hash { get; set; }
    public string sha256Hash { get; set; }
    public string pinSha256 { get; set; }
    public string keyAlg { get; set; }
    public int keySize { get; set; }
    public int keyStrength { get; set; }
    public string raw { get; set; }
    public bool keyKnownDebianInsecure { get; set; }
}




public class SslSummary
{
    public string Host { get; set; }
    public string Status { get; set; }
    public DateTime TestTime { get; set; }
    public List<EndpointSummary> Endpoints { get; set; }
    public CertificateSummary Certificate { get; set; }
    public string HstsPolicy { get; set; }
}

public class EndpointSummary
{
    public string IpAddress { get; set; }
    public string Grade { get; set; }
    public bool HasWarnings { get; set; }
    public List<string> Protocols { get; set; }
    public List<string> PreferredCiphers { get; set; }
}

public class CertificateSummary
{
    public IEnumerable<string> CommonNames { get; set; }
    public DateTime NotBefore { get; set; }
    public DateTime NotAfter { get; set; }
}


