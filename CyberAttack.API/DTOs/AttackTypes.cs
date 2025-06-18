namespace CyberAttack.API.DTOs;

public enum AttackTypes
{
    CrossSiteScripting_Reflected = 40012,
    CrossSiteScripting_Persistent = 40014,
    CrossSiteScripting_Persistent_Prime = 40016,
    CrossSiteScripting_Persistent_Spider = 40017,
    SqlInjection = 40018,
    SqlInjection_MySQL = 40019,
    SqlInjection_HypersonicSQL = 40020,
    SqlInjection_Oracle = 40021,
    SqlInjection_PostgreSQL = 40022,
    SqlInjection_SQLite = 40024,
    SqlInjection_MsSQL = 40027,
    Log4Shell = 40043,
    Spring4Shell = 40045,
    ServerSideCodeInjection = 90019,
    RemoteOSCommandInjection = 90020,
    XPathInjection = 90021,
    XmlExternalEntityAttack = 90023,
    ServerSideTemplateInjection = 90035,
    ServerSideTemplateInjection_Blind = 90036

}
