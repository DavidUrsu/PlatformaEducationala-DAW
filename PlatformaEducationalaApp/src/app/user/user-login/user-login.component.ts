import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styles: ``,
})
export class UserLoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({
    username: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required),
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
    if (this.loginForm.valid) {
      this.userService
        .login(this.loginForm.value.username, this.loginForm.value.password)
        .pipe(
          finalize(() => {
            this.router.navigate(['/user']);
            // This code will run after the Observable completes
          })
        )
        .subscribe({
          next: (res) => {
            console.log(res);
          },
          error: (err) => {
            console.log(err);
          },
        });
    }
  }
}
