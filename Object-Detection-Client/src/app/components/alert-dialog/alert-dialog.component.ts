import { Component, OnInit } from '@angular/core';
import { Alert } from '../../interfaces/alert';
import { LogService } from '../../services/log.service';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UpdateService } from '../../services/updateService';

@Component({
  selector: 'app-alert-dialog',
  templateUrl: './alert-dialog.component.html',
  styleUrl: './alert-dialog.component.scss',
  providers: [DialogService]
})
export class AlertDialogComponent implements OnInit{

  alert: any;
  isUpdated: boolean = false;
 detailAreaFill: boolean = true;

  constructor(private logService: LogService,
    public ref: DynamicDialogRef, 
    public config: DynamicDialogConfig,
    private updateService: UpdateService) {
  }


  ngOnInit(): void {
    // this.audio = new Audio('assets/sounds/??.mp3');
    // this.audio.play();
    // this.audio.loop = true;
    this.alert = this.config?.data?.alert;
  }


  stopAudio(){
    //this.audio.pause();
    
  }

  closeDialog() {
    this.ref.close({
      isUpdated: this.isUpdated
    });
  
  }

  update(){
    if(this.alert.actionStatus == 'Stable' && this.alert.actionTaken == null){
      this.alert.actionTaken = 'No need action';
      if(this.alert.detail == null){
        alert('You need to provide detail why no need action?');
        this.detailAreaFill = false;
        return;
      }
    }
    
    this.logService.updateLog(this.alert).subscribe((res) => {
      if(res.isSuccessful){
        this.isUpdated = true;
        this.updateService.triggerUpdate();
        this.closeDialog();
      }
      else{
       alert('problem!')
      }

    })
  }

}
