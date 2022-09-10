import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-accout-container',
  templateUrl: './accout-container.component.html',
  styleUrls: ['./accout-container.component.scss'],
})
export class AccoutContainerComponent implements OnInit {
  register?: boolean = false;
  authorized?: boolean = false;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getAuth().subscribe((bool) => {
      this.authorized = bool;
    });
  }

  switchToRegister() {
    this.register = true;
  }

  switchToLogin() {
    this.register = false;
  }
}
