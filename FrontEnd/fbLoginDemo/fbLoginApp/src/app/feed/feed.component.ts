import { Component, OnInit } from '@angular/core';
import { UserDataServService } from '../user-data-serv.service';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

  public userData = [];
  public feedList = [];
  public feedString;
  public feedIdList = [];
  public feedObj;
  private pp;
  public fbUrl :string;
  
  constructor(private _dataService:UserDataServService,) { }

  ngOnInit() {
    this._dataService.getUserData()
        .subscribe(data =>{ 
          if(data["id"]==null){
            this.userData["id"]="  -  ";
            this.userData["email"]="  -  ";
            this.userData["location"]="  -  ";
          }
          else{
            // console.log(JSON.stringify(data["realUrlList"]).trimLeft().split(" "));
            this.userData = data;

            this.feedString = data["feed"];
            let objList = JSON.parse(this.feedString)["data"];
            this.feedList = objList;
            this.fbUrl = "https://www.facebook.com/profile.php/"+this.userData["id"];
            // console.log(data["id"]);
          for(let a of objList){
            let realDeal = a["id"].split("_")[1];
            console.log(a["id"].split("_")[0]);
            if(typeof(a["message"])!=='undefined'&& a["message"] !== null)
            {
              this.feedIdList.push( [realDeal,a["message"],a["created_time"]]);
            }
            else{
              this.feedIdList.push( [realDeal,"~This post was deleted or can not be displayed(i.e. shared post)~",a["created_time"]]);
            }
          }
        }
        })
  }
  
}