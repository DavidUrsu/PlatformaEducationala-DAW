import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms'; // Import the ReactiveFormsModule

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { BlogPostListComponent } from './blog-post/blog-post-list/blog-post-list.component';
import { HttpClientModule } from '@angular/common/http';
import { BlogPostDetailsComponent } from './blog-post/blog-post-details/blog-post-details.component';
import { BlogPostEditComponent } from './blog-post/blog-post-edit/blog-post-edit.component';
import { BlogPostCreateComponent } from './blog-post/blog-post-create/blog-post-create.component';
import { CourseComponent } from './course/course.component';
import { CourseEditComponent } from './course/course-edit/course-edit.component';
import { CourseListComponent } from './course/course-list/course-list.component';
import { CourseCreateComponent } from './course/course-create/course-create.component';
import { UserComponent } from './user/user.component';
import { UserLoginComponent } from './user/user-login/user-login.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';
import { EnrollmentsComponent } from './course/enrollments/enrollments.component';

@NgModule({
  declarations: [
    AppComponent,
    BlogPostComponent,
    BlogPostListComponent,
    BlogPostDetailsComponent,
    BlogPostEditComponent,
    BlogPostCreateComponent,
    CourseComponent,
    CourseEditComponent,
    CourseListComponent,
    CourseCreateComponent,
    UserComponent,
    UserLoginComponent,
    UserRegisterComponent,
    EnrollmentsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule // Add the ReactiveFormsModule to the imports array
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
