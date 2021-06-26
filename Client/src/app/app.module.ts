// modules (keep alpahabetical)
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';

// components (keep alphabetical)
import { AppComponent } from './app.component';
import { LoginComponent } from './modules/login/login.component';
import { BaseComponent } from './modules/base/base.component';

@NgModule({
  declarations: [AppComponent, LoginComponent, BaseComponent],
  imports: [BrowserModule, FormsModule, FormsModule, HttpClientModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
