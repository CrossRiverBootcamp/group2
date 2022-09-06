import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-body-container',
  templateUrl: './body-container.component.html',
  styleUrls: ['./body-container.component.scss'],
})
export class BodyContainerComponent implements OnInit {
  authorized: boolean = false;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getAuth().subscribe((bool) => {
      this.authorized = bool;
    });
  }
}
