import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPostService } from '../../shared/blog-post.service';
import { BlogPost } from '../../shared/blog-post.model';

@Component({
  selector: 'app-blog-post-create',
  templateUrl: './blog-post-create.component.html',
  styles: ``
})
export class BlogPostCreateComponent implements OnInit {
  blogPostForm: FormGroup = new FormGroup({
    'blogPostTitle': new FormControl(null, Validators.required),
    'blogPostContent': new FormControl(null, Validators.required),
    'blogPostImage': new FormControl(null, Validators.required)
  });

  constructor(
    private blogPostService: BlogPostService, 
    private router: Router
  ) {}

  ngOnInit() {}

  onSubmit() {
    if (this.blogPostForm.valid) {
      const formValues = this.blogPostForm.value;
      const newPost = {
        blogPostTitle: formValues.blogPostTitle || '',
        blogPostContent: formValues.blogPostContent || '',
        blogPostImage: formValues.blogPostImage || '',
      };

      this.blogPostService.createPost(newPost).subscribe( () => {
        this.router.navigate(['/blogpost']);
      });
    }
  }
}
