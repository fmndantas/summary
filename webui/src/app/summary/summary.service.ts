import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {ISummary} from "./summary.model";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {ISearchSummary} from "../search-summary/search-summary.model";

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

  public findByTitle$(title: string): Observable<ISearchSummary[]> {
    let body: any = {
      searchText: title,
      caseInsensitive: true
    };
    return this.http
      .post<ISearchSummary[]>(`${environment.api}summary/matcher`, body);
  }

  public save$(summary: ISummary): Observable<ISummary> {
    return this.http
      .post<ISummary>(`${environment.api}summary`, summary);
  }

  public update$(summary: ISummary): Observable<ISummary> {
    return this.http
      .put<ISummary>(`${environment.api}summary`, summary);
  }

  public delete$(summary: ISummary): Observable<any> {
    let id = summary.id;
    return this.http
      .delete<any>(`${environment.api}summary/${id}`);
  }
}
