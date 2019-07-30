import { Component, OnInit, NgModule } from '@angular/core';
import { Validators, ReactiveFormsModule} from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { FormGroup, NgControl } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

//dodala
@NgModule({
  imports:[
    FormBuilder,
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    FormGroup
  ], 
  declarations:[
    Validators
  ],
  exports:[
    Validators
  ]
})

export class RegisterComponent implements OnInit {
  fd:FormData;
  mySrc;
  canUpload:boolean;
  message:string;

  registerForm = this.fb.group({
    Email: ['', Validators.required],
    Password: ['', Validators.required],
    ConfirmPassword: ['', Validators.required],
    Name: ['', Validators.required],
    Surname: ['', Validators.required],
    Birthday:['',Validators.required],
    Address: ['', Validators.required],
    ImageUrl: [''],
    IDtypeOfUser:[],
  });

  constructor(public router: Router, public fb: FormBuilder) { }

  ngOnInit() {
    
  }
  register(){
    this.router.navigate(["/home"]);
  }
}
