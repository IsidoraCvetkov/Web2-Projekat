import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-log-out',
  templateUrl: './log-out.component.html',
  styleUrls: ['./log-out.component.css']
})
export class LogOutComponent implements OnInit {

  constructor(public route:Router) { 
    localStorage.removeItem('jwt');
    localStorage.removeItem('role');
    localStorage.removeItem('email');
    localStorage.removeItem('pass');

    //location.reload();
    window.location.href = '/home'; 
    //this.route.navigate(['/home']);
    //location.href = location.href;
    //location.reload();
    
  }

  ngOnInit() {

  }
}
