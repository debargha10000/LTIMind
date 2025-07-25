import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user-service.service';

@Component({
  selector: 'app-register-form',
  imports: [FormsModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.scss',
})
export class RegisterFormComponent {
  public data = {
    name: '',
    address: '',
    email: '',
    contactNumber: '',
    password: '',
  };

  constructor(private userService: UserService, private router: Router) {}

  public handleSubmit = () => {
    this.userService.createNewCustomer(this.data).subscribe({
      next: (res) => {
        // localStorage.setItem('session', JSON.stringify(res.data));
        // localStorage.setItem('loginstatus', JSON.stringify(res.message));
        console.log(res);
        alert(res.message);
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.log(err);
        alert('Error: ' + err.error.message);
      },
    });
  };
}
