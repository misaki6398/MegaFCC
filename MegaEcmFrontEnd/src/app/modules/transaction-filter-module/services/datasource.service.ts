import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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

  doGetTfAlerts(caseId: string): Observable<any> {
    return this.http.get(`${this.url}/api/TfAlerts/${caseId}`, this.options);

  }
  doGetHitColumn(caseId: string): Observable<any> {
    return this.http.get(`${this.url}/api/TfAlerts/HitColumn/${caseId}`, this.options);
  }
  doGetListDetail(listSubTypeId: string): Observable<any> {
    return this.http.get(`${this.url}/api/TfAlerts/ListDetail/${listSubTypeId}`, this.options);
  }
}
