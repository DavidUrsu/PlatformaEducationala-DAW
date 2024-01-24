import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../../shared/blog-post.service';


@Component({
  selector: 'app-blog-post-list',
  templateUrl: './blog-post-list.component.html',
  styles: ``
})
export class BlogPostListComponent implements OnInit {
  constructor(public service: BlogPostService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }
}
