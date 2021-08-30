import { CaseStatus } from './../enums/case-status.enum';
import { DatasourceService } from './../services/datasource.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CaseList } from '../classes/case-list';
import { State } from '../classes/state';

@Component({
  selector: 'app-case-management',
  templateUrl: './case-management.component.html',
  styleUrls: ['./case-management.component.scss']
})

export class CaseManagementComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['messageType', 'swallowId', 'businessMessageIdentifier', 'currency', 'amount', 'brachNo', 'caseStatus', 'assignee', 'createDateTime', 'actions'];
  tableDataSource: MatTableDataSource<CaseList>;
  cases: any = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(public router: Router, private datasourceService: DatasourceService) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {

    this.datasourceService.doGetTfCases().subscribe((response: any) => {
      this.cases = response;
      console.log(this.cases);
      this.tableDataSource = new MatTableDataSource(this.cases);
      this.tableDataSource.paginator = this.paginator;
      this.tableDataSource.sort = this.sort;
    });

  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.tableDataSource.filter = filterValue.trim().toLowerCase();

    if (this.tableDataSource.paginator) {
      this.tableDataSource.paginator.firstPage();
    }
  }

  clickAssign(caseId: string, assignee: string): void {
    const state: State = {
      caseId,
      userComment: '',
      nowWorkflow: CaseStatus.NewCase,
      nextWorkflow: CaseStatus.Assigned
    };
    // TODO: 未來要抓登入者與 assignee 相等
    if (assignee === '009647') {
      this.router.navigate(['TransationFilter/CaseDetail', caseId]);
      return;
    }


    this.datasourceService.doPostWorkflow(state).subscribe((response: any) => {
      this.router.navigate(['TransationFilter/CaseDetail', caseId]);
    }, error => {
      alert('Case Already Assigned by other user');
    });
  }

}
