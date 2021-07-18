import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {AccountComponent} from './account/account.component';
import { FileUploadComponent } from './file-upload/file-upload.component';

const routes: Routes = [
{path:'account', component: AccountComponent},
{path: 'files', component: FileUploadComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }