import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styles: ``,
})
export class UserRegisterComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({
    email: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required),
    username: new FormControl(null, Validators.required),
  });

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit() {
    //check if the user is logged in
    //if yes, redirect to user page
    if (this.userService.essentialService.isUserLoggedIn()) {
      this.router.navigate(['/user']);
    }
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.userService
        .register(
          this.registerForm.value.username,
          this.registerForm.value.password,
          this.registerForm.value.email
        )
        .pipe(
          finalize(() => {
            this.router.navigate(['/user']);
            // This code will run after the Observable completes
          })
        )
        .subscribe({
          next: (res) => {
            console.log(res);
            this.router.navigate(['/user']);
          },
          error: (err) => {
            console.log(err);
          },
        });
    }
  }
}
