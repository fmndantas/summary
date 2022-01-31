import {Injectable} from "@angular/core";
import {HttpClient, HttpResponse} from "@angular/common/http";
import {ISummary} from "./summary.model";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class SummaryService {
  constructor(private http: HttpClient) {
  }

  public findAll$(): Observable<ISummary[]> {
    return this.http
      .get<ISummary[]>(`${environment.api}summary`);
  }

  public findById$(id: number): Observable<ISummary> {
    return this.http
      .get<ISummary>(`${environment.api}summary/${id}`);
  }

  public save$(summary: ISummary): Observable<ISummary> {
    return this.http
      .post<ISummary>(`${environment.api}summary`, summary);
  }
}