import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { NotificationAllComponent } from 'src/components/notification-all/notification-all.component';

export const routes: Routes = [
  { path: 'notification-all', component: NotificationAllComponent

   },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRouterModule {}
