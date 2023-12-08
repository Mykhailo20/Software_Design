import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TeacherTableComponent } from './components/teacher-table/teacher-table.component';
import { TeacherService } from './services/teacher.service';
import { TeachingStylePipe } from './pipes/teaching-style.pipe';
import { BlueBackgroundDirective } from './directives/blue-background.directive';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { headerInterceptor } from './interceptors/header.interceptor';


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
  providers: [
    TeacherService,
    provideHttpClient(withInterceptors([
      headerInterceptor
    ]))
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
