import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from '../../environments/environment.development';
import { BlogPost } from './blog-post.model';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  url:string = environment.apiBaseUrl + '/BlogPost';
  list: BlogPost[] = [];
  constructor(private http: HttpClient) { }

  refreshList() {
    this.http.get(this.url)
    .subscribe({
      next: res=>{
        this.list = res as BlogPost[];
      },
      error: err=>{console.log(err)}
    })
  }

  getPost(id: number) {
    return this.http.get(`${this.url}/${id}`, { observe: 'response' });
  }

  updatePost(post: {id: number, blogPostTitle: string, blogPostContent: string, blogPostImage: string}) {
    let params = new HttpParams()
      .set('blogPostTitle', post.blogPostTitle)
      .set('blogPostContent', post.blogPostContent)
      .set('blogPostImage', post.blogPostImage);

    return this.http.put(`${this.url}/edit/${post.id}`, {}, { params, withCredentials: true }).pipe(
      catchError((error: any) => {
        console.log(error.error.text);
        return throwError(error);
      })
    );
  }

  createPost(post: {blogPostTitle: string, blogPostContent: string, blogPostImage: string}) {
    let params = new HttpParams()
      .set('blogPostTitle', post.blogPostTitle)
      .set('blogPostContent', post.blogPostContent)
      .set('blogPostImage', post.blogPostImage);
  
    return this.http.post(`${this.url}`, {}, { params, withCredentials: true }).pipe(
      catchError((error: any) => {
        console.log(error.error.text);
        return throwError(error);
      })
    );
  }
}
