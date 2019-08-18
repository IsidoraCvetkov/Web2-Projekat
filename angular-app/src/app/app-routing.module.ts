import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './register/register.component';
import { LogInComponent } from './log-in/log-in.component';
import { HomeComponent } from './home/home.component';
import { BusLocationComponent } from './bus-location/bus-location.component';
import { PriceListComponent } from './price-list/price-list.component';
import { ShceduleComponent } from './shcedule/shcedule.component';
import { TicketComponent } from './ticket/ticket.component';
import { LineMeshComponent } from './line-mesh/line-mesh.component';
import { LogOutComponent } from './log-out/log-out.component';
import { ProfileViewComponent } from './profile-view/profile-view.component';
import { ValidateProfileComponent } from './validate-profile/validate-profile.component';
import { ValidateTicketComponent } from './validate-ticket/validate-ticket.component';
import { AdminLineMeshComponent } from './admin-line-mesh/admin-line-mesh.component';
import { AdminScheduleComponent } from './admin-schedule/admin-schedule.component';
import { AdminPriceListComponent } from './admin-price-list/admin-price-list.component';

const routes : Routes = [
  {path:"login", component: LogInComponent},
  {path:"register", component: RegisterComponent},
  {path:"home", component: HomeComponent},
  {path:"bus-location", component: BusLocationComponent},
  {path:"price-list", component: PriceListComponent},
  {path:"schedule", component: ShceduleComponent},
  {path:"ticket", component: TicketComponent},
  {path:"line-mesh", component : LineMeshComponent},
  {path:"log-out", component : LogOutComponent},
  {path:"profile", component : ProfileViewComponent},
  {path:"validate-profile", component : ValidateProfileComponent},
  {path:"validate-ticket", component : ValidateTicketComponent},
  {path:"admin-line-mesh", component : AdminLineMeshComponent},
  {path:"admin-schedule", component : AdminScheduleComponent},
  {path:"admin-price-list", component : AdminPriceListComponent}

  // {path: "", component: HomeComponent, pathMatch: "full"},
  // {path: "**", redirectTo: "home"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
