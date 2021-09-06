import { CaseStatus } from './../enums/case-status.enum';
import { AlertDecision } from './../classes/alert-decision';
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
import { SelectionModel } from '@angular/cdk/collections';
import { AlertStatus } from '../enums/alert-status.enum';
import { CaseCommentComponent } from '../case-comment/case-comment.component';

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

  // 需對應到 backend 專案 AlertStatus enum
  alertStatus = [
    { name: $localize`:@@NewAlert:New Alert`, value: AlertStatus.NewAlert },
    { name: $localize`:@@TrueMatch:True Match`, value: AlertStatus.TrueMatch },
    { name: $localize`:@@FalseMatch:False Match`, value: AlertStatus.FalseMatch }
  ];

  // 需對應到 backend 專案 CaseStatus enum
  l1CaseStatus = [
    { name: $localize`:@@ReleaseRecommand:Release Recommand`, value: CaseStatus.ReleaseRecommand },
    { name: $localize`:@@BlockRecommand:Block Recommand`, value: CaseStatus.BlockRecommand },
  ];

  // 需對應到 backend 專案 CaseStatus enum
  RRCaseStatus = [
    { name: $localize`:@@Release:Release`, value: CaseStatus.Release },
    { name: $localize`:@@Reject:Reject`, value: CaseStatus.Reject },
  ];

  // 需對應到 backend 專案 CaseStatus enum
  BRCaseStatus = [
    { name: $localize`:@@Block:Block`, value: CaseStatus.Block },
    { name: $localize`:@@Reject:Reject`, value: CaseStatus.Reject },
  ];

  selection = new SelectionModel<AlertList>(true, []);
  tableDataSource: MatTableDataSource<AlertList>;
  alerts: Array<AlertList> = [];
  caseId: string;
  hitColumns: Array<HitColumns>;
  selectedColumn;
  distinctColumns;
  listDetail: any = [];
  caseResolution = 0;
  newAlerDecisionCount;
  caseDetail: any = [];
  caseAudits: any = [];

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
    this.datasourceService.doGetTfCase(this.caseId).subscribe((response: any) => {
      this.caseDetail = response[0];
      console.log(this.caseDetail);
    });

    this.datasourceService.doGetTfAlerts(this.caseId).subscribe((response: any) => {
      this.alerts = response;
      this.tableDataSource = new MatTableDataSource(this.alerts);
      this.tableDataSource.paginator = this.paginator;
      this.tableDataSource.sort = this.sort;
      console.log(this.alerts);
      this.newAlerDecisionCount = this.alerts.filter(c => c.alertStatusCode === 0).length;
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

    this.datasourceService.doGetTfCaseAudits(this.caseId).subscribe((response: any) => {
      this.caseAudits = response;
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
      this.datasourceService.doGetListDetail(expandedElement.alertId).subscribe((responses: any) => {
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

  openRawdataDialog(): void {
    const dialogRef = this.dialog.open(RawdataComponent, {
      data: { caseId: this.caseId, hitColums: this.distinctColumns }
    });
  }

  back(): void {
    this.router.navigate(['TransationFilter/CaseManagement']);
  }


  onClickDecision(element, alertStatusCode): void {
    const alertDecision: Array<AlertDecision> = [{
      alertId: element.alertId,
      alertStatusCode
    }];
    console.log(this.newAlerDecisionCount);
    this.datasourceService.doPostAlertDesicion(alertDecision).subscribe((respones: any) => {
    }, error => {
      alert('Update error');
    });
  }

  onClickAllFalseMatch(): void {
    this.updateDecision(AlertStatus.FalseMatch);

  }

  onClickAllTrueMatch(): void {
    this.updateDecision(AlertStatus.TrueMatch);
  }

  updateDecision(alertStatusCode: number): void {
    const alertDecisions: Array<AlertDecision> = [];

    this.alerts.forEach((row, index) => {
      const alertDecision: AlertDecision = { alertId: row.alertId, alertStatusCode };
      alertDecisions.push(alertDecision);
    });

    this.datasourceService.doPostAlertDesicion(alertDecisions).subscribe((respones: any) => {
      this.alerts.map(c => c.alertStatusCode = alertStatusCode);
      this.newAlerDecisionCount = this.alerts.filter(c => c.alertStatusCode === 0).length;
    }, error => {
      alert('Update error');
    });
  }

  onClickSubmit(): void {
    if (this.caseDetail.caseStatusCode === CaseStatus.Assigned || this.caseDetail.caseStatusCode === CaseStatus.Reject) {
      const dialogRef = this.dialog.open(CaseCommentComponent, {
        data: { caseId: this.caseId, caseResolution: this.caseResolution, caseStatus: CaseStatus.Assigned }
      });
    }

    if (this.caseDetail.caseStatusCode === CaseStatus.ReleaseRecommand) {
      const dialogRef = this.dialog.open(CaseCommentComponent, {
        data: { caseId: this.caseId, caseResolution: this.caseResolution, caseStatus: CaseStatus.ReleaseRecommand }
      });
    }

    if (this.caseDetail.caseStatusCode === CaseStatus.BlockRecommand) {
      const dialogRef = this.dialog.open(CaseCommentComponent, {
        data: { caseId: this.caseId, caseResolution: this.caseResolution, caseStatus: CaseStatus.BlockRecommand }
      });
    }

  }
}

