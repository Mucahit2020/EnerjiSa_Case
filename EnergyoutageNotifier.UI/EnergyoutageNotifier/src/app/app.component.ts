import { Component } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import { Service } from './app.service';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { SignalService } from 'src/shared/services/Signal.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'EnergyoutageNotifier';
  receivedMessage: string | undefined;

  _isVisiblePopup$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);


  onClickNewNotification(){
    this._isVisiblePopup$.next(true);

  }
  onHiding(e:any) {

    this._isVisiblePopup$.next(false);

  }
  onClickGetAllNotification(){

    this._isVisiblePopup$.next(true);

  }

  onClickBtnSubmit() {


}
constructor(service: Service,private signalService: SignalService) {
}

ngOnInit(){

}
sendMessage(message: string): void {
  this.signalService.sendMessage(message);
}
}
