import { Component, ElementRef, OnInit } from '@angular/core';
import { LogService } from '../../services/log.service';
import { LogDto } from '../../interfaces/logDto';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { LogDetailDialogComponent } from '../log-detail-dialog/log-detail-dialog.component';
import { Log } from '../../interfaces/log';
import { Router } from '@angular/router';
import { AlertDialogComponent } from '../alert-dialog/alert-dialog.component';
import { Alert } from '../../interfaces/alert';
import { UpdateService } from '../../services/updateService';
import { SelectItem } from 'primeng/api';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';
import { UserOperationDto } from '../../interfaces/userOperationDto';
import { UserService } from '../../services/user.service';
import { AlertLogService } from '../../services/alertLogService';

@Component({
  selector: 'app-home-table',
  templateUrl: './home-table.component.html',
  styleUrls: ['./home-table.component.scss'],
  providers: [DialogService, FormsModule, DropdownModule]
})
export class HomeTableComponent implements OnInit {

  logs: LogDto[] = [];
  unfilteredLogs: LogDto[] = [];
  loading: boolean = true;
  ref: DynamicDialogRef | undefined;
  statuses: SelectItem[] = [
    { label: 'Any', value: null },
    { label: 'Alert', value: 'Alert' },
    { label: 'Progress', value: 'Progress' },
    { label: 'Stable', value: 'Stable' }
  ];
  userList: UserOperationDto[] = [];
  areas: string[] = [
    'Manufacturing Area',
    'Storage Area',
    'Warehouse',
    'Lab',
    'Maintenance Workshop',
    'Offices',
    'Gardens'
  ];
  selectedArea: null;

  constructor(
    private logService: LogService,
    public dialogService: DialogService,
    private elementRef: ElementRef,
    private updateService: UpdateService,
    private userService: UserService,
    private alertLogService: AlertLogService
  ) { }


  ngOnInit(): void {
    this.getUserList();
    this.getAllLogs();
    this.updateService.updateEvent.subscribe(() => {
      this.getAllLogs();
    });
  //   this.alertLogService.alertReceived$.subscribe(() => {
  //     this.getAllLogs();
  //   });
  }


  getAllLogs() {
    this.logService.getAllLogs().subscribe((res) => {
      this.logs = res.data;
      this.loading = false;
      this.logs.forEach(log => {
        log.capturedTime = new Date(log.capturedTime);
      });
      this.unfilteredLogs = this.logs;
    })
  }

  getUserList(){
    this.userService.getAllUsers().subscribe((res) =>  {
      if(res.isSuccessful){
        this.userList = res.data;
      }
    })
  }

  filterLogsByArea(){
    if (this.selectedArea) {
      this.logs = this.logs.filter(log => log.areas === this.selectedArea);
    } else {
      this.logs = [...this.unfilteredLogs]; 
    }
  }

  filterGlobal(keyword: string) {
    const searchKey = keyword.toLowerCase();
    if (searchKey.length >= 1) {
      this.logs = this.unfilteredLogs.filter(log => {
        return Object.values(log).some(value => {
          if (typeof value === 'string') {
            const text = value.toLowerCase();
            return text.includes(searchKey);
          } else if (typeof value === 'number' || typeof value === 'boolean') {
            const text = value.toString().toLowerCase();
            return text.includes(searchKey);
          }
          return false;
        }) || log.eventType.toLowerCase().includes(searchKey);
      });
    } else {
      this.logs = this.unfilteredLogs;
    }
  }

  filterStatus(selectedState: string) {
    if (selectedState) {
      this.logs = this.logs.filter(log => log.actionStatus === selectedState);
    } else {
      this.logs = this.unfilteredLogs;
    }
  }

  getSeverityStatus(state: string): string {
    switch (state) {
      case 'Alert':
        return 'danger';
      case 'Progress':
        return 'warning';
      case 'Stable':
        return 'success';
      default:
        return 'neutral';
    }
  }



  getActionStatusIcon(actionStatus: string): string {
    let status = actionStatus.toLowerCase();
    switch (status) {
      case 'alert':
        return 'pi pi-exclamation-triangle';
      case 'progress':
        return 'pi pi-clock';
      case 'stable':
        return 'pi pi-check';
      default:
        return '';
    }
  }

  getActionStatusRowColor(rowData: any): string {
    let status = rowData.actionStatus.toLowerCase();
    switch (status) {
      case 'alert':
        return 'alert-row';
      case 'progress':
        return 'progress-row';
      case 'stable':
        return 'stable-row';
      default:
        return '';
    }
  }

  getLogById(id: string) {
    this.logService.getLogById(id).subscribe((res) => {
      if (res.isSuccessful == false) {
        alert('can not find object'); // ADD SWEET ALERT
      }
      else {
        this.ref = this.dialogService.open(LogDetailDialogComponent, {
          data: {
            log: res.data,
            userList: this.userList
          },
          header: 'Log Details',
          width: '75%'
        })

        this.ref.onClose.subscribe((dialogResult: any) => {
          if (dialogResult) {
            this.ngOnInit();
          } else {
            // No change any log
          }
        });
      }

    })
  }

}
