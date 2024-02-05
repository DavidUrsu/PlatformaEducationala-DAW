import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../../shared/blog-post.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPost } from '../../shared/blog-post.model';

@Component({
  selector: 'app-blog-post-details',
  templateUrl: './blog-post-details.component.html',
  styles: ``,
})
export class BlogPostDetailsComponent implements OnInit {
  blogPost: BlogPost = {} as BlogPost;

  constructor(
    private route: ActivatedRoute,
    private blogPostService: BlogPostService,
    private router: Router,
    public service: BlogPostService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    const numericId = Number(id); // Convert id to number using the Number function

    // If the id is not a number, redirect to the blog post list
    if (isNaN(numericId)) {
      this.router.navigate(['/blogpost']);
    } else {
      this.blogPostService.getPost(numericId).subscribe(
        (response) => {
          if (response.status === 200) {
            this.blogPost = response.body as BlogPost; // Type assertion to cast response body to BlogPost
          }
        },
        error => {
          console.log(error);
          this.router.navigate(['/blogpost']);
        }
      );
    }
  }
}
