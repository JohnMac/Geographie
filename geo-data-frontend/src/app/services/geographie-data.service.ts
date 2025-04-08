import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface GeographieData {
  id: number;
  openbareruimte: string;
  huisnummer: number;
  huisletter: string;
  huisnummertoevoeging: number;
  postcode: string;
  woonplaats: string;
  gemeente: string;
  provincie: string;
  nummeraanduiding: string;
  verblijfsobjectgebruiksdoel: string;
  oppervlakteverblijfsobject: number;
  verblijfsobjectstatus: string;
  objectId: string;
  objectType: string;
  nevenadres: string;
  pandId: string;
  pandstatus: string;
  pandbouwjaar: number;
  x: number;
  y: number;
  lon: number;
  lat: number;
}

@Injectable({
  providedIn: 'root'
})
export class GeographieDataService {
  private apiUrl = `${environment.apiUrl}/GeoData`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<GeographieData[]> {
    return this.http.get<GeographieData[]>(this.apiUrl);
  }

  get(id: number): Observable<GeographieData> {
    return this.http.get<GeographieData>(`${this.apiUrl}/${id}`);
  }

  create(data: GeographieData): Observable<GeographieData> {
    return this.http.post<GeographieData>(this.apiUrl, data);
  }

  update(id: number, data: GeographieData): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, data);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
