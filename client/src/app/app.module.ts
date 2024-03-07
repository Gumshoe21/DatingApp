import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  // declares any components that our app might need to load
  declarations: [AppComponent],
  // imports modules for Angular or our own even
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, BrowserAnimationsModule],
  providers: [],
  // bootstrapping the AppComponent
  bootstrap: [AppComponent],
})
export class AppModule {}
