import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { MatMenuModule } from '@angular/material/menu';
import { FormsModule } from '@angular/forms';
import { AccountModule } from '../account/account.module';
import { AccountInfoComponent } from './account-info/account-info.component';

@NgModule({
  declarations: [NavBarComponent, AccountInfoComponent],
  imports: [CommonModule, MatMenuModule, FormsModule],
  exports: [NavBarComponent],
})
export class SharedModule {}
