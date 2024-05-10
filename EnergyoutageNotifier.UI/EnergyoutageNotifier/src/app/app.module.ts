import { NgModule } from '@angular/core';
import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { DxBulletModule, DxButtonModule, DxCheckBoxModule, DxContextMenuModule, DxDataGridModule, DxDateBoxModule, DxFormModule, DxNumberBoxModule, DxPopupModule, DxSelectBoxModule, DxTemplateModule, DxTextBoxModule} from 'devextreme-angular';
import { AppComponent } from './app.component';
import { Service } from './app.service';
import { SignalService } from 'src/shared/services/Signal.service';
import { NotificationComponent } from 'src/components/notification/notification.component';
import { NotificationAllComponent } from 'src/components/notification-all/notification-all.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NotificationCreateComponent } from 'src/components/notification-create/notification-create.component';


@NgModule({
  declarations: [
    AppComponent,
    NotificationComponent,
    NotificationAllComponent,
    NotificationCreateComponent
   ],
  imports: [
    BrowserModule,
    DxButtonModule,
    BrowserModule,
    HttpClientModule,
    BrowserTransferStateModule,
    DxDataGridModule,
    DxTemplateModule,
    DxBulletModule,
    RouterModule,
    DxPopupModule,
    DxFormModule,
    DxDateBoxModule,
    DxTextBoxModule

  ],
  providers: [Service,SignalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
