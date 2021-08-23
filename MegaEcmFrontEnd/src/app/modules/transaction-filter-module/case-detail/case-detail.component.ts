import { RawdataComponent } from './../rawdata/rawdata.component';
import { HitColumns } from './../classes/hit-columns';
import { DatasourceService } from './../services/datasource.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertList } from '../classes/alert-list';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-case-detail',
  templateUrl: './case-detail.component.html',
  styleUrls: ['./case-detail.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class CaseDetailComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['matchedListRecordId', 'matchedName', 'matchType', 'rule', 'matchedListSubKey'];
  states: string[] = [$localize`:@@TrueMatch:True Match`, $localize`:@@FalseMatch:False Match`];
  tableDataSource: MatTableDataSource<AlertList>;
  alterts: Array<AlertList> = [];
  caseId: string;
  hitColumns: Array<HitColumns>;
  selectedColumn;
  distinctColumns;
  listDetail: any = [];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private activatedRoute: ActivatedRoute,
    public dialog: MatDialog,
    private router: Router,
    private datasourceService: DatasourceService
  ) {
    this.caseId = this.activatedRoute.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {

    this.datasourceService.doGetTfAlerts(this.caseId).subscribe((response: any) => {
      this.alterts = response;
      this.tableDataSource = new MatTableDataSource(this.alterts);
      this.tableDataSource.paginator = this.paginator;
      this.tableDataSource.sort = this.sort;
      console.log(this.alterts);
    });

    this.datasourceService.doGetHitColumn(this.caseId).subscribe((responses: any) => {
      const distinctColumns: Array<any> = [];
      for (const response of responses) {
        if (!(distinctColumns.map(e => e.tagName).includes(response.tagName)
          && distinctColumns.map(e => e.matchData).includes(response.matchData))) {
          distinctColumns.push({
            tagName: response.tagName,
            matchData: response.matchData,
            highlight: false
          });
        }
      }
      this.hitColumns = responses;
      this.distinctColumns = distinctColumns;
    });

  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.tableDataSource.filter = filterValue.trim().toLowerCase();

    if (this.tableDataSource.paginator) {
      this.tableDataSource.paginator.firstPage();
    }
  }

  getListDetail(expandedElement, target: HTMLElement): void {
    this.listDetail = [];

    this.distinctColumns.forEach((column: any) => {
      column.highlight = false;
    });

    if (expandedElement !== null) {
      this.datasourceService.doGetListDetail(expandedElement.listSubTypeId).subscribe((responses: any) => {
        responses.forEach((response: any) => {
          // 處理 aliases 換行
          if (response.key === 'Aliases') {
            response.value = response.value.split('\\n').join('<br />');
          }
        });
        this.listDetail = responses;
      });

      // 高亮命中欄位
      this.selectedColumn = this.hitColumns.filter(c =>
        c.listSubTypeId === expandedElement.listSubTypeId
        && c.tagName === expandedElement.tagName)[0];

      this.distinctColumns.forEach((column: any) => {
        if (column.tagName === this.selectedColumn.tagName &&
          column.matchData === this.selectedColumn.matchData) {
          column.highlight = true;
        }
      });


    }
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(RawdataComponent, {
      data: { caseId: this.caseId, hitColums: this.distinctColumns }
    });
  }

  back(): void {
    this.router.navigate(['TransationFilter/CaseManagement']);
  }

}


