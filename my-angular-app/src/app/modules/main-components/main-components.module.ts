import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountModule } from '../account/account.module';
import { HomeComponent } from './home/home.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
];

@NgModule({
  declarations: [HomeComponent],
  imports: [CommonModule, AccountModule, RouterModule.forChild(routes)],
  exports: [],
})
export class MainComponentsModule {}
