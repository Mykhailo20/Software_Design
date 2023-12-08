import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  constructor() { }
  getTeachers(){
    return [
      {"teacherId": 1, "firstName": "John", "lastName": "Doe", "middleName": "Antonovich", "birthDate": "1990-05-15", "style": "LectureBased"},
      {"teacherId": 2, "firstName": "Ivan", "lastName": "Ivanov", "middleName": "Ivanovich", "birthDate": "1985-07-12", "style": "Mentorship"}
    ];
  }
}
