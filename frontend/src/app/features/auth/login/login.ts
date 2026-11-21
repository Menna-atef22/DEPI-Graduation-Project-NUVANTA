import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  email = 'admin@nuvanta.com';
  password = '123456';
  error = '';

  constructor(private router: Router) {}

  login() {
    if (!this.email.trim() || !this.password.trim()) {
      this.error = 'Please enter your email and password.';
      return;
    }

    // Demo login only — no real authentication.
    this.error = '';
    this.router.navigate(['/dashboard']);
  }
}
