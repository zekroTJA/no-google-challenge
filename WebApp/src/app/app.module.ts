import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import RestAPIService from './services/restapi.service';
import { LoginRouteComponent } from './routes/login-route/login-route.component';
import { FormsModule } from '@angular/forms';
import { SnackBarComponent } from './components/snack-bar/snack-bar.component';

@NgModule({
  declarations: [AppComponent, LoginRouteComponent, SnackBarComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [
    {
      provide: 'APIService',
      useClass: RestAPIService,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
