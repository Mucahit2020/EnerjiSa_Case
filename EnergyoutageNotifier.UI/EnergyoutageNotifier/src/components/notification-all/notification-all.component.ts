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
  selector: 'app-notification-all',
  templateUrl: './notification-all.component.html',
  styleUrls: ['./notification-all.component.css']
})
export class NotificationAllComponent implements OnInit {
  receivedMessage: string | undefined;
  _isVisibleLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
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
    private httpClient: HttpClient
  ) {
    this.dataSource = service.getDataSource();
  }
  ngOnDestroy(): void {
  }



  ngOnInit() {
    // this.getCurrentNotify();
    this.loadDataGrid();

  }
  calculateOutageStatus(rowData: any): string {
    if (rowData.outageETD === 0) {
      return "Kesinti Sona Erdi";
    } else if (rowData.outageETD === 1) {
      return "Kesinti Devam Ediyor";
    } else {
      return "";
    }
  }  loadDataGrid() {
    this._isVisibleLoading$.next(true);
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    this.httpClient
      .get('https://localhost:44321/get-all', httpOptions)
      .toPromise()
      .then((data: any) => {
        console.log("Benim Data:",data);
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
