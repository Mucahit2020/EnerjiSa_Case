export interface Notification {
  OutageStartTime: Date;
  OutageETD: Date;
  NotificationType: number;
  AffectedArea: string;
  NotificationDetails: NotificationDetail;
}

export interface NotificationDetail {
  NotificationDetail: string;
  OutageCause: string;
}
