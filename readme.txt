Proje ön yüz ve arka yüz olmak üzere iki bölümden oluşmaktadır.


* Ön Yüz
Ön yüzde Angular kullandım. Devextreme araçlarını kullandım. 
Farklı olarak SignalR teknolojisi de bulunmaktadır, arka yüz ile entegre biçimde.

*Arka Yüz 
Net Core 7.0 ile geliştirdim. Katmanlı mimariyi tercih ettim. 



Çalışma Yapısı

https://localhost:44321/add endpointine ;

{
  "OutageStartTime": "2024-05-05T08:00:00",
  "OutageETD": "2024-05-05T12:00:00",
  "NotificationType": 1,
  "AffectedArea": "İstanbul",
  "NotificationDetails": {
    "NotificationDetail": "Elektrik kesintisi yaşanacak.",
    "OutageCause": "Bakım Çalışması Yapılacak."
  }
}

verisi ile atılan istek sonucunda bir notification eklenir.

Burada satırların sırası ile açıklaması; 

*Kesinti Başlangıç Tarihi, Tahmini Sona Erme Tarihi, Bildirim Tipi (1 ve 0 'dan oluşuyor 0 ise kesinti sona erdi 1 ise kesinti devam ediyor), Etkilenen Bölge, Bildirim detayı ve Kesinti Detayı.

*Bu istek sonucunda Bildirim(Notification) tablosuna ve Kesinti(Outage) tablosuna kayıt atılmaktadır. Notification tablosu ile Outage tablosu birbirine bağlıdır. (Outage tablosu NotificationId foreign key to Notification)

*Bu istek sonucunda ön yüzde angularda bir bildirim tetiklenir, burada signalR devreye giriyor ve anlık olarak gelen bildirimi ekranda görebiliyoruz. Bu bildirim sırasında kullanılan api endpointi ise https://localhost:44321/get-last

*Aynı sayfada hemen alt bölümde enerji kesintisinin devam ettiği kayıtlar (NotificationType==1) yer almaktadır. Buradaki api isteği ise https://localhost:44321/get-current enpointine atılmaktadır.

*https://localhost:44321/update isteği gönderildiği zaman hangi notification için gönderilmişse o notificationType==0 olarak update edilir. Ve artık sayfamızda yer alan gridde yer almaz.

{
  "NotificationId": 3,
  "NotificationType": 0,
  "OutageEndTime": "2024-05-07T12:30:00"
}
update işlemi için kullanılan ilgili veri.

*Geçmiş Kesintiler adında bir sekme bulunmaktadır, bu bir popup'u tetikler ve bize tüm kesintilerin kayıtlarını verir .
https://localhost:44321/get-all endpointi ile kullanılmaktadır.








