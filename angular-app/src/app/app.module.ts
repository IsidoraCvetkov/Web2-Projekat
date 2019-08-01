import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './register/register.component';
import { LogInComponent } from './log-in/log-in.component';
import { HomeComponent } from './home/home.component';
import { LineMeshComponent } from './line-mesh/line-mesh.component';
import { PriceListComponent } from './price-list/price-list.component';
import { TicketComponent } from './ticket/ticket.component';
import { ShceduleComponent } from './shcedule/shcedule.component';
import { BusLocationComponent } from './bus-location/bus-location.component';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpHandler} from '@angular/common/http';
import { JwtInterceptor } from './auth/jwt-interceptor';
import { LogOutComponent } from './log-out/log-out.component';
import { ProfileViewComponent } from './profile-view/profile-view.component';
import { FormBuilder, FormsModule } from '@angular/forms';
import { Validators, ReactiveFormsModule} from '@angular/forms';
import { MapComponent } from './map/map.component';
import { UnregistredComponent } from './unregistred/unregistred.component';
import { ValidateProfileComponent } from './validate-profile/validate-profile.component';
import { ValidateTicketComponent } from './validate-ticket/validate-ticket.component';
import { AdminLineMeshComponent } from './admin-line-mesh/admin-line-mesh.component';
import { AdminScheduleComponent } from './admin-schedule/admin-schedule.component';
import { AdminPriceListComponent } from './admin-price-list/admin-price-list.component';
import { AdminStationComponent } from './admin-station/admin-station.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    LogInComponent,
    HomeComponent,
    LineMeshComponent,
    PriceListComponent,
    TicketComponent,
    ShceduleComponent,
    BusLocationComponent,
    LogOutComponent,
    ProfileViewComponent,
    MapComponent,
    UnregistredComponent,
    ValidateProfileComponent,
    ValidateTicketComponent,
    AdminLineMeshComponent,
    AdminScheduleComponent,
    AdminPriceListComponent,
    AdminStationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule, //dodala
    HttpClientModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}, FormBuilder],
  bootstrap: [AppComponent]
})
export class AppModule { }
