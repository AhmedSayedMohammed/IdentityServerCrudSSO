import{HttpClientModule} from '@angular/common/http'
import{FormsModule,ReactiveFormsModule} from '@angular/forms'
import { NgModule,CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { PrimengComponentsModule } from './components/primeng-components/primeng-components.module';
import { AppComponent } from './app.component';
import { VacancyComponent } from './components/vacancy/vacancy.component';
import { ConfirmationService } from 'primeng/api';
import {MessageService} from 'primeng/api';
import {ToastModule} from 'primeng/toast';
import { MainService } from './service/main.service';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './components/auth/login/login.component';
import { AddEditVacancyComponent } from './components/add-edit-vacancy/add-edit-vacancy.component';
@NgModule({
  declarations: [
    AppComponent,
    VacancyComponent,
    LoginComponent,
    AddEditVacancyComponent
   
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    PrimengComponentsModule,
    ToastModule,
    AppRoutingModule,
    BrowserAnimationsModule,
   
   
    
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: [MainService,MessageService,ConfirmationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
