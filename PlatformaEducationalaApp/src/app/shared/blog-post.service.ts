import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { BlogPost } from './blog-post.model';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { EssentialService } from './essential.service';

@Injectable({
  providedIn: 'root',
})
export class BlogPostService {
  url: string = environment.apiBaseUrl + '/BlogPost';
  list: BlogPost[] = [];

  // get the userid from the cookie
  loggedInUserId: number = 0;

  constructor(
    private http: HttpClient,
    public essentialService: EssentialService
  ) {
    this.loggedInUserId = essentialService.getCookie('id')
      ? Number(essentialService.getCookie('id'))
      : 0;
  }

  refreshList() {
    this.http.get(this.url).subscribe({
      next: (res) => {
        this.list = res as BlogPost[];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getPost(id: number) {
    return this.http.get(`${this.url}/${id}`, { observe: 'response' });
  }

  updatePost(blogpost: BlogPost) {
    return this.http.put(`${this.url}`, blogpost, { withCredentials: true });
  }

  createPost(blogpost: BlogPost) {
    return this.http.post(`${this.url}`, blogpost, { withCredentials: true });
  }

  deleteBlogPost(id: number) {
    return this.http.delete(`${this.url}/${id}`, { withCredentials: true });
  }
}
