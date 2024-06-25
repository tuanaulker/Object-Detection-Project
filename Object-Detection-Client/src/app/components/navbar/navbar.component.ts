import { Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { TokenService } from '../../services/token.service';
import { AuthService } from '../../services/auth.service';
import { AlertDto } from '../../interfaces/alertDto';
import { LogService } from '../../services/log.service';
import { interval } from 'rxjs';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AlertDialogComponent } from '../alert-dialog/alert-dialog.component';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
import { AlertLogService } from '../../services/alertLogService';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
  encapsulation: ViewEncapsulation.Emulated
})
export class NavbarComponent {
  public items: MenuItem[] | undefined;
  alerts: AlertDto[] = [];
  length: number = 0;
  private audio: HTMLAudioElement;
  isAudioPlaying: boolean = false;

  constructor(
    private tokenService: TokenService,
    private authService: AuthService,
    private logService: LogService,
    public dialogService: DialogService,
    private elementRef: ElementRef, 
    private router: Router,
    private alertLogService: AlertLogService
  ) {

  }
  ref: DynamicDialogRef | undefined;
  ngOnInit() {
    this.isAudioPlaying = false;
  
    this.audio = new Audio('assets/sounds/alarm-sound.mp4');
    this.audio.muted = true;
    this.getAlerts();
    interval(5000).subscribe(() => {
      this.getAlerts();
      //this.alertLogService.triggerGetLog();
    });
    const fullName = this.tokenService.tokenFullName();
    this.updateNavbarItems(fullName);
  }

  updateNavbarItems(fullName: string) {
    let items = [
      {
        label: fullName,
        icon: 'pi pi-fw pi-user',
      },
      {
        label: ` ${this.generateAlertsLabel()} Alerts`,
        icon: 'pi pi-fw pi-bell',
        items: this.alerts.map(alert => {
          return {
            label: `
            <div class="submenunavbar">
              <i class="pi pi-shield"></i>&nbsp; ${alert.eventType}
              <br>
              <i class="pi pi-video"></i>&nbsp; ${alert.location}
              <br>
              <i class="pi pi-clock"></i>&nbsp; ${new Date(alert.capturedTime).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', second: '2-digit' })}
            </div>
            <br>
            <hr>`,
            icon: 'pi pi-fw pi-exclamation-triangle',
            command: () => this.alertDetail(alert.id)
          };
        })
      },
      {
        label: 'Logout',
        icon: 'pi pi-fw pi-power-off',
        command: () => this.authService.logOut()
      }
    ];
    
    // Check if the user has admin or super admin role then include the 'Admin Panel' item
    if (this.tokenService.hasRole('Admin') || this.tokenService.hasRole('SuperAdmin')) {
      items.splice(1, 0, {
        label: 'Admin Panel',
        icon: 'pi pi-fw pi-cog',
        command: () => this.router.navigateByUrl('/admin-panel')
      });
    }
    
    this.items = items;
  }

  

  getAlerts() {
    this.logService.logListen().subscribe((res) => {
      this.alerts = res.data;
      if (this.alerts.length != 0) {
        if (this.isAudioPlaying === false) {
          this.audio.play();
          this.audio.loop = true;
          this.isAudioPlaying = true;
          this.audio.muted = false;
        }
      }
      else {
        this.audio.pause();
        this.isAudioPlaying = false;
      }
      this.updateNavbarItems(this.tokenService.tokenFullName());
    })
  }

  generateAlertsLabel(): string {
    if (this.alerts.length > 0) {
      return `${this.alerts.length}`;
    } else {
      return ``;

    }
  }

  alertDetail(id: string) {
    this.logService.getLogById(id).subscribe((res) => {
      if (res.isSuccessful == true) {
        this.ref = this.dialogService.open(AlertDialogComponent, {
          data: {
            alert: res.data,
          },
          header: 'ALERT',
          width: '70%'
        })
        this.ref.onClose.subscribe((isUpdated: any) => {
          if (isUpdated) {
            this.getAlerts();
          } else {
            // No change any log
          }
        });
      }
      else {
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "Something went wrong!",
          footer: '<a href="http://localhost:4200/log-detail/' + id + '">Try to access the details by clicking here.</a>'
        });

      }
    })
    //http://localhost:4200/log-detail/73a95e04-06f0-4590-9190-95dd0249dd96
  }

  clickedAlertButton() {
    this.audio.pause();
    this.isAudioPlaying = false;
  }

}  