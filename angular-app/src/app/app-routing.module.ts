import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './register/register.component';
import { LogInComponent } from './log-in/log-in.component';


const routes : Routes = [
  {path:"log-in", component: LogInComponent},
  {path:"register", component: RegisterComponent},

  // {path: "", component: HomeComponent, pathMatch: "full"},
  // {path: "**", redirectTo: "home"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
