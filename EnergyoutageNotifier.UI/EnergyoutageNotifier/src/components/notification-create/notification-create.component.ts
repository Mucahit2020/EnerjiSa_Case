import { DatePipe, formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Service } from 'src/app/app.service';

@Component({
  selector: 'app-notification-create',
  templateUrl: './notification-create.component.html',
  styleUrls: ['./notification-create.component.css']
})
export class NotificationCreateComponent implements OnInit {
//   employee = {
//     name: "John Doe",
//     officeNumber: "123",
//     hireDate: new Date("2024-05-05T08:00:00") // Belirtilen tarih ve saat
// };

  formData: any = {}; // Form verilerini tutacak nesne

  onSubmit(): void {
    console.log(this.formData); // Form verilerini konsola yazdırır
    // Burada form verilerini bir nesneye atayabilir ve işleyebilirsiniz
  }



  dateOptions: any = {
    type: 'datetime',
    label: 'Hire Date and Time',
    format: "yyyy-MM-ddTHH:mm:ss" // ISO 8601 formatı
};

  constructor( ) { }

  ngOnInit() {
  }

}
