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
    title: new FormControl(''),
    content: new FormControl(''),
    imageUrl: new FormControl(''),
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

          //check if the user is the owner of the post
          if (this.blogPost.userId != this.blogPostService.loggedInUserId) {
            this.router.navigate(['/blogpost']);
          }

          this.blogPostForm.setValue({
            blogPostId: post.blogPostId.toString(), // Convert the number to a string
            title: post.title,
            content: post.content,
            imageUrl: post.imageUrl,
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
    if (this.blogPostForm.valid) {
      const updatedPost: BlogPost = {
        blogPostId: Number(this.blogPostForm.value.blogPostId),
        title: this.blogPostForm.value.title ?? '',
        content: this.blogPostForm.value.content ?? '',
        imageUrl: this.blogPostForm.value.imageUrl ?? '',
      };

      this.blogPostService.updatePost(updatedPost).subscribe(
        (response) => {
          this.router.navigate(['/blogpost']);
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  onDelete() {
    this.blogPostService.deleteBlogPost(this.blogPost.blogPostId).subscribe(
      (response) => {
        this.router.navigate(['/blogpost']);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
