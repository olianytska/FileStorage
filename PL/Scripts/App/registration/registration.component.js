"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var user_service_1 = require("../user/user.service");
var RegistrationComponent = /** @class */ (function () {
    function RegistrationComponent(formBuilder, _userService) {
        this.formBuilder = formBuilder;
        this._userService = _userService;
        this.CreateForm();
    }
    RegistrationComponent.prototype.CreateForm = function () {
        this.userForm = this.formBuilder.group({
            'Name': ['', forms_1.Validators.required],
            'Surname': ['', forms_1.Validators.required],
            'Email': ['', [forms_1.Validators.required]],
            'Password': ['', [forms_1.Validators.required]],
            'ConfirmPassword': ['', [forms_1.Validators.required]]
        });
    };
    RegistrationComponent.prototype.saveUser = function () {
        if (this.userForm.dirty && this.userForm.valid) {
            this.user = {
                Name: this.userForm.value.Name,
                Surname: this.userForm.value.Surname,
                Email: this.userForm.value.Email,
                Password: this.userForm.value.Password,
                ConfirmPassword: this.userForm.value.ConfirmPassword
            };
            console.log(this.user);
            this._userService.InsertUser("/Account/Register", this.user).subscribe(function (data) {
                if (data == "ok") {
                    console.log('success');
                }
            });
        }
    };
    RegistrationComponent = __decorate([
        core_1.Component({
            selector: 'app-registration',
            templateUrl: './registration.component.html',
            styles: ['p {font-family: Lato;} '],
            providers: [user_service_1.UserService],
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, user_service_1.UserService])
    ], RegistrationComponent);
    return RegistrationComponent;
}());
exports.RegistrationComponent = RegistrationComponent;
//# sourceMappingURL=registration.component.js.map