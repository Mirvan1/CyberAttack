import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AlertExtResult, AttackResult, DirBruteForceResult, SSLAnalyzerResult, StatusResult } from '../dtos/scan';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ScanService {
  private readonly base = `${environment.apiUrl}/api/Scan`;

  constructor(private http: HttpClient) {}

 startActiveScan(targetUrl: string, attackTypes: string): Observable<AttackResult> {
    const params = new HttpParams()
      .set('targetUrl', targetUrl)
      .set('attackTypes', attackTypes);
    return this.http.get<AttackResult>(`${this.base}/active-scan`, { params });
  }

  getActiveScanStatus(scanId: number, targetUrl: string, attackTypes: string): Observable<StatusResult | AlertExtResult> {
    let params = new HttpParams().set('scanId', scanId.toString())
      .set('targetUrl', targetUrl)
      .set('attackTypes', attackTypes);
    return this.http.get<StatusResult & AlertExtResult>(`${this.base}/status-attack2`, { params });
  }

  startPassiveScan(targetUrl: string, attackTypes: string): Observable<AlertExtResult> {
    const params = new HttpParams()
      .set('targetUrl', targetUrl)
      .set('attackTypes', attackTypes);
    return this.http.get<AlertExtResult>(`${this.base}/passive-scan`, { params });
  }

  directoryBruteForce(targetUrl: string): Observable<DirBruteForceResult> {
    const params = new HttpParams().set('targetUrl', targetUrl);
    return this.http.get<DirBruteForceResult>(`${this.base}/directory-brute-force`, { params });
  }

  
  disableScanner() {
    return this.http.get<AlertExtResult>(`${this.base}/disable-scan`);
  }

  sslAnalyzer(targetUrl: string): Observable<SSLAnalyzerResult> {
    const params = new HttpParams().set('targetUrl', targetUrl);
    return this.http.get<SSLAnalyzerResult>(`${this.base}/ssl-report`,{params});
  }

}
