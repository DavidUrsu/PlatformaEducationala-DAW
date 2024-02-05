import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from '../../environments/environment.development';
import { EssentialService } from './essential.service';
import { User } from './user.model';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  url:string = environment.apiBaseUrl + '/User';
  // get the userid from the cookie

  constructor(
    private http: HttpClient,
    public essentialService: EssentialService
    ) { 
    }

  getLoggedUserDetails() {
    return this.http.get(`${this.url}`, { withCredentials: true });
  }

  deleteUser() {
    return this.http.delete(`${this.url}`, { withCredentials: true });
  }

  logout() {
    return this.http.post(`${this.url}/logout`, null, { withCredentials: true, responseType: 'text' });
  }

  changeEmail(newEmail: string) {
    const params = new HttpParams()
      .set('newEmail', newEmail);
    return this.http.put(`${this.url}/changeEmail?${params.toString()}`, null, { withCredentials: true });
  }

  register(username_: string,  password_: string, email_: string) {
    const params = new HttpParams()
      .set('username', username_)
      .set('password', password_)
      .set('email', email_);
  
    return this.http.post(`${this.url}/register?${params.toString()}`, null, { withCredentials: true });
  }

  login (username_: string, password_: string) {
    const params = new HttpParams()
      .set('username', username_)
      .set('password', password_);
  
    return this.http.post(`${this.url}/login?${params.toString()}`, null, { withCredentials: true });
  }
}
