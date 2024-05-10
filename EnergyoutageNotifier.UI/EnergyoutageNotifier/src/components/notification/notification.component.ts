import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subject, catchError, tap } from 'rxjs';
import { Service } from 'src/app/app.service';
import { SignalService } from 'src/shared/services/Signal.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { OutageDto } from 'src/shared/models/notification';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
})
export class NotificationComponent implements OnInit, OnDestroy {
  receivedMessage: string | undefined;
  _isVisibleLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );

  showContainer$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );

  dataSource: any;
  collapsed = false;

  notificationResultDto: OutageDto | undefined;

  contentReady = (e: DxDataGridTypes.ContentReadyEvent) => {
    if (!this.collapsed) {
      this.collapsed = true;
      e.component.expandRow(['EnviroCare']);
    }
  };

  customizeTooltip = ({ originalValue }: Record<string, string>) => ({
    text: `${parseInt(originalValue)}%`,
  });

  constructor(
    service: Service,
    private signalService: SignalService,
    private httpClient: HttpClient
  ) {
    this.dataSource = service.getDataSource();
  }
  ngOnDestroy(): void {
    this.signalService.hubConnection?.off('ReceiveMessage');
  }

  closeContainer() {
    this.showContainer$.next(false);
  }

  ngOnInit() {
    // this.getCurrentNotify();
    this.loadDataGrid();
    this.signalService.startConnection();
    this.signalService.showContainer$.subscribe((show) => {
      if (show) {
        this.toggleContainer();
      }
    });
    setTimeout(() => {
      this.signalService.ReceiveMessageListener();
      this.signalService.SendMessage();
    }, 200);
  }

  toggleContainer() {
    this.getNotify();
    this.showContainer$.next(true);
  }

  sendMessage(message: string): void {
    this.signalService.sendMessage(message);
  }

  private getNotify() {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    this._isVisibleLoading$.next(true);

    this.httpClient
      .get<OutageDto>(
        'https://localhost:44321/get-last',
        httpOptions
      )
      .subscribe((data: OutageDto) => {
        this.notificationResultDto = data;
        console.log('Son :', this.notificationResultDto);
        console.log('Son :', this.notificationResultDto?.notification);

        this._isVisibleLoading$.next(false);
      });
  }

  // private getCurrentNotify() {
  //   const httpOptions = {
  //     headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  //   };

  //   this._isVisibleLoading$.next(true);

  //   this.httpClient
  //     .get<OutageDto>(
  //       'https://localhost:44321/notificationGetCurrentOutages',
  //       httpOptions
  //     )
  //     .subscribe((data: OutageDto) => {
  //       this.notificationResultDto = data;
  //       console.log('Son 2 :', this.notificationResultDto);
  //     });
  // }

  loadDataGrid() {
    this._isVisibleLoading$.next(true);
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    this.httpClient
      .get('https://localhost:44321/get-current', httpOptions)
      .toPromise()
      .then((data: any) => {
        console.log(data);
        this.notificationResultDto = data;
        this.dataSource = this.notificationResultDto;
        this._isVisibleLoading$.next(false);
      })
      .catch((error) => {
        this._isVisibleLoading$.next(false);
        throw 'Bir Hata Oluştu';
      });
  }
}
