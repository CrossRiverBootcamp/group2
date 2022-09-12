import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'E&L-Bank-accounts-app';
  authorized: boolean = false;

  constructor(
    private modalService: NgbModal,
    private userService: UserService
  ) {}

  public open(modal: any): void {
    this.modalService.open(modal);
    this.userService.getAuth().subscribe((bool) => {
      this.authorized = bool;
    });
  }

  ngOnInit(): void {
    this.userService.getAuth().subscribe((bool) => {
      this.authorized = bool;
    });
  }
}
