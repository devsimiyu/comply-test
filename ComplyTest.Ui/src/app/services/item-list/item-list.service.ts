import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Item {
  id: string;
  name: string;
  dateCreated: string;
  dateModified: string;
  factorial?: number;
}

@Injectable()
export class ItemListService {

  constructor(private http: HttpClient) { }

  list(): Observable<Array<Item>> {
    const url = environment.API;
    return this.http.get<Array<Item>>(url);
  }

  delete(id: string): Observable<void> {
    const url = environment.API + id;
    return this.http.delete<void>(url);
  }

  factorial(): Observable<Item> {
    const url = environment.API + 'factorial';
    return this.http.get<Item>(url).pipe(tap(console.log));
  }
}
