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

@NgModule({
  declarations: [
    AppComponent,
    BlogPostComponent,
    BlogPostListComponent,
    BlogPostDetailsComponent,
    BlogPostEditComponent,
    BlogPostCreateComponent
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
