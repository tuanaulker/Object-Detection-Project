import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserLogin } from '../../interfaces/user-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  user: UserLogin = {
    usernameOrEmail: '',
    password: ''
  };

  showPassword: boolean = false;

  constructor(
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    public router: Router,
  ) { }

  login() {
    if (this.user.usernameOrEmail != '' && this.user.password != '') {
      this.authService.login(this.user).subscribe((res) => {
        const tokenT = localStorage.getItem("token");
        if (tokenT !== null) {
          this.router.navigate(['home'], { relativeTo: this.activatedRoute });
        }
      }, error => {
        alert("Incorrect Entry");
      });
    }
  }

  togglePasswordVisibility(passwordField: HTMLInputElement) {
    this.showPassword = !this.showPassword;
    passwordField.type = this.showPassword ? 'text' : 'password';
  }
}
