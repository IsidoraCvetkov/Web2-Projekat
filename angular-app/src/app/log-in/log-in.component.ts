import { Component, OnInit } from '@angular/core';
import { Validators, ReactiveFormsModule} from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

import { FormGroup, NgControl } from '@angular/forms';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  message: string;

  logInForm = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });
  constructor(public authService: AuthService, public router: Router, private fb: FormBuilder) {
    this.setMessage();
  }

  setMessage() {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');
  }

  logIn() {
    this.authService.login(this.logInForm.value).subscribe((data) => {
      console.log(data);
      this.setMessage();
    });
  }

  logout() {
    this.authService.logout();
    this.setMessage();
  }

  ngOnInit() {
  }
}
