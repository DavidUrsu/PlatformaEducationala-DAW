import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../shared/course.service';

@Component({
  selector: 'app-course-create',
  templateUrl: './course-create.component.html',
  styles: ``
})
export class CourseCreateComponent implements OnInit {
  courseForm: FormGroup = new FormGroup({
    courseName: new FormControl(null, Validators.required),
    courseDescription: new FormControl(null, Validators.required),
    coursePrice: new FormControl(null, Validators.required),
    courseImage: new FormControl(null, Validators.required)
  });
  
  constructor(
    private courseService: CourseService, 
    private router: Router
  ) {}

  ngOnInit() {}

  onSubmit() {
    if (this.courseForm.valid) {
      this.courseService.createCourse(this.courseForm.value).subscribe({
        next: res => {
          this.courseService.refreshList();
          this.router.navigate(['/course']);
        },
        error: err => {
          console.log(err);
        }
      });
    }
  }
}
