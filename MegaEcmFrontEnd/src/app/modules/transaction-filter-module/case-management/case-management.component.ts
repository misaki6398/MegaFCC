import { DatasourceService } from './../services/datasource.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';


export interface CaseList {
  messageDefinitionIdentifier: string;
  swallowId: string;
  businessMessageIdentifier: string;
  currency: string;
  amount: number;
  brachNo: string;
  caseStatus: string;
  assignBy: string;
  createDateTime: string;
  caseId: string;
}


@Component({
  selector: 'app-case-management',
  templateUrl: './case-management.component.html',
  styleUrls: ['./case-management.component.scss']
})

export class CaseManagementComponent implements OnInit {
  displayedColumns: string[] = ['messageDefinitionIdentifier', 'swallowId', 'businessMessageIdentifier', 'currency', 'amount', 'brachNo', 'caseStatus', 'assignBy', 'createDateTime', 'actions'];
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

  clickAssign(caseId): void {
    this.router.navigate(['TransationFilter/CaseDetail', caseId]);
  }

}
