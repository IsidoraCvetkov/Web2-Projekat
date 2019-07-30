import { Component, OnInit } from '@angular/core';
import { Validators, ReactiveFormsModule} from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { FormGroup, NgControl } from '@angular/forms';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  logInForm = this.fb.group({
    Email: ['', Validators.required],
    Password: ['', Validators.required]
  });
  constructor(public fb: FormBuilder) { }

  ngOnInit() {
  }

  login(){

  }
}
