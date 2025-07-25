import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  imports: [FormsModule],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss',
})
export class LoginFormComponent {
  constructor(private authService: AuthService, private router: Router) {}
  public data = {
    email: '',
    password: '',
  };
  public handleSubmit = () => {
    this.authService.loginUser(this.data).subscribe({
      next: (res) => {
        localStorage.setItem('session', JSON.stringify(res.data));
        localStorage.setItem('loginstatus', JSON.stringify(res.message));
        alert(res.message);

        this.router.navigate(['/menu']);
      },
      error: (err) => {
        alert('Error: ' + err.error.message);
      },
    });
  };
}
