import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../services/Account.service';
import { AccoutContainerComponent } from './accout-container/accout-container.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, AccoutContainerComponent],
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  providers: [AccountService],
  exports: [LoginComponent, RegisterComponent],
})
export class AccountModule {}
