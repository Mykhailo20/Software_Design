import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TeacherTableComponent } from './components/teacher-table/teacher-table.component';
import { TeacherService } from './services/teacher.service';
import { TeachingStylePipe } from './pipes/teaching-style.pipe';
import { BlueBackgroundDirective } from './directives/blue-background.directive';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    TeacherTableComponent,
    TeachingStylePipe,
    BlueBackgroundDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [TeacherService],
  bootstrap: [AppComponent]
})
export class AppModule { }
