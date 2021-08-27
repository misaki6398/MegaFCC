import { AlertStatus } from '../enums/alert-status.enum';
export class AlertList {
  alertId: string;
  alertStatusCode: AlertStatus;
  listSubTypeId: string;
  matchType: string;
  matchedListRecordId: string;
  matchedListSubKey: string;
  matchedName: string;
  rule: string;
  tagName: string;
}

