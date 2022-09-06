import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BodyContainerComponent } from './body-container/body-container.component';
import { AccountModule } from '../account/account.module';

@NgModule({
  declarations: [NavBarComponent, BodyContainerComponent],
  imports: [CommonModule, AccountModule],
  exports: [NavBarComponent, BodyContainerComponent],
})
export class MainComponentsModule {}
