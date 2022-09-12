import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from 'src/app/services/Account.service';
import { AccoutContainerComponent } from './accout-container/accout-container.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import localeHe from '@angular/common/locales/he';

registerLocaleData(localeHe);

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    AccoutContainerComponent,
    AccountInfoComponent,
  ],
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  providers: [AccountService, { provide: LOCALE_ID, useValue: 'he_IL' }],
  exports: [AccoutContainerComponent, AccountInfoComponent],
})
export class AccountModule {}
