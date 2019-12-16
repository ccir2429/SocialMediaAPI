import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserDataServService } from './user-data-serv.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Social Media Login';
  username= '';
  message='You are logged in as';
  fbUrl:string;
  constructor(private _dataService:UserDataServService) { }

  ngOnInit() {
    try{
      this._dataService.getUserData()
          .subscribe(data =>{ 
            
            if(data["name"]==null)
            {
              this.message="You are not logged in";
            }else
            this.username = data["name"];
            this.fbUrl="https://www.facebook.com/profile.php/"+data["id"];
          })
    }catch(e){
      console.error(e);
      this.username="";
      this.message="You are not logged in";
    }

}
}
