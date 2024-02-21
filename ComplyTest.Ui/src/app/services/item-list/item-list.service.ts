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

  factorial(): Observable<Array<Item>> {
    const url = environment.API + 'factorial';
    return this.http.get<Array<Item>>(url);
  }
}
