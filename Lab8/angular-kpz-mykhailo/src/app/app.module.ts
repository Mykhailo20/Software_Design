import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { TeacherService } from './services/teacher.service';
import { TeachingStylePipe } from './pipes/teaching-style.pipe';
import { BlueBackgroundDirective } from './directives/blue-background.directive';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { headerInterceptor } from './interceptors/header.interceptor';
import { SkillService } from './services/skill.service';


@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
    TeachingStylePipe,
    BlueBackgroundDirective,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    TeacherService,
    SkillService,
    provideHttpClient(withInterceptors([
      headerInterceptor
    ]))
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
