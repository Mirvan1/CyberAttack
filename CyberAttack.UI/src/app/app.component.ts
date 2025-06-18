import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { ScanService } from './services/scan.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule     } from '@angular/material/input';
import { MatButtonModule    } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSelectModule    } from '@angular/material/select';
import { MatCardModule      } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule      } from '@angular/material/snack-bar';
import { CommonModule, JsonPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { StatusDialogComponent } from './status-dialog/status-dialog.component';
import { Alert } from './dtos/scan';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
     MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatSelectModule,
    MatCardModule,
    MatSnackBarModule,
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatListModule,
    MatExpansionModule,
    MatGridListModule,
    MatDividerModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
 })
export class AppComponent {
  title = 'CyberAttack';

  scanForm!: FormGroup;
  loading = false;
  result: any;
  activeScanStart=false;

 activeAttackOptions= [
    { value: '6',      viewValue: 'Path Traversal' },
    { value: '7',      viewValue: 'Remote File Inclusion' },
    { value: '10045',  viewValue: 'Source Code Disclosure - /WEB-INF Folder' },
    { value: '20015',  viewValue: 'Heartbleed OpenSSL Vulnerability' },
    { value: '20017',  viewValue: 'Source Code Disclosure - CVE-2012-1823' },
    { value: '20018',  viewValue: 'Remote Code Execution - CVE-2012-1823' },
    { value: '20019',  viewValue: 'External Redirect' },
    { value: '40009',  viewValue: 'Server Side Include' },
    { value: '40012',  viewValue: 'Cross Site Scripting (Reflected)' },
    { value: '40014',  viewValue: 'Cross Site Scripting (Persistent)' },
    { value: '40018',  viewValue: 'SQL Injection' },
    { value: '40019',  viewValue: 'SQL Injection - MySQL' },
    { value: '40020',  viewValue: 'SQL Injection - Hypersonic SQL' },
    { value: '40021',  viewValue: 'SQL Injection - Oracle' },
    { value: '40022',  viewValue: 'SQL Injection - PostgreSQL' },
    { value: '40024',  viewValue: 'SQL Injection - SQLite' },
    { value: '40026',  viewValue: 'Cross Site Scripting (DOM Based)' },
    { value: '40027',  viewValue: 'SQL Injection - MsSQL' },
    { value: '40043',  viewValue: 'Log4Shell' },
    { value: '40045',  viewValue: 'Spring4Shell' },
    { value: '90019',  viewValue: 'Server Side Code Injection' },
    { value: '90020',  viewValue: 'Remote OS Command Injection' },
    { value: '90021',  viewValue: 'XPath Injection' },
    { value: '90023',  viewValue: 'XML External Entity Attack' },
    { value: '90035',  viewValue: 'Server Side Template Injection' },
    { value: '90036',  viewValue: 'Server Side Template Injection (Blind)' },
    { value: '0',      viewValue: 'Directory Browsing' },
    { value: '30001',  viewValue: 'Buffer Overflow' },
    { value: '30002',  viewValue: 'Format String Error' },
    { value: '40003',  viewValue: 'CRLF Injection' },
    { value: '40008',  viewValue: 'Parameter Tampering' },
    { value: '40016',  viewValue: 'Cross Site Scripting (Persistent) - Prime' },
    { value: '40017',  viewValue: 'Cross Site Scripting (Persistent) - Spider' },
    { value: '50000',  viewValue: 'Script Active Scan Rules' },
    { value: '90026',  viewValue: 'SOAP Action Spoofing' },
    { value: '90029',  viewValue: 'SOAP XML Injection' }
  ];

