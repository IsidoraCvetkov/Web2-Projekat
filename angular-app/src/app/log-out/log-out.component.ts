import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-log-out',
  templateUrl: './log-out.component.html',
  styleUrls: ['./log-out.component.css']
})
export class LogOutComponent implements OnInit {

  constructor(private route:Router) { 

    // localStorage.removeItem('jwt');
    // localStorage.removeItem('role');
    // localStorage.removeItem('email');
    // localStorage.removeItem('pass');

    // location.reload();
    this.route.navigate(['/home']);
  }

  ngOnInit() {

  }
}
