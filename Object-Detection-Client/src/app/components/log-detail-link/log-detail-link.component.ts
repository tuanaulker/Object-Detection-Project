import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { LogService } from '../../services/log.service';
import { Log } from '../../interfaces/log';
import Swal from 'sweetalert2';
import { UserService } from '../../services/user.service';
import { UserOperationDto } from '../../interfaces/userOperationDto';

@Component({
  selector: 'app-log-detail-link',
  templateUrl: './log-detail-link.component.html',
  styleUrls: ['./log-detail-link.component.scss'],
  providers: [DialogService]
})
export class LogDetailLinkComponent implements OnInit {

  log: any;
  chosenStable: boolean = false;
  userList: UserOperationDto[] = [];
  isAlertToCheckStable: string; 
  constructor(
    private route: ActivatedRoute, 
    private logService: LogService, 
    private userService: UserService) { }

  ngOnInit(): void {
    let logId = this.route.snapshot.params['id'];
    this.logService.getLogById(logId).subscribe((res) => {
      if(res.isSuccessful == true){
        this.log = res.data;
        this.isAlertToCheckStable = this.log.actionStatus;
        this.getUserList();
      }
      else{
        //display message redirect to home
      }
    })
  }

  getUserList(){
    this.userService.getAllUsers().subscribe((res) =>  {
      if(res.isSuccessful){
        this.userList = res.data;
      }
    })
  }

  update(){
    if (this.log.actionStatus === 'Stable') {
      if (!this.log.details || this.log.details.trim() === '') {
        this.chosenStable = true;
        
      }
    }

    if (this.log.actionStatus === 'Stable' && this.isAlertToCheckStable =='Alert') {
      if (!this.log.details || this.log.details.trim() === '') {
        this.chosenStable = true;
        Swal.fire({
          icon: "error",
          title: "Oops...",
          html: "Please provide a details why <b>No Need Action</b>."
        })
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
