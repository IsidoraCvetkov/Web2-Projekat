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
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './auth/jwt-interceptor';

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
    BusLocationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
