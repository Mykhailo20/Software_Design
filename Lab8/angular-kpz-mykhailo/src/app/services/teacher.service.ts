import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ITeacher } from '../entities/teacher';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  private _getTeachersUrl: string = 'http://localhost:5018/api/Teacher/GetAll';

  constructor(private _http: HttpClient) {}
  getTeachers(): Observable<any>{
    console.log("Inside getTeachers");
    return this._http.get<any>(this._getTeachersUrl);
  }
}
