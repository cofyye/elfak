import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdvertListComponent } from './advert-list/advert-list.component';
import { AdvertSingleComponent } from './advert-single/advert-single.component';
import { IndexComponent } from './index.component';
import { advertSingleResolver } from './resolvers/advert-single.resolver';
import { advertAllResolver } from './resolvers/advert-all.resolver';
import { advertCommentsResolver } from './resolvers/advert-comments-resolver';
const routes: Routes = [
  {
    path: '',
    component: IndexComponent,
    children: [
      {
        path: '',
        component: AdvertListComponent,
        pathMatch: 'full',
        resolve: { adverts: advertAllResolver },
      },
      {
        path: ':advert_id',
        component: AdvertSingleComponent,
        pathMatch: 'full',
        resolve: {
          data: advertSingleResolver,
          comments: advertCommentsResolver,
        },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdvertRoutingModule {}
