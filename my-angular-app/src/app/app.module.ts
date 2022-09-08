import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccountModule } from './account/account.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserService } from './services/user.service';
import { MainComponentsModule } from './main-components/main-components.module';
import { TransactionModule } from './transaction/transaction.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AccountModule,
    HttpClientModule,
    MainComponentsModule,
    TransactionModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
