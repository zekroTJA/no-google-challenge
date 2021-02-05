import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import RestAPIService from './services/restapi.service';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, BrowserModule, HttpClientModule],
  providers: [
    {
      provide: 'APIService',
      useClass: RestAPIService,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
