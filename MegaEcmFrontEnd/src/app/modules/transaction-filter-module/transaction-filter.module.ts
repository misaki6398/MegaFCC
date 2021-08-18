import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CaseManagementComponent } from './case-management/case-management.component';
import { TransationFilterRoutingModule } from './transaction-filter-routing.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CaseDetailComponent } from './case-detail/case-detail.component';
import { XmlPipe } from 'src/app/Pipes/xml.pipe';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule } from '@angular/material/select';
import { DatasourceService } from './services/datasource.service';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    CaseManagementComponent,
    CaseDetailComponent,
    XmlPipe
  ],
  imports: [
    CommonModule,
    TransationFilterRoutingModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    MatInputModule,
    MatSortModule,
    MatProgressBarModule,
    MatTabsModule,
    MatSelectModule,
    HttpClientModule,
    MatProgressSpinnerModule
  ],
  providers: [DatasourceService],
  exports: [
    CaseManagementComponent
  ]
})
export class TransationFilterModule { }
