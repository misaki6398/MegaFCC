import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { State } from '../classes/state';
import { DatasourceService } from '../services/datasource.service';

@Component({
  selector: 'app-case-comment',
  templateUrl: './case-comment.component.html',
  styleUrls: ['./case-comment.component.scss']
})
export class CaseCommentComponent implements OnInit {
  userComment = '';
  constructor(
    public dialogRef: MatDialogRef<CaseCommentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private datasource: DatasourceService,
    public router: Router) { }

  ngOnInit(): void {
  }

  onClickSubmit(): void {
    console.log(this.userComment);
    const state: State = {
      caseId: this.data.caseId,
      nowWorkflow: this.data.caseStatus,
      nextWorkflow: this.data.caseResolution,
      userComment: this.userComment
    };
    this.datasource.doPostWorkflow(state).subscribe(response => {
      alert('Submit ok!');
      this.dialogRef.close();
      this.router.navigate(['TransationFilter/CaseManagement']);
    });
  }

}
