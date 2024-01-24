import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { BlogPostDetailsComponent } from './blog-post/blog-post-details/blog-post-details.component';
import { BlogPostEditComponent } from './blog-post/blog-post-edit/blog-post-edit.component';
import { BlogPostCreateComponent } from './blog-post/blog-post-create/blog-post-create.component';

const routes: Routes = [
  { path: 'blogpost', component: BlogPostComponent },
  { path: 'blogpost/create', component: BlogPostCreateComponent },
  { path: 'blogpost/edit/:id', component: BlogPostEditComponent },
  { path: 'blogpost/:id', component: BlogPostDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
