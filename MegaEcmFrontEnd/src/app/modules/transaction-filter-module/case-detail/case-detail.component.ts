import { DatasourceService } from './../services/datasource.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CaseList } from '../case-management/case-management.component';
import { ActivatedRoute } from '@angular/router';

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
export class CaseDetailComponent implements OnInit {
  xmlString = `<?xml version="1.0" encoding=\"UTF-8\"?> <env:Envelope xmlns:env=\"urn:swift:xsd:envelope\"> <AppHdr xmlns=\"urn:iso:std:iso:20022:tech:xsd:head.001.001.02\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"> <Fr> <FIId> <FinInstnId> <BICFI>CHASGB2LXXX</BICFI> </FinInstnId> </FIId> </Fr> <To> <FIId> <FinInstnId> <BICFI>CHASUS33XXX</BICFI> </FinInstnId> </FIId> </To> <BizMsgIdr>SS04044506272708</BizMsgIdr> <MsgDefIdr>pacs.008.001.08</MsgDefIdr> <BizSvc>swift.cbprplus.01</BizSvc> <CreDt>2020-01-11T12:43:41.960+00:00</CreDt> </AppHdr> <Document xmlns=\"urn:iso:std:iso:20022:tech:xsd:pacs.008.001.08\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"> <FIToFICstmrCdtTrf> <GrpHdr> <MsgId>SS04044506272708</MsgId> <CreDtTm>2020-01-11T00:00:00+00:00</CreDtTm> <NbOfTxs>1</NbOfTxs> <SttlmInf> <SttlmMtd>INDA</SttlmMtd> </SttlmInf> </GrpHdr> <CdtTrfTxInf> <PmtId> <InstrId>SS04044506272708</InstrId> <EndToEndId>KK04044506271305</EndToEndId> <UETR>174c245f-2682-4291-ad67-2a41e530cd27</UETR> </PmtId> <IntrBkSttlmAmt Ccy=\"USD\">300000</IntrBkSttlmAmt> <IntrBkSttlmDt>2020-01-11</IntrBkSttlmDt> <InstdAmt Ccy=\"USD\">300000</InstdAmt> <ChrgBr>SHAR</ChrgBr> <InstgAgt> <FinInstnId> <BICFI>CHASGB2LXXX</BICFI> </FinInstnId> </InstgAgt> <InstdAgt> <FinInstnId> <BICFI>CHASUS33XXX</BICFI> </FinInstnId> </InstdAgt> <IntrmyAgt1> <FinInstnId> <BICFI>FTSBUS33XXX</BICFI> </FinInstnId> </IntrmyAgt1> <Dbtr> <Nm>A21,INC.</Nm> <PstlAdr> <StrtNm>CENTURION PKWY</StrtNm> <PstCd>32034</PstCd> <TwnNm>JACKSONVILLE</TwnNm> <TwnLctnNm>FLORIDA</TwnLctnNm> <Ctry>US</Ctry> </PstlAdr> </Dbtr> <DbtrAgt> <FinInstnId> <BICFI>CHASGB2LXXX</BICFI> </FinInstnId> </DbtrAgt> <CdtrAgt> <FinInstnId> <BICFI>ABNANL2AXXX</BICFI> </FinInstnId> </CdtrAgt> <Cdtr> <Nm>DELTA LLOYD</Nm> <PstlAdr> <StrtNm>OMVAL 300</StrtNm> <PstCd>1096</PstCd> <TwnNm>AMSTERDAM</TwnNm> <Ctry>NL</Ctry> </PstlAdr> </Cdtr> <CdtrAcct> <Id> <Othr> <Id>NL02ABNA0123456789</Id> </Othr> </Id> </CdtrAcct> <RmtInf> <Ustrd>INVOICE A245,5637/04,991/05</Ustrd> </RmtInf> </CdtTrfTxInf> </FIToFICstmrCdtTrf> </Document> </env:Envelope>`;

  displayedColumns: string[] = ['matchedListRecordId', 'matchedName', 'matchType', 'rule', 'matchedListSubKey'];
  states: string[] = ['True Match', 'False Match'];
  tableDataSource: MatTableDataSource<CaseList>;
  alterts: any = [];
  caseid: string;
  hitColumns;
  distinctColumns;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private activatedRoute: ActivatedRoute, private datasourceService: DatasourceService) {
    this.caseid = this.activatedRoute.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {

    this.datasourceService.doGetTfAlerts(this.caseid).subscribe((response: any) => {
      this.alterts = response;
      console.log(this.alterts);
      this.tableDataSource = new MatTableDataSource(this.alterts);
      this.tableDataSource.paginator = this.paginator;
      this.tableDataSource.sort = this.sort;
    });
    this.datasourceService.doGetHitColumn(this.caseid).subscribe((responses: any) => {

      const distinctColumns: Array<any> = [];
      for (const response of responses) {
        if (!distinctColumns.map(e => e.tagName).includes(response.tagName)
        && !distinctColumns.map(e => e.matchData).includes(response.matchData)) {
          distinctColumns.push({
            tagName: response.tagName,
            matchData: response.matchData
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

}






