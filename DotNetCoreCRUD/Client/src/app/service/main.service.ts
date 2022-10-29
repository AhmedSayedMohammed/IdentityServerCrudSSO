import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IPaginatedResult } from '../shared/wrapper/IPaginatedResult';
import { IResponse } from '../shared/wrapper/iresponse';
import { IVacancy } from '../shared/wrapper/ivacancy';



@Injectable({
  providedIn: 'root'
})
export class MainService {
 
  readonly baseUrl=environment.apiUrl+'Vacancies'
  constructor(private http:HttpClient, private router: Router,) {}

  getVacancyList(PageNumber:number,PageSize:number):Observable<IPaginatedResult<IVacancy>>{
    return this.http.get<IPaginatedResult<IVacancy>>(this.baseUrl+'?PageNumber='+PageNumber+'&PageSize='+PageSize);
   }

  getVacancyById(Id:BigInteger):Observable<IResponse<IVacancy>>{
    return this.http.get<IResponse<IVacancy>>(this.baseUrl+'/'+Id);
   } 
  editVacancy(data:IVacancy)
   {
    return this.http.put<IResponse<string>>(this.baseUrl+'/'+data.id,data);
   }
  addVacancy(data:IVacancy)
  {
   return this.http.post<IVacancy>(this.baseUrl,data);
  }
  deleteVacancy(id:BigInteger)
  {
   return this.http.delete<IResponse<string>>(this.baseUrl+'/'+id);
  }
}

