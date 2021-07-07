import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { RegistrationComponent } from './registration/registration.component';  
@NgModule({
    imports: [BrowserModule, FormsModule],
    declarations: [AppComponent, RegistrationComponent],
    bootstrap: [AppComponent, RegistrationComponent]
})
export class AppModule { }