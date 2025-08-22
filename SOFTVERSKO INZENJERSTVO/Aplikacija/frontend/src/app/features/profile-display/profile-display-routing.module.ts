import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileDisplayComponent } from './profile-display/profile-display.component';
import { userProfileResolver } from './resolvers/profile-display.resolver';

const routes: Routes = [
  {
    path: ':expert_id',
    component: ProfileDisplayComponent,
    children: [
      {
        path: '',
        component: ProfileDisplayComponent,
        pathMatch: 'full',
      },
    ],
    resolve: { userProfile: userProfileResolver },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProfileDisplayRoutingModule {}
