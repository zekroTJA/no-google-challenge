import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import RestAPIService from './services/restapi.service';
import { LoginRouteComponent } from './routes/login-route/login-route.component';
import { FormsModule } from '@angular/forms';
import { SnackBarComponent } from './components/snack-bar/snack-bar.component';
import { ListsRouteComponent } from './routes/lists-route/lists-route.component';
import { EditTileComponent } from './components/edit-tile/edit-tile.component';
import { ListRouteComponent } from './routes/list-route/list-route.component';
import { SkeletonTileComponent } from './components/skeleton-tile/skeleton-tile.component';
import { environment } from 'src/environments/environment';
import { MockAPIService } from './services/mockapi.service';
import { HoverNavigationComponent } from './components/hover-navigation/hover-navigation.component';
import { UserRouteComponent } from './routes/user-route/user-route.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginRouteComponent,
    SnackBarComponent,
    ListsRouteComponent,
    EditTileComponent,
    ListRouteComponent,
    SkeletonTileComponent,
    HoverNavigationComponent,
    UserRouteComponent,
  ],
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
      useClass: environment.production ? RestAPIService : MockAPIService,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
