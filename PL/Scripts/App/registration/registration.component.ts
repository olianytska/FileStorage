import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../user/user.interface';
import { UserService } from '../user/user.service'; 

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styles: ['p {font-family: Lato;} '],
    providers: [UserService], 
})
export class RegistrationComponent {
    userForm: any;
    user: User;

    constructor(private formBuilder: FormBuilder, private _userService: UserService) {
        this.CreateForm();

    }
    CreateForm() {
        this.userForm = this.formBuilder.group({
            'Name': ['', Validators.required],
            'Surname': ['', Validators.required],
            'Email': ['', [Validators.required]],
            'Password': ['', [Validators.required]],
            'ConfirmPassword': ['', [Validators.required]]
        });
    }

    saveUser(): void {
        if (this.userForm.dirty && this.userForm.valid) {
            this.user = {
                Name: this.userForm.value.Name,
                Surname: this.userForm.value.Surname,
                Email: this.userForm.value.Email,
                Password: this.userForm.value.Password,
                ConfirmPassword: this.userForm.value.ConfirmPassword
            }
            console.log(this.user);
            this._userService.InsertUser("/Account/Register", this.user).subscribe((data) => {
                if (data == "ok") {
                    console.log('success');
                }
            });
        }
    }
}   