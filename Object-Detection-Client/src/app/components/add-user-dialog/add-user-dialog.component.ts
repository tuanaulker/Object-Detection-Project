import { Component } from '@angular/core';
import { UserOperationDto } from '../../interfaces/userOperationDto';
import { AddUser } from '../../interfaces/add-user';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import Swal from 'sweetalert2';
import { UserService } from '../../services/user.service';
import { DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-add-user-dialog',
  templateUrl: './add-user-dialog.component.html',
  styleUrl: './add-user-dialog.component.scss',
  providers: [InputGroupAddonModule]
})
export class AddUserDialogComponent {
  profile: string;
  newUser: AddUser = {
    name: '',
    surname: '',
    username: '',
    email: '',
    password: '',
    role: '',
    profileUrl: ''
  }

  roles = ['Admin', 'User'];
  isAdded: boolean = false;

  constructor(private userService: UserService, public ref: DynamicDialogRef,) {

  }

  addUser() {
    if (!this.newUser.name.trim() || !this.newUser.surname.trim() || !this.newUser.username.trim() ||
      !this.newUser.email.trim() || !this.newUser.password.trim() || !this.newUser.role.trim()) {
      alert("Please fill in all fields.");
    } else {
      this.userService.addUser(this.newUser).subscribe((res) => {
        if (res.isSuccessful) {
          this.isAdded = true;
        }
        else {
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Something went wrong!",
          });
        }
        this.closeDialog();
      })
    }
  }

  profileAdded() {
    this.newUser.profileUrl = this.profile;
  }

  generateUsername() {
    if (this.newUser.name && this.newUser.surname) {
      this.newUser.username = this.newUser.name.toLowerCase() + '.' + this.newUser.surname.toLowerCase();
    }
  }

  closeDialog() {
    this.ref.close({
      updated: this.isAdded
    });

  }
}
