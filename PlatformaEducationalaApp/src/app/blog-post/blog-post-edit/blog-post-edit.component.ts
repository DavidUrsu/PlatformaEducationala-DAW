import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPostService } from '../../shared/blog-post.service';
import { BlogPost } from '../../shared/blog-post.model';

@Component({
  selector: 'app-blog-post-edit',
  templateUrl: './blog-post-edit.component.html',
  styles: ``,
})
export class BlogPostEditComponent implements OnInit {
  blogPost: BlogPost = {} as BlogPost;

  blogPostForm = new FormGroup({
    blogPostId: new FormControl(''),
    blogPostTitle: new FormControl(''),
    blogPostContent: new FormControl(''),
    blogPostImage: new FormControl(''),
  });

  constructor(
    private route: ActivatedRoute,
    private blogPostService: BlogPostService,
    private router: Router
  ) {}

  ngOnInit() {
    const postId = this.route.snapshot.paramMap.get('id');
    const numericId = Number(postId); // Convert id to number using the Number function

    // If the id is not a number, redirect to the blog post list
    if (isNaN(numericId)) {
      this.router.navigate(['/blogpost']);
    } else {
      this.blogPostService.getPost(numericId).subscribe(
        (response) => {
          const post = response.body as BlogPost; // Type assertion to cast response body to BlogPost
          this.blogPost = post;

          this.blogPostForm.setValue({
            blogPostId: post.blogPostId.toString(), // Convert the number to a string
            blogPostTitle: post.title,
            blogPostContent: post.content,
            blogPostImage: post.imageUrl,
          });
        },
        (error) => {
          console.log(error);
          this.router.navigate(['/blogpost']);
        }
      );
    }
  }

  onSubmit() {
    const formValues = this.blogPostForm.value;
    const updatedPost = {
      id: Number(this.route.snapshot.paramMap.get('id')), // Convert id to number using the Number function
      blogPostTitle: formValues.blogPostTitle || '', // Ensure blogPostTitle is always a string
      blogPostContent: formValues.blogPostContent || '',
      blogPostImage: formValues.blogPostImage || ''
    };

    this.blogPostService.updatePost(updatedPost).subscribe(() => {
      this.router.navigate(['/blogpost/' + this.blogPostForm.value.blogPostId]);
    });
  }
}
