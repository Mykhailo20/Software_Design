import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeacherTableComponent } from './components/teacher-table/teacher-table.component';
import { SkillTableComponent } from './components/skill-table/skill-table.component';

const routes: Routes = [
  { path: 'teachers', component: TeacherTableComponent },
  { path: 'skills', component: SkillTableComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [TeacherTableComponent, SkillTableComponent]
