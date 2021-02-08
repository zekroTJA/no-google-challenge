import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListRouteComponent } from './routes/list-route/list-route.component';
import { ListsRouteComponent } from './routes/lists-route/lists-route.component';
import { LoginRouteComponent } from './routes/login-route/login-route.component';
import { UserRouteComponent } from './routes/user-route/user-route.component';

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
    path: 'lists/:id',
    component: ListRouteComponent,
  },
  {
    path: 'profile',
    component: UserRouteComponent,
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
