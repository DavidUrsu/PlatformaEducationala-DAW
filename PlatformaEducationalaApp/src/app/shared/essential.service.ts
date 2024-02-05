import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EssentialService {
  constructor() { }

  getCookie(name: string): string {
    let cookieArr = document.cookie.split(";");
  
    for(let i = 0; i < cookieArr.length; i++) {
      let cookiePair = cookieArr[i].split("=");
  
      if(name == cookiePair[0].trim()) {
        return cookiePair[1];
      }
    }
  
    return '';
  }
  
  isUserLoggedIn(): boolean {
    return this.getCookie('id') ? true : false;
  }
}
