import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  messagesCount: number = 11;
  accountID?: number;

  constructor(private userService: UserService) {}
  ngOnInit(): void {
    this.accountID = this.userService.getAccountID();
  }
}
