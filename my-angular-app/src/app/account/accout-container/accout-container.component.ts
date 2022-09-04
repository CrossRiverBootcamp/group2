import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-accout-container',
  templateUrl: './accout-container.component.html',
  styleUrls: ['./accout-container.component.scss'],
})
export class AccoutContainerComponent implements OnInit {
  register?: boolean = false;
  authorized?: boolean = false;

  constructor() {}

  ngOnInit(): void {}
}
