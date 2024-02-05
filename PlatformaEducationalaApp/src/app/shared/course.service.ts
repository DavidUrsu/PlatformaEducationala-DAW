import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from '../../environments/environment.development';
import { Course } from './course.model';
import { EssentialService } from './essential.service';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  url:string = environment.apiBaseUrl + '/Course';
  list: Course[] = [];
  // get the userid from the cookie
  loggedInUserId: number = 0;

  constructor(
    private http: HttpClient,
    public essentialService: EssentialService
    ) { 
      this.loggedInUserId = essentialService.getCookie('id') ? Number(essentialService.getCookie('id')) : 0;
    }

  refreshList() {
    this.http.get(this.url)
    .subscribe({
      next: res=>{
        this.list = res as Course[];
      },
      error: err=>{console.log(err)}
    })
  }

  createCourse(course: Course) {
    course.courseSalePrice = course.coursePrice;
    return this.http.post(`${this.url}`, course, { withCredentials: true });
  }

  updateCourse(course: Course) {
    console.log(course);
    return this.http.put(`${this.url}`, course, { withCredentials: true });
  }

  getEnrolledCoursesByUserId(userId: number) {
    return this.http.get(`${this.url}/GetEnrolledCoursesByUserId/${userId}`, { withCredentials: true });
  }

  deleteCourse(id: number) {
    return this.http.delete(`${this.url}/${id}`, { withCredentials: true });
  }

  enrollCourse(courseId: number) {
    return this.http.post(`${this.url}/${courseId}/enroll`, null, { withCredentials: true }).subscribe({
    });;
  }

  unenrollCourse(courseId: number) {
    return this.http.post(`${this.url}/${courseId}/unenroll`, null, { withCredentials: true }).subscribe({
    });
  }

  getCourse(id: number) {
    return this.http.get(`${this.url}/${id}`, { observe: 'response', withCredentials: true });
  }
}
