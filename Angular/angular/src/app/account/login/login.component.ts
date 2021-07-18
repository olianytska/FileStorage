import { Component, OnInit, Input } from '@angular/core';
import {SharedService} from 'src/app/shared.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() log:any;
  Email:string="";
  Password:string="";

  ngOnInit(): void {
    this.Email = this.log.Email,
    this.Password = this.log.Password
  }

  loginAccount(){
    var val = {
      Email:this.Email,
      Password:this.Password
    };
    this.service.loginAccount(val).subscribe(res=>{
      alert(res.toString());
    });
  }

}