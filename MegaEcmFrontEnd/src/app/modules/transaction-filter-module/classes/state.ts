import { CaseStatus } from './../enums/case-status.enum';
export class State {
  caseId: string;
  userComment: string;
  nowWorkflow: CaseStatus;
  nextWorkflow: CaseStatus;
}
