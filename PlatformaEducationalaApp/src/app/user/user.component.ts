import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { Router } from '@angular/router';
import { EssentialService } from '../shared/essential.service';
import { Course } from '../shared/course.model';
import { User } from '../shared/user.model';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styles: ``,
})
export class UserComponent implements OnInit {
  userDetail: User = new User();

  constructor(
    public essentialService: EssentialService,
    public userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    //check if the user is logged in
    //if not, redirect to login page
    if (!this.userService.essentialService.isUserLoggedIn()) {
      this.router.navigate(['user/login']);
    }

    // get the logged in user details
    this.userService.getLoggedUserDetails().subscribe({
      next: (res) => {
        this.userDetail = res as User;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  logout() {
    this.userService.logout().pipe(
      finalize(() => {
        console.log('logout completed');
        // This code will run after the Observable completes
      })
    ).subscribe({
      next: (res) => {
        console.log(res);
        this.router.navigate(['user/login']);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  deleteUser() {
    this.userService.deleteUser().subscribe({
      next: (res) => {
        // redirect to login page
        this.router.navigate(['user/login']);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  changeEmail(newEmail: string) {
    this.userService.changeEmail(newEmail).subscribe({
      next: (res) => {
        // redirect to login page
        this.router.navigate(['user']);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
