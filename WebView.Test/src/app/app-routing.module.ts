import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './components/layout/layout.component';
import { HomeComponent } from './components/home/home.component';
import { AssetComponent } from './components/asset/asset.component';
import { AssetDetailComponent } from './components/asset/asset-detail/asset-detail.component';
import { AssetEditComponent } from './components/asset/asset-edit/asset-edit.component';
import { AssetCreateComponent } from './components/asset/asset-create/asset-create.component';

const mainRoutes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  {
    path: 'asset', children: [
      { path: '', component: AssetComponent },
      { path: 'create', component: AssetCreateComponent },
      {
        path: 'detail', children: [
          { path: '', redirectTo: '/asset', pathMatch: 'full' },
          { path: ':id', component: AssetDetailComponent }
        ]
      },
      {
        path: 'edit', children: [
          { path: '', redirectTo: '/asset', pathMatch: 'full' },
          { path: ':id', component: AssetEditComponent }
        ]
      }
    ]
  }
];

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: mainRoutes
  },
  { path: '**', redirectTo: '' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
