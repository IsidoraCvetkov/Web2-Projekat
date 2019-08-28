import { Component, OnInit } from '@angular/core';
import { RegistrateUser } from '../register/registration-user';
import { Ticket } from '../models/price';
import { Validators, FormBuilder } from '@angular/forms';
import { PriceService } from '../services/priceService';
import { ProfileService } from '../services/profileService';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {

  public isUnregistrated: boolean;
  public isRegistrated: boolean;
  public isAdmin: boolean;
  public isController: boolean;

  public isPriceDataLoaded: boolean;
  public isOneHour: boolean;

  message1:string;
  message:string;
  selectedTicket;
  selectedUser;
  email;
  isLogged:boolean;
  prices:number;
  oneHour:number;
  tickets : Ticket[];
  t:Ticket;

  user:RegistrateUser;
  
  dat:Date;
  ticket:Ticket={IDticket:1, BoughtTime:this.dat,CheckIn:this.dat,TypeOfTicket:1,UserName:"guest"};

  role;

  emailForm = this.fb.group({
    email: ['', Validators.email],
  });


  constructor(public priceService: PriceService,private fb: FormBuilder, public profileService : ProfileService) {
    this.isPriceDataLoaded=false;
    this.isOneHour=false;
    if(localStorage.email!=undefined){
    this.isLogged=true;
    this.getUser();
    this.showTickets();
    }else{
      this.isLogged=false;
    }
    this.message="";
    this.message1="";
   }

  ngOnInit() {
    this.isUnregistrated=this.isRegistrated=this.isAdmin=this.isController=false;
    if(localStorage.role == "Admin"){
      this.isAdmin=true;
    }else if(localStorage.role == "AppUser"){
      this.isRegistrated=true;
    }else if(localStorage.role == "Controller"){
      this.isController=true;
    }else{
      this.isUnregistrated=true;
    }
  }

  showTickets():void{
    this.priceService.showAllTickets().subscribe(tickets=>{
     this.tickets = tickets;
    }) 
   }
   
   getUser(){
     this.profileService.showProfile(localStorage.email).subscribe(regUser=>{
       this.user=regUser;
     });
   }
}
