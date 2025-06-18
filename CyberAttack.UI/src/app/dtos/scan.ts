export interface AttackResult {
  success: boolean;
  scanId?: number;
  errorMessage?: string;
  status:number;
}

export interface StatusResult {
  success: boolean;
  status?: number;
  errorMessage?: string;
}

export interface Alert {
  pluginId: string;
  name: string;
  risk: string;
  description: string;
  solution: string;
  url: string;
}

export interface AlertExtResult {
  success: boolean;
  alerts: Alert[];
  errorMessage?: string;
}

export interface DirBruteForceResult {
  success: boolean;
  urlLeaks?: string[];
  errorMessage?: string;
}


export interface SSLAnalyzerResult{
    success: boolean;
  errorMessage?: string;
  sslSummary:SslSummary
}

export interface SslSummary {
    host: string;
    status: string;
    testTime: string;
    endpoints: EndpointSummary[];
    certificate: CertificateSummary;
    hstsPolicy: string;
}

export interface EndpointSummary {
    ipAddress: string;
    grade: string;
    hasWarnings: boolean;
    protocols: string[];
    preferredCiphers: string[];
}

export interface CertificateSummary {
    commonNames: string[];
    notBefore: string;
    notAfter: string;
}