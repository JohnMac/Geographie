import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface GeographieData {
  id: number;
  openbareruimte: string;
  huisnummer: number;
  woonplaats: string;
  postcode: string;
}

@Injectable({
  providedIn: 'root'
})
export class GeographieDataService {
  private apiUrl = 'https://localhost:5001/api/v1/geographiedata';

  constructor(private http: HttpClient) { }

  getAll(): Observable<GeographieData[]> {
    return this.http.get<GeographieData[]>(this.apiUrl);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
