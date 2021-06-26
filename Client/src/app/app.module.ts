import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidebarComponent } from './modules/components/sidebar/sidebar.component';
import { NavbarComponent } from './modules/components/navbar/navbar.component';
import { LoginComponent } from './modules/login/login.component';
import { DashboardComponent } from './modules/dashboard/dashboard.component';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, CommonModule],
  providers: [
    NavbarComponent,
    LoginComponent,
    DashboardComponent,
    SidebarComponent,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
