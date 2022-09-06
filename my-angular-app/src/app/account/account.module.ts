import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { AccoutContainerComponent } from './accout-container/accout-container.component';
import { AccountInfoComponent } from './account-info/account-info.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    AccoutContainerComponent,
    AccountInfoComponent,
  ],
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  providers: [AccountService],
  exports: [AccoutContainerComponent, AccountInfoComponent],
})
export class AccountModule {}