 passiveAttackOptions = [
    { value: '10111', viewValue: 'Authentication Request Identified' },
    { value: '10112', viewValue: 'Session Management Response Identified' },
    { value: '10113', viewValue: 'Verification Request Identified' },
    { value: '10020', viewValue: 'Anti-clickjacking Header' },
    { value: '90022', viewValue: 'Application Error Disclosure' },
    { value: '10044', viewValue: 'Big Redirect Detected (Potential Sensitive Information Leak)' },
    { value: '10015', viewValue: 'Re-examine Cache-control Directives' },
    { value: '90011', viewValue: 'Charset Mismatch' },
    { value: '10038', viewValue: 'Content Security Policy (CSP) Header Not Set' },
    { value: '10055', viewValue: 'CSP' },
    { value: '10019', viewValue: 'Content-Type Header Missing' },
    { value: '10010', viewValue: 'Cookie No HttpOnly Flag' },
    { value: '90033', viewValue: 'Loosely Scoped Cookie' },
    { value: '10054', viewValue: 'Cookie without SameSite Attribute' },
    { value: '10011', viewValue: 'Cookie Without Secure Flag' },
    { value: '10098', viewValue: 'Cross-Domain Misconfiguration' },
    { value: '10017', viewValue: 'Cross-Domain JavaScript Source File Inclusion' },
    { value: '10202', viewValue: 'Absence of Anti-CSRF Tokens' },
    { value: '10033', viewValue: 'Directory Browsing' },
    { value: '10097', viewValue: 'Hash Disclosure' },
    { value: '10034', viewValue: 'Heartbleed OpenSSL Vulnerability (Indicative)' },
    { value: '2',     viewValue: 'Private IP Disclosure' },
    { value: '3',     viewValue: 'Session ID in URL Rewrite' },
    { value: '10023', viewValue: 'Information Disclosure - Debug Error Messages' },
    { value: '10024', viewValue: 'Information Disclosure - Sensitive Information in URL' },
    { value: '10025', viewValue: 'Information Disclosure - Suspicious Comments' },
    { value: '10105', viewValue: 'Weak Authentication Method' },
    { value: '10041', viewValue: 'HTTP to HTTPS Insecure Transition in Form Post' },
    { value: '10042', viewValue: 'HTTPS to HTTP Insecure Transition in Form Post' },
    { value: '90001', viewValue: 'Insecure JSF ViewState' },
    { value: '10108', viewValue: 'Reverse Tabnabbing' },
    { value: '10040', viewValue: 'Secure Pages Include Mixed Content' },
    { value: '10109', viewValue: 'Modern Web Application' },
    { value: '10062', viewValue: 'PII Disclosure' },
    { value: '10115', viewValue: 'Script Served From Malicious Domain (polyfill)' },
    { value: '10050', viewValue: 'Retrieved from Cache' },
    { value: '10036', viewValue: 'HTTP Server Response Header' },
    { value: '10035', viewValue: 'Strict-Transport-Security Header' },
    { value: '10096', viewValue: 'Timestamp Disclosure' },
    { value: '10030', viewValue: 'User Controllable Charset' },
    { value: '10029', viewValue: 'Cookie Poisoning' },
    { value: '10031', viewValue: 'User Controllable HTML Element Attribute (Potential XSS)' },
    { value: '10043', viewValue: 'User Controllable JavaScript Event (XSS)' },
    { value: '10028', viewValue: 'Open Redirect' },
    { value: '10057', viewValue: 'Username Hash Found' },
    { value: '10032', viewValue: 'Viewstate' },
    { value: '10061', viewValue: 'X-AspNet-Version Response Header' },
    { value: '10039', viewValue: 'X-Backend-Server Header Information Leak' },
    { value: '10052', viewValue: 'X-ChromeLogger-Data (XCOLD) Header Information Leak' },
    { value: '10021', viewValue: 'X-Content-Type-Options Header Missing' },
    { value: '10056', viewValue: 'X-Debug-Token Information Leak' },
    { value: '10037', viewValue: 'Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s)' },
    { value: '10003', viewValue: 'Vulnerable JS Library (Powered by Retire.js)' },
    { value: '90030', viewValue: 'WSDL File Detection' },
    { value: '50001', viewValue: 'Script Passive Scan Rules' },
    { value: '50003', viewValue: 'Stats Passive Scan Rule' }
  ];


