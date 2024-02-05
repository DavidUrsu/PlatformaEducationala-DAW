import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../shared/course.service';
import { Router } from '@angular/router';
import { EssentialService } from '../../shared/essential.service';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styles: ``,
})
export class CourseListComponent implements OnInit{
  constructor(
    public service: CourseService,
    private router: Router,
    public essentialService: EssentialService
  ) {}

  enrolledCourses: any = [];

  ngOnInit(): void {
    this.service.refreshList();

    //check if the user is logged in
    if (this.service.loggedInUserId) {
      this.service
        .getEnrolledCoursesByUserId(this.service.loggedInUserId)
        .subscribe({
          next: (res) => {
            this.enrolledCourses = res as number[];
          },
          error: (err) => {
            console.log(err);
          },
        });
    }
  }

  editCourse(courseId: number) {
    this.router.navigate([`course/edit/${courseId}`]);
  }

  enrollCourse(courseId: number) {
    this.service.enrollCourse(courseId);
    // Add the courseId to the enrolledCourses array
    this.enrolledCourses.push(courseId);
  }

  unenrollCourse(courseId: number) {
    this.service.unenrollCourse(courseId);
    // Remove the courseId from the enrolledCourses array
    const index = this.enrolledCourses.indexOf(courseId);
    if (index > -1) {
      this.enrolledCourses.splice(index, 1);
    }
  }
}
