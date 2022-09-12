import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { RouterModule, Routes } from '@angular/router';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { HttpClientModule } from '@angular/common/http';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';

import { AccountModule } from '../account/account.module';

const routes: Routes = [
  {
    path: 'operations-history',
    component: OperationsHistoryComponent,
  },
];

@NgModule({
  declarations: [OperationsHistoryComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),

    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,

    MatPaginatorModule,
    MatTableModule,
    MatButtonModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatProgressBarModule,

    AccountModule,
  ],
})
export class OperationModule {}