    displayedColumns = ['pluginId','name','risk','url','description'];
  dataSource = new MatTableDataSource<Alert>([]);

  constructor(
    private fb: FormBuilder,
    private api: ScanService,
    private snack: MatSnackBar,
        private dialog: MatDialog,

  ) {}

  ngOnInit() {
    this.scanForm = this.fb.group({
      targetUrl:   ['', [Validators.required]],
      scanType:    ['active', Validators.required],
      attackTypes: [{value: [], disabled: false}, Validators.required]
    });

     this.scanForm.get('scanType')!.valueChanges.subscribe(type => {
      const atk = this.scanForm.get('attackTypes')!;
      if (type === 'active') {
        atk.enable();
        atk.setValidators([Validators.required]);
      } else if (type === 'passive') {
        atk.enable();
        atk.setValidators([Validators.required]);
      } 
      else if(type==='sslanalyze'){
    atk.enable();
        atk.setValidators([Validators.required]);
      }
      else   {
        atk.disable();
        atk.clearValidators();
        atk.setValue([]);
      }
      atk.updateValueAndValidity();
    });
  }

  onSubmit() {
    if (this.scanForm.invalid) {
      this.scanForm.markAllAsTouched();
      return;
    }

    const { targetUrl, scanType, attackTypes } = this.scanForm.value;
    this.loading = true;
    this.result = null;

    let obs:Observable<any>;
    if (scanType === 'active') {
       this.api.startActiveScan(targetUrl, attackTypes.join(',')).subscribe({
      next: (res: any) => {
        this.loading = false;
        this.result = null;
        this.activeScanStart=true;
console.log("start ")
if(res.status){
    const dialogRef = this.dialog.open(StatusDialogComponent, {
            width: '500px',
            disableClose:true,
            data: {
              scanId: res.scanId,
             targerUrl: targetUrl,
             attackTypes:attackTypes.join(',')
            }
          });

            dialogRef.afterClosed().subscribe(result => {
      console.log('Dialog kapandı, result:', result);
     this.activeScanStart=false;
    });
        }
      },
      error: (err: any) => {
        this.loading = false;
        this.snack.open('Error: ' + (err.error?.errorMessage || err.message), 'Close', {
          duration: 5000
        });
      }
    });
           
    } else if (scanType === 'passive') {
              this.activeScanStart=false;

     this.api.startPassiveScan(targetUrl, attackTypes.join(',')).subscribe({
      next: (res: any) => {
        this.loading = false;
        this.result = res;
          if (scanType !== 'dirbrute' && res.alerts) {
          this.dataSource.data = res.alerts;
        }
      },
      error: (err: any) => {
        this.loading = false;
        this.snack.open('Error: ' + (err.error?.errorMessage || err.message), 'Close', {
          duration: 5000
        });
      }
    });
    }
    
    else if (scanType === 'sslanalyze'){
              this.activeScanStart=false;
this.api.sslAnalyzer(targetUrl).subscribe({
      next: (res: any) => {
        this.loading = false;
        this.result = res;

      },
      error: (err: any) => {
        this.loading = false;
        this.snack.open('Error: ' + (err.error?.errorMessage || err.message), 'Close', {
          duration: 5000
        });
      }
    });


    }
    
    
    else {
              this.activeScanStart=false;

      this.api.directoryBruteForce(targetUrl).subscribe({
      next: (res: any) => {
        this.loading = false;
        this.result = res;
      },
      error: (err: any) => {
        this.loading = false;
        this.snack.open('Error: ' + (err.error?.errorMessage || err.message), 'Close', {
          duration: 5000
        });
      }
    });
    }

  }

   get currentAttackOptions() {
    const type = this.scanForm.get('scanType')!.value;
    return type === 'active'
      ? this.activeAttackOptions
      : this.passiveAttackOptions;
  }
}
