import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../shared/course.service';
import { Router } from '@angular/router';
import { EssentialService } from '../../shared/essential.service';
import { Course } from '../../shared/course.model';

@Component({
  selector: 'app-enrollments',
  templateUrl: './enrollments.component.html',
  styles: ``,
})
export class EnrollmentsComponent implements OnInit {
  constructor(
    public service: CourseService,
    private router: Router,
    public essentialService: EssentialService
  ) {}

  enrolledCourses: any = [];
  enrolledCoursesDetails: Course[] = [];

  ngOnInit(): void {
    this.service.refreshList();

    //check if the user is logged in
    if (this.service.loggedInUserId) {
      this.service
        .getEnrolledCoursesByUserId(this.service.loggedInUserId)
        .subscribe({
          next: (res) => {
            this.enrolledCourses = res as number[];

            // go through the enrolledCourses array and get the details of each course
            this.enrolledCourses.forEach((courseId: number) => {
              this.service.getCourse(courseId).subscribe({
                next: (res) => {
                  this.enrolledCoursesDetails.push(res.body as unknown as Course);
                },
                error: (err) => {
                  console.log(err);
                },
              });
            });
            console.log(this.enrolledCoursesDetails);
          },
          error: (err) => {
            console.log(err);
          },
        });
    } else {
      // redirect to login page
      this.router.navigate(['user/login']);
    }
  }
}
