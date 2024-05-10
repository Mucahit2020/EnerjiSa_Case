export interface OutageDto {
  outageId: number;
  outageStartTime: Date;
  outageETD: Date;
  outageEndTime: Date;
  affectedArea: string;
  outageCause: string;
  notificationId: number;
  notification: NotificationDto;
}

export interface NotificationDto {
  notificationId: number;
  notificationTime: Date;
  notificationType: number;
  affectedArea: string;
  notificationDetail: NotificationDetailsDto;
}
export interface NotificationDetailsDto {
  notificationDetail: string;
  outageCause: string;
}
