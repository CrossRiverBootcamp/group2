import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BodyContainerComponent } from './body-container/body-container.component';
import { AccountModule } from '../account/account.module';
import { MyFooterComponent } from './my-footer/my-footer.component';
import { HomeComponent } from './home/home.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
];

@NgModule({
  declarations: [
    NavBarComponent,
    BodyContainerComponent,
    MyFooterComponent,
    HomeComponent,
  ],
  imports: [CommonModule, AccountModule, RouterModule.forChild(routes)],
  exports: [NavBarComponent, BodyContainerComponent],
})
export class MainComponentsModule {}
