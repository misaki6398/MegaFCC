import { CaseDetailComponent } from './case-detail/case-detail.component';
import { CaseManagementComponent } from './case-management/case-management.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'TransationFilter/CaseManagement', component: CaseManagementComponent
  }, {
    path: 'TransationFilter/CaseDetail/:id', component: CaseDetailComponent
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true, relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class TransationFilterRoutingModule { }
