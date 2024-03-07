import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})

// Now, inside our app component, it goes through several stages before it displays the content inside our browser. The earliest part is the constructor. This happens as soon as our component is instantiated and there are several life cycle events that goes through a component when it's created. But the constructor is normally considered too early to go and fetch data from an API. So we're going to implement a life cycle event inside this component. And to do that we're going to implement an interface and the interface we're going to implement is the Oninit interface. And if we take a look at this description, this is a lifecycle hook that is called after Angular has initialized all data bound properties of a directive. Define an NG init method to handle any additional initialization tasks in the grand scheme of things. This one happens after our component has been constructed and then we can carry out any additional initialization tasks that we need.
export class AppComponent implements OnInit {
  title = 'Dating app';
  users: any;
  // private - available only to class
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    // get returns Observable - a stream of data that we want to observe in some way
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }
}
