import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import * as _ from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class HttpLayerService {
  protected url: string;
  constructor(
    private http: HttpClient,
    @Inject('API_URL') private apiUrl: string) {
  }

  public delete<T>(url: string): Observable<T> {
    return this.sendRequest<T>('delete', url);
  }

  public get<T>(url: string): Observable<T> {
    return this.sendRequest<T>('get', url, null);
  }

  public put<T>(url: string, body: any): Observable<T> {
    return this.sendRequest<T>('put', url, body);
  }

  public post<T>(url: string, body: any): Observable<T> {
    return this.sendRequest<T>('post', url, body);
  }

  private sendRequest<T>(method: string, url: string, body?: T): Observable<T> {
    let request: any;
    let options: any;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    switch (method) {
      case 'get':
        options = { headers };
        request = this.http.get(`${this.apiUrl}/${url}`, options);
        break;
      case 'post':
        request = this.http.post<T>(`${this.apiUrl}/${url}`, body, {
        });
        break;
      case 'put':
        request = this.http.put<T>(`${this.apiUrl}/${url}`, body, {
          headers
        });
        break;
      case 'put-external':
        options = {};
        request = this.http.put<T>(url, body, options);
        break;
      case 'delete':
        request = this.http.delete<T>(`${this.apiUrl}/${url}`, {
          headers
        });
        break;
      default:
        return throwError(`Unsupported request method: ${method}`);
    }

    return request;
  }

  public buildUrlParams(params: {
    [propName: string]: any
  }): string {
    return _.reduce(params, (res, val, key) => {
      if (_.isArray(val)) {
        val = _.join(val, ',');
      }
      if (!res) {
        res += `?${key}=${val}`;
      } else {
        res += `&${key}=${val}`;
      }
      return res;
    }, '');
  }
}
