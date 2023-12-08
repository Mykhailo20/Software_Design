import { Component } from '@angular/core';
import { TeacherService } from '../../services/teacher.service';
import { ITeacher } from '../../entities/teacher';

@Component({
  selector: 'app-teacher-table',
  templateUrl: './teacher-table.component.html',
  styleUrl: './teacher-table.component.css'
})
export class TeacherTableComponent{
  public teachers: ITeacher[] = [];
  public tableFields: string[] = ['teacherId', 'firstName', 'lastName', 'middleName', 'birthDate', 'style'];

  constructor(private _teacherService: TeacherService) {
    
    console.log("Inside TeacherTable constructor");
    this._teacherService.getTeachers()
        .subscribe(data => this.teachers = data.data);
  }
}
