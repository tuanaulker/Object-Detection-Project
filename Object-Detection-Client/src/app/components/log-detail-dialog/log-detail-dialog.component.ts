import { Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { LogService } from '../../services/log.service';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import Swal from 'sweetalert2';
import { UserOperationDto } from '../../interfaces/userOperationDto';

@Component({
  selector: 'app-log-detail-dialog',
  templateUrl: './log-detail-dialog.component.html',
  styleUrl: './log-detail-dialog.component.scss',
  providers: [DialogService],
  encapsulation: ViewEncapsulation.None
})
export class LogDetailDialogComponent implements OnInit {
  log: any;
  isLogUpdated: boolean = false;
  chosenStable: boolean = false;
  userList: UserOperationDto[] = [];
  isAlertToCheckStable: string; 
  constructor( private logService: LogService,
    public ref: DynamicDialogRef, 
    public config: DynamicDialogConfig
    
  ) { }

  ngOnInit(): void {
    this.log = this.config?.data?.log;
    this.userList = this.config?.data?.userList;
    this.isAlertToCheckStable = this.log.actionStatus;
  }


  closeDialog() {
    this.ref.close({
      updated: this.isLogUpdated
    });
  
  }

  update(){
    if (this.log.actionStatus === 'Stable' && this.isAlertToCheckStable =='Alert') {
      if (!this.log.details || this.log.details.trim() === '') {
        this.chosenStable = true;
        alert('Please provide a details why no need action.');
        return; 
      }
      else{
        this.log.actionTaken = 'No need action';
      }
    }
    else if (this.log.actionStatus === 'Stable' && this.isAlertToCheckStable !='Alert') {
      if (!this.log.details || this.log.details.trim() === '') {
        this.chosenStable = true;
        alert('Please provide a details action taken.');
        return; 
      }
    }

    this.logService.updateLog(this.log).subscribe((res) => {
      if(res.isSuccessful == true){
        this.isLogUpdated = true;
        this.closeDialog();
        Swal.fire({
          title: "Good job!",
          text: "You updated the log record!",
          icon: "success"
        });
      }
      else if (res.isSuccessful == false){
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "Something went wrong!"
        });
      }
    })
  }
}

