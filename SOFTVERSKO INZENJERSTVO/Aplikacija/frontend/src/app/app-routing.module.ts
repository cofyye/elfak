import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { fetchUserGuard } from './shared/guards/fetch-user.guard';
import { HomeComponent } from './home/home.component';
import { notAuthenticatedGuard } from './shared/guards/not-authenticated.guard';
import { authenticatedGuard } from './shared/guards/authenticated.guard';
import { GradeSettingComponent } from './shared/components/grade-setting/grade-setting.component';
import { VideocallComponent } from './shared/components/videocall/videocall.component';
import { canGradeResolver } from './shared/resolvers/grade.resolver';
import { homeCarouselResolver } from './home/resolver/home.resolver';

const routes: Routes = [
  {
    path: 'video/:formId',
    component: VideocallComponent,
    pathMatch: 'full',
  },
  {
    path: 'grade/:adId',
    component: GradeSettingComponent,
    pathMatch: 'full',
    resolve: {
      canGrade: canGradeResolver,
    },
  },
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
    canActivate: [fetchUserGuard],
    resolve: { home: homeCarouselResolver },
  },
  {
    path: 'auth',
    canActivate: [fetchUserGuard],
    canActivateChild: [notAuthenticatedGuard],
    loadChildren: () =>
      import('./features/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'adverts',
    canActivate: [fetchUserGuard],
    loadChildren: () =>
      import('./features/advert/advert.module').then((m) => m.AdvertModule),
  },
  {
    path: 'panel/expert',
    canActivate: [fetchUserGuard],
    loadChildren: () =>
      import('./features/panel/expert/expert.module').then(
        (m) => m.ExpertModule
      ),
  },
  {
    path: 'panel/admin',
    canActivate: [fetchUserGuard],
    loadChildren: () =>
      import('./features/panel/admin/admin.module').then((m) => m.AdminModule),
  },
  {
    path: 'panel/client',
    canActivate: [fetchUserGuard],
    canActivateChild: [authenticatedGuard],
    loadChildren: () =>
      import('./features/panel/client/client.module').then(
        (m) => m.ClientModule
      ),
  },
  {
    path: 'profile', // dodati username dinamicki
    loadChildren: () =>
      import('./features/profile-display/profile-display.module').then(
        (m) => m.ProfileDisplayModule
      ),
  },
  {
    path: '**',
    redirectTo: '/',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
