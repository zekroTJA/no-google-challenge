import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginRouteComponent } from './routes/login-route/login-route.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginRouteComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
