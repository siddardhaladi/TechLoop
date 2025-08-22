import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from './Authservice';
 
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [CommonModule, AuthService]
})
export class LoginComponent {
  isSignDivVisiable = false;
  signUpObj = { name: '', email: '', password: '', role: 'Student' };
  loginObj = { email: '', password: '', role: 'Student' };
  errorMessage = ''; // Popup error message
 
  constructor(private auth: AuthService, private router: Router) {}
 
  onRegister(): void {
    this.auth.register(this.signUpObj).subscribe(
      (response: any) => {
        console.log('Registration successful', response);
        this.isSignDivVisiable = false;
      },
      (error) => {
        this.errorMessage = 'Registration failed! Please check your details.';
      }
    );
  }
 
  onLogin(): void {
    this.auth.login(this.loginObj).subscribe(
      (response: any) => {
        console.log('Login successful', response);
        localStorage.setItem('token', response.token);
        this.router.navigate(['/Homepage']);
      },
      (error) => {
        this.errorMessage = 'Login failed! Incorrect email or password.';
      }
    );
  }
}
 