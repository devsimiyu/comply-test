import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Item {
  name: string;
}

@Injectable()
export class ItemFormService {

  constructor(private http: HttpClient) { }

  create(item: Item): Observable<Item> {
    const url = environment.API;
    return this.http.post<Item>(url, item);
  }
  
  update(id: string, item: Item): Observable<Item> {
    const url = environment.API + id;
    return this.http.put<Item>(url, item);
  }

  details(id: string): Observable<Item> {
    const url = environment.API + id;
    return this.http.get<Item>(url);
  }
}
