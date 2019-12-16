import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserData } from './user-data';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserDataServService {

  private _url:string = "https://localhost:44393/Users";
  constructor(private http:HttpClient){
  }

  getUserData():Observable<UserData[]>{
    return this.http.get<UserData[]>(this._url);
  }
}
