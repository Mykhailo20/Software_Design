import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TeacherTableComponent } from './components/teacher-table/teacher-table.component';
import { TeacherService } from './services/teacher.service';

@NgModule({
  declarations: [
    AppComponent,
    TeacherTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [TeacherService],
  bootstrap: [AppComponent]
})
export class AppModule { }
