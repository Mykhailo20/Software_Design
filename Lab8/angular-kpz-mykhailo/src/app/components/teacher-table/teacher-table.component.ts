import { Component } from '@angular/core';
import { TeacherService } from '../../services/teacher.service';

@Component({
  selector: 'app-teacher-table',
  templateUrl: './teacher-table.component.html',
  styleUrl: './teacher-table.component.css'
})
export class TeacherTableComponent{
  public teachers: any[];
  public tableFields: string[] = ['teacherId', 'firstName', 'lastName', 'middleName', 'birthDate', 'style'];

  constructor(private _teacherService: TeacherService) {
    this.teachers = this._teacherService.getTeachers();
  }

}
