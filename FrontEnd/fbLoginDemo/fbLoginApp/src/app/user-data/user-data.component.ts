import { Component, OnInit } from '@angular/core';
import { UserDataServService } from '../user-data-serv.service';

@Component({
  selector: 'app-user-data',
  templateUrl: './user-data.component.html',
  styleUrls: ['./user-data.component.css']
})
export class UserDataComponent implements OnInit {

  public userData = [];
  constructor(private _dataService:UserDataServService) { }

  ngOnInit() {
    this._dataService.getUserData()
        .subscribe(data => this.userData = data)
  }
}
