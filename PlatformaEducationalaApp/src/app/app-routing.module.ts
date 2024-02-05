import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { BlogPostDetailsComponent } from './blog-post/blog-post-details/blog-post-details.component';
import { BlogPostEditComponent } from './blog-post/blog-post-edit/blog-post-edit.component';
import { BlogPostCreateComponent } from './blog-post/blog-post-create/blog-post-create.component';
import { CourseComponent } from './course/course.component';
import { CourseCreateComponent } from './course/course-create/course-create.component';
import { CourseEditComponent } from './course/course-edit/course-edit.component';
import { UserComponent } from './user/user.component';
import { UserLoginComponent } from './user/user-login/user-login.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';
import { EnrollmentsComponent } from './course/enrollments/enrollments.component';

const routes: Routes = [
  { path: 'blogpost', component: BlogPostComponent },
  { path: 'blogpost/create', component: BlogPostCreateComponent },
  { path: 'blogpost/edit/:id', component: BlogPostEditComponent },
  { path: 'blogpost/:id', component: BlogPostDetailsComponent },

  { path: 'course', component: CourseComponent },
  { path: 'course/create', component: CourseCreateComponent },
  { path: 'course/edit/:id', component: CourseEditComponent },
  { path: 'course/my-enrollments', component: EnrollmentsComponent},

  { path: 'user', component: UserComponent },
  { path: 'user/login', component: UserLoginComponent },
  { path: 'user/register', component: UserRegisterComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
