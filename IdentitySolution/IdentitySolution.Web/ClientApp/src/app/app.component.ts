import { Component, OnInit, isDevMode } from '@angular/core';
import { environment } from '../environments/environment'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  data: string = environment.data;
}

//export class AppComponent implements OnInit {
//  ngOnInit() {
//    if (isDevMode()) {
//      console.log(environment.data);
//    } else {
//      console.log(environment.data);
//    }
//  }
//}
