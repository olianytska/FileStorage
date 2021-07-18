import { Component, OnInit, Input } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() reg:any;
  Id:string='';
  Name:string="";
  Surname:string="";
  Email:string="";
  Password:string="";
  ConfirmPassword:string="";

  ngOnInit(): void {
    this.Id = this.reg.Id,
    this.Name = this.reg.Name,
    this.Surname = this.reg.Surname,
    this.Email = this.reg.Email,
    this.Password = this.reg.Password,
    this.ConfirmPassword = this.reg.ConfirmPassword;
  }

  addAccount(){
    var val = {
      Id:this.Id,
      Name:this.Name,
      Surname:this.Surname,
      Email:this.Email,
      Password:this.Password,
      ConfirmPassword:this.ConfirmPassword
    };
    this.service.addAccount(val).subscribe(res=>{
      alert(res.toString());
    });
  }

}