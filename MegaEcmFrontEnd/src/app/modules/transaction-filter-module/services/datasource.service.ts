import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AlertDecision } from '../classes/alert-decision';
import { State } from '../classes/state';

@Injectable({
  providedIn: 'root'
})
export class DatasourceService {
  options: any;
  url = 'https://localhost:5001';
  constructor(private http: HttpClient) {
    // IE會Cache Web Api，需取消
    const headers = new HttpHeaders()
      .set('cache-control', 'no-cache')
      .append('pragma', 'no-cache');


    this.options = {
      headers
    };
  }


  doGetTfCases(): Observable<any> {
    return this.http.get(`${this.url}/api/TfCases/`, this.options);
  }

  doGetTfCase(caseId: string): Observable<any> {
    return this.http.get(`${this.url}/api/TfCases/${caseId}`, this.options);
  }

  doGetTfAlerts(caseId: string): Observable<any> {
    return this.http.get(`${this.url}/api/TfAlerts/${caseId}`, this.options);

  }
  doGetHitColumn(caseId: string): Observable<any> {
    return this.http.get(`${this.url}/api/TfAlerts/HitColumn/${caseId}`, this.options);
  }
  doGetListDetail(listSubTypeId: string): Observable<any> {
    return this.http.get(`${this.url}/api/TfAlerts/ListDetail/${listSubTypeId}`, this.options);
  }

  doGetTfCasesRawdata(caseId: string): Observable<any> {
    return this.http.get(`${this.url}/api/TfCases/RawData/${caseId}`, this.options);
  }

  doPostWorkflow(state: State): Observable<any> {
    return this.http.post(`${this.url}/api/TfCases/Workflow/`, state, this.options);
  }

  doPostAlertDesicion(alertDecision: Array<AlertDecision>): Observable<any> {
     return this.http.post(`${this.url}/api/TfAlerts/AlertDecision/`, alertDecision, this.options);
  }
}
