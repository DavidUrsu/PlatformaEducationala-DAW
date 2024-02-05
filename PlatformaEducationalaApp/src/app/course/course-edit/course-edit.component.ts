import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../shared/course.service';
import { Course } from '../../shared/course.model';

@Component({
  selector: 'app-course-edit',
  templateUrl: './course-edit.component.html',
  styles: ``
})
export class CourseEditComponent {
  course: Course = {} as Course;

  courseForm = new FormGroup({
    courseId: new FormControl(''),
    courseName: new FormControl(''),
    courseDescription: new FormControl(''),
    coursePrice: new FormControl(''),
    courseSalePrice: new FormControl(''),
    courseImage: new FormControl(''),
  });

  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService,
    private router: Router
  ) {}

  ngOnInit() {
    const courseId = this.route.snapshot.paramMap.get('id');
    const numericId = Number(courseId); // Convert id to number using the Number function

    // If the id is not a number, redirect to the course list
    if (isNaN(numericId)) {
      this.router.navigate(['/course']);
    } else {
      this.courseService.getCourse(numericId).subscribe(
        (response) => {
          const course = response.body as Course; // Type assertion to cast response body to Course
          this.course = course;

          //check if the user is the owner of the course
          if (this.course.professorUserId != this.courseService.loggedInUserId) {
            this.router.navigate(['/course']);
          }

          this.courseForm.setValue({
            courseId: course.courseId.toString(), // Convert the number to a string
            courseName: course.courseName,
            courseDescription: course.courseDescription,
            coursePrice: course.coursePrice.toString(),
            courseSalePrice: course.courseSalePrice.toString(),
            courseImage: course.courseImage,
          });
        },
        (error) => {
          console.log(error);
          this.router.navigate(['/course']);
        }
      );
    }
  }

  onSubmit() {
    if (this.courseForm.valid) {
      const updatedCourse: Course = {
        courseId: Number(this.courseForm.value.courseId),
        courseName: this.courseForm.value.courseName ?? '',
        courseDescription: this.courseForm.value.courseDescription ?? '',
        coursePrice: Number(this.courseForm.value.coursePrice),
        courseSalePrice: Number(this.courseForm.value.courseSalePrice),
        courseImage: this.courseForm.value.courseImage ?? '',
        professorUserId: this.course.professorUserId,
      };

      this.courseService.updateCourse(updatedCourse).subscribe({
        next: (res) => {
          this.router.navigate(['/course']);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
}
