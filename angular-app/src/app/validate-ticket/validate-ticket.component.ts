import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { ValidateTicketService } from '../services/validateTicketService';

@Component({
  selector: 'app-validate-ticket',
  templateUrl: './validate-ticket.component.html',
  styleUrls: ['./validate-ticket.component.css']
})
export class ValidateTicketComponent implements OnInit {

  message:string;
  validateTicket = this.fb.group({
    idTicket: ['', Validators.required],
  });


  constructor(public validateTicketService: ValidateTicketService, private fb: FormBuilder) { 

   this.message="";
  }

  ngOnInit() {
  }

  isValid():void{
  if(this.validateTicket.value.idTicket==null){
    this.message="Invalid input";
  }else{
this.validateTicketService.getInfo(this.validateTicket.value.idTicket).subscribe(data=>{
  this.message=data;
  });
  }
}

}
