import { DatasourceService } from './../services/datasource.service';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-rawdata',
  templateUrl: './rawdata.component.html',
  styleUrls: ['./rawdata.component.scss']
})
export class RawdataComponent implements OnInit {
  rawdata;
  constructor(
    public dialogRef: MatDialogRef<RawdataComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private datasource: DatasourceService) { }

  ngOnInit(): void {
    console.log(this.data);
    this.datasource.doGetTfCasesRawdata(this.data.caseId).subscribe(response => {
      response[0] = response[0].replaceAll('<', '&lt;').replaceAll('>', '&gt;');
      this.rawdata = response[0];
      this.data.hitColums.forEach(element => {
        this.rawdata = this.rawdata.replaceAll(`&lt;${element.tagName}&gt;${element.matchData}&lt;/${element.tagName}&gt;`, `<span class="highlight">&lt;${element.tagName}&gt;${element.matchData}&lt;/${element.tagName}&gt;</span>`);
      });
    });
  }
}
