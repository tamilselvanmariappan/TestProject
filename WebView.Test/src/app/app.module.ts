import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutComponent } from './components/layout/layout.component';
import { HomeComponent } from './components/home/home.component';
import { AssetComponent } from './components/asset/asset.component';
import { AssetDetailComponent } from './components/asset/asset-detail/asset-detail.component';
import { environment } from 'src/environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { AssetEditComponent } from './components/asset/asset-edit/asset-edit.component';
import { AssetCreateComponent } from './components/asset/asset-create/asset-create.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    HomeComponent,
    AssetComponent,
    AssetDetailComponent,
    AssetEditComponent,
    AssetCreateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [{
    provide: 'API_URL', useValue: environment.apiUrl
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
