import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { RegistrationComponent } from '../../Scripts/App/registration/registration.component';  
import { AppComponent } from './app.component';

@NgModule({
    imports: [
        //angular builtin module
        BrowserModule,
        //HttpModule,
        FormsModule,
        ReactiveFormsModule
    ],
    declarations: [
        AppComponent,
        RegistrationComponent
    ],
    exports: [RegistrationComponent],
    providers: [
        //register services here
    ],
    bootstrap: [
        AppComponent,
        RegistrationComponent
    ]
})

export class AppModule {
}

