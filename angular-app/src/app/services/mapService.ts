import { HttpClient } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Station } from "../admin-station/map/model/station";
import { Line } from '../models/Line';


@Injectable({
    providedIn: 'root',
  })
export class MapService {
  
    registerUrl: string =  'http://localhost:52295/api/';

    constructor(private http: HttpClient,private route:Router) { }
  
    public getLines() : Promise<Line[]>{
      return this.http.get<Line[]>(this.registerUrl+"api/Line/GetLines").toPromise<Line[]>();
    }  

    update(station:Station): Observable<string> {

        return this.http.put<string>(this.registerUrl+"Station/UpdateStation",station,{ 'headers': { 'Content-type': 'application/json' }} ).pipe(
          map(res => {
          alert("Successfully update!");
          }),
          catchError(this.handleError<any>('login'))
        );
    }


    add(station: Station): Observable<any> {
      return this.http.post<any>(this.registerUrl+"Station/Add", station, { 'headers': { 'Content-type': 'application/json' } }).pipe(
        map(res => {
        alert("Uspesno dodata stanica");
        }),
        catchError(this.handleError<any>('login'))
      );
    }
  
    public delete(name: string){
      return this.http.delete(`http://localhost:52295/api/Station/Delete/${name}`);
      }

    getStations(): Observable<Station[]> {        
        return this.http.get<Station[]>(this.registerUrl+"Station/GetAll")
        .pipe(
          //catchError(this.handleError<Hero[]>('getHeroes', []))
        );
      }
 
      getStation(number:string): Observable<Station[]> {        
        return this.http.get<Station[]>(this.registerUrl+"Station/Get/?Number="+number)
        .pipe(
          //catchError(this.handleError<Hero[]>('getHeroes', []))
        );
      }
 
    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
        if(error.error.Message!=undefined)
        alert(error.error.Message);
        
        alert("Greska! Stanica nije dodata.");
        return of(result as T);
      };
    }
  }
  