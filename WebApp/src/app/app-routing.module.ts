import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListsRouteComponent } from './routes/lists-route/lists-route.component';
import { LoginRouteComponent } from './routes/login-route/login-route.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginRouteComponent,
  },
  {
    path: 'lists',
    component: ListsRouteComponent,
  },
  {
    path: '**',
    redirectTo: '/lists',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
