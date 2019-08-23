import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public isUnregistrated: boolean;
  public isRegistrated: boolean;
  public isAdmin: boolean;
  public isController: boolean;

  constructor() { }

  ngOnInit() {
    this.isUnregistrated=this.isRegistrated=this.isAdmin=this.isController=false;
    if(localStorage.role == "Admin"){
      this.isAdmin=true;
    }else if(localStorage.role=="AppUser"){
      this.isRegistrated=true;
    }else if(localStorage.role=="Controller"){
      this.isController=true;
    }else{
      this.isUnregistrated=true;
    }
  }

}
