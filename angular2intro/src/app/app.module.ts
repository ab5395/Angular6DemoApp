import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { SimpleComponent } from './simple/simple.component';
import {RouterModule,Routes} from '@angular/router';

// const appRoutes: Routes = [
//   { path: 'app', component: AppComponent },
//   { path: 'app-simple', component: SimpleComponent }
// ];

@NgModule({
  declarations: [
    AppComponent,
    SimpleComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path:'app-simple',component:SimpleComponent},
      {path:'**',component:SimpleComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
