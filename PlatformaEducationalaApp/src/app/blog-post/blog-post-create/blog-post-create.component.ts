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
    title: new FormControl(null, Validators.required),
    content: new FormControl(null, Validators.required),
    imageUrl: new FormControl(null, Validators.required)
  });

  constructor(
    private blogPostService: BlogPostService, 
    private router: Router
  ) {}

  ngOnInit() {}

  onSubmit() {
    if (this.blogPostForm.valid) {
      this.blogPostService.createPost(this.blogPostForm.value).subscribe({
        next: res => {
          this.blogPostService.refreshList();
          this.router.navigate(['/blogpost']);
        },
        error: err => {
          console.log(err);
        }
      });
    }
  }
}
