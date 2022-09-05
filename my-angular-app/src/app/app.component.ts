import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'E&L-Bank-accounts-app';
  register?: boolean = sessionStorage.getItem('AccountID') ? true : false;
  authorized?: boolean = false;

  constructor(private modalService: NgbModal) {}

  public open(modal: any): void {
    this.modalService.open(modal);
  }
}
