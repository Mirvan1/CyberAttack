import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { interval, Subscription, switchMap } from 'rxjs';
import { ScanService } from '../services/scan.service';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-status-dialog',
  standalone: true,
  imports: [CommonModule,MatDialogModule,MatListModule,MatProgressBarModule,MatButtonModule],
  templateUrl: './status-dialog.component.html',
  styleUrl: './status-dialog.component.css'
})
export class StatusDialogComponent {
 progress = 0;
  alerts: any[] = [];
  private sub!: Subscription;

  constructor(
    private dialogRef: MatDialogRef<StatusDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private svc: ScanService
  ) {}

  ngOnInit() {
      this.sub = interval(10_000).pipe(
       switchMap(() => this.svc.getActiveScanStatus(this.data.scanId,this.data.targerUrl,this.data.attackTypes))
    ).subscribe((res:any) => {
      if (!res.success) {
        console.error(res.errorMessage);
        return;
      }
 if(res.status<100){
      this.progress = res.status;
      }
            else if(res.status != undefined && res.alerts) {
         this.alerts = res.alerts;
        this.sub.unsubscribe(); 
         this.dialogRef.close();
      }
    });
     this.svc.getActiveScanStatus(this.data.scanId,this.data.targerUrl,this.data.activeScan).subscribe((res:any) => {
      if(res.status<100){
      this.progress = res.status;
      }
    });
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }

  close()
  {
    this.sub?.unsubscribe();
    this.svc.disableScanner().subscribe();

  }
}
