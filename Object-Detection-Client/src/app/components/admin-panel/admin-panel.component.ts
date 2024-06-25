import { Component, ElementRef, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserOperationDto } from '../../interfaces/userOperationDto';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import Swal from 'sweetalert2';
import { SelectItem } from 'primeng/api';
import { FormsModule } from '@angular/forms';
import { AddUserDialogComponent } from '../add-user-dialog/add-user-dialog.component';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss'],
  providers: [DialogService, FormsModule]
})
export class AdminPanelComponent implements OnInit {

  loading: boolean = true;
  users!: UserOperationDto[];
  filteredUsers: UserOperationDto[];
  statuses!: SelectItem[];
  clonedUser: { [s: string]: UserOperationDto } = {};
  userImageLinks: string;
  ref: DynamicDialogRef | undefined;

  constructor(private userService: UserService,
    public dialogService: DialogService,
    private elementRef: ElementRef) { }

  ngOnInit() {
    const userIdToRemove = '94c328af-952d-42a5-ae86-4f0fe6d84d74';
    this.userService.getAllUsers().subscribe((res) => {
      if (res.isSuccessful) {
        this.users = res.data;
        this.users = this.users.filter(user => user.id !== userIdToRemove);
        this.loading = false;
        this.filteredUsers = this.users;
      }
      else {
        //
      }
    });
    this.loading = false;


    this.statuses = [
      // { label: 'Super Admin', value: '4dc5874d-f3be-459a-b05f-2244512d13e3' },
      { label: 'Admin', value: '6a2c4fe5-5b10-45b6-a1f6-7cfecc629d3f' },
      { label: 'User', value: '336e1648-5384-4d2c-b886-0281db620ccb' }
    ];

    this.userImageLinks = 'assets/images/userProfile.png';
  }

  onRowEditInit(user: UserOperationDto) {
    this.clonedUser[user.id as string] = { ...user };
  }

  onRowEditSave(user: UserOperationDto) {
    if (!this.isEqual(this.clonedUser[user.id], user)) {
      const role = this.statuses.find(status => status.value === user.role)?.label;
      if (role) {
        Swal.fire({
          title: "Do you want to save the changes?",
          showDenyButton: true,
          showCancelButton: true,
          confirmButtonText: "Save",
          denyButtonText: `Don't save`
        }).then((result) => {
          if (result.isConfirmed) {
            user.role = role;
            this.userService.updateUser(user).subscribe((res) => {
              if (res.isSuccessful == true) {
                Swal.fire("Saved!", "", "success");
                this.ngOnInit();
              }
              else {
                alert(res.message);
              }
            })
          } else if (result.isDenied) {
            Swal.fire("Changes are not saved", "", "info");
          }
        });
      }
    }
    else {
      // nothing changed
    }
  }

  isEqual(obj1: UserOperationDto, obj2: UserOperationDto): boolean {
    return JSON.stringify(obj1) === JSON.stringify(obj2);
  }

  onRowEditCancel(user: UserOperationDto, index: number) {
    this.users[index] = this.clonedUser[user.id as string];
    delete this.clonedUser[user.id as string];
  }

  getSeverity(role: string) {
    switch (role) {
      case '6a2c4fe5-5b10-45b6-a1f6-7cfecc629d3f': //admin
        return 'primary';
      case '336e1648-5384-4d2c-b886-0281db620ccb': // user
        return 'success';
    }
    return ''
  }

  addUser() {
    this.ref = this.dialogService.open(AddUserDialogComponent, {
      header: 'Add New User',
      width: '33%',
      height: '87%'
    })

    this.ref.onClose.subscribe((isAdded: any) => {
      // debugger;
      // if (isAdded.isAdded) {
      this.ngOnInit();
      // }
    });
  }

  profileDecide(profileUrl: string) {
    if (profileUrl) {
      return profileUrl;
    } else {
      return this.userImageLinks;
    }
  }

  delete(userId: string) {
    Swal.fire({
      title: "Do you want to delete this user?",
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: "Delete",
      denyButtonText: `Don't delete`
    }).then((result) => {
      if (result.isConfirmed) {
        this.userService.deleteUser(userId).subscribe((res) => {
          if (res.isSuccessful == true) {
            Swal.fire("Deleted!", "", "success");
            this.ngOnInit();
          }
        })
      } else if (result.isDenied) {
        Swal.fire("Changes are not saved", "", "info");
      }
    });
  }

  searchUsers(searchTerm: string) {
    if (searchTerm) {
      this.filteredUsers = this.users.filter(user =>
        user.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
        user.surname.toLowerCase().includes(searchTerm.toLowerCase()) ||
        user.username.toLowerCase().includes(searchTerm.toLowerCase()) ||
        user.email.toLowerCase().includes(searchTerm.toLowerCase())
      );
    } else {
      this.filteredUsers = this.users;
    }
  }
}
