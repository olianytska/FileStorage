import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl="https://localhost:44303/api";
readonly httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};
  constructor(private http:HttpClient) { }

  getAccList(): Observable<any[]>{
    return this.http.get<any>(this.APIUrl + "/account");
  }

  addAccount(val:any){
    return this.http.post(this.APIUrl + "/account", val);
  }

  deleteAccount(val:any){
    return this.http.delete(this.APIUrl + "/account/" + val);
  }

  loginAccount(val:any){
    return this.http.post(this.APIUrl + "/account/login", val);
  }

}
