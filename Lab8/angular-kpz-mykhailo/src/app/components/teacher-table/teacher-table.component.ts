import { Component } from '@angular/core';

@Component({
  selector: 'app-teacher-table',
  templateUrl: './teacher-table.component.html',
  styleUrl: './teacher-table.component.css'
})
export class TeacherTableComponent {
  public teachers = [
    {"teacherId": 1, "firstName": "John", "lastName": "Doe", "middleName": "Antonovich", "birthDate": "1990-05-15", "style": "LectureBased"},
    {"teacherId": 2, "firstName": "Ivan", "lastName": "Ivanov", "middleName": "Ivanovich", "birthDate": "1985-07-12", "style": "Mentorship"}
  ];
  public tableFields: string[] = ['teacherId', 'firstName', 'lastName', 'middleName', 'birthDate', 'style'];
}
