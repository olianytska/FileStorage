import { Component, OnInit } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-show-acc',
  templateUrl: './show-acc.component.html',
  styleUrls: ['./show-acc.component.css']
})
export class ShowAccComponent implements OnInit {

  constructor(private service:SharedService) { }

  AccountList:any=[];
  ModalTitle:string="";
  ActivateRegistrationComp:boolean=false;
  ActivateLoginComp:boolean=false;
  reg:any;
  log:any;

  ngOnInit(): void {
    this.refreshAccountList()
  }

  addClick(){
    this.reg={
      Id:"",
      Name:"",
      Surname:"",
      Email:"",
      Password:"",
      ConfirmPassword:""
    }
    this.ModalTitle="Create user";
    this.ActivateRegistrationComp=true;
  }

  addLoginClick(){
    this.log={
      Email:"",
      Password:""
    }
    this.ModalTitle="Login";
    this.ActivateLoginComp=true;
  }

  closeLoginClick(){
    this.ActivateLoginComp=false;
  }
  
  closeClick(){
    this.ActivateRegistrationComp=false;
    this.refreshAccountList();
  }

  deleteClick(item:any){
    if(confirm('Are you sure?')){
      this.service.deleteAccount(item.Id).subscribe(data=>{
        alert(data.toString()); 
        this.refreshAccountList(); 
      })
    }
    
  }

  refreshAccountList(){
    this.service.getAccList().subscribe(data=>
      this.AccountList = data
    );
  }

}
