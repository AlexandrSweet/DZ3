import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public Dictionary: Array<string>;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Array<string>>(baseUrl + 'DataProviderProfiler').subscribe(result => {
      this.Dictionary = result;
    }, error => console.error(error));
  }
}
