import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Observable, Subject, from } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class SignalService {
  public hubConnection: signalR.HubConnection | undefined;

  showContainer$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );

  constructor() {
    // this.hubConnection = new signalR.HubConnectionBuilder()
    //   .withUrl('https://localhost:44321/outageNotificationsAdd', {
    //     skipNegotiation: true,
    //   transport: signalR.HttpTransportType.WebSockets
    //   })
    //   .build();
  }

  startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44321/outageNotificationsAdd', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();

      this.hubConnection.start().then(()=>{console.log("hub connection started");})
      .catch(err=>console.log('hata'+err))
  };

  toggleContainer() {
    let that = this;
    that.showContainer$.next(true);
  }

  sendMessage(message: string): void {
    this.hubConnection!.invoke('SendMessage', message);
  }

  SendMessage() {
    this.hubConnection
      ?.invoke('SendMessage', 'hey')
      .catch((err) => console.error(err));
  }

  ReceiveMessageListener() {
    let that = this;
    this.hubConnection?.on('ReceiveMessage', (someText) => {
      console.log(someText);
     that.toggleContainer();
    });
  }

}
