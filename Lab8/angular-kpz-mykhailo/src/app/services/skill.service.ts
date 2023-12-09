import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SkillService {

  private _getSkillsUrl: string = 'http://localhost:5018/api/Skill/GetAll';

  constructor(private _http: HttpClient) {}
  getSkills(): Observable<any>{
    return this._http.get<any>(this._getSkillsUrl);
  }
}
