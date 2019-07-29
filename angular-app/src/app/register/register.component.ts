import { Component, OnInit } from '@angular/core';
import { Validators} from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(public router: Router) { }

  ngOnInit() {
    
  }
  register(){
    this.router.navigate(["/log-in"]);
  }
}
