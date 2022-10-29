import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { IVacancy } from '../../shared/wrapper/ivacancy';
import { MainService } from '../../service/main.service';
import { AuthService } from '../../service//auth.service';
import { IPaginatedResult } from 'src/app/shared/wrapper/IPaginatedResult';
import { FormControl, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-vacancy',
  templateUrl: './vacancy.component.html',
  styleUrls: ['./vacancy.component.css']
})
export class VacancyComponent implements OnInit {
  appForm = new FormGroup({
    name: new FormControl(''),
    responsibilities: new FormControl(''),
    jobCategory: new FormControl(''),
    skills: new FormControl(''),
  });
  vacancies:IVacancy[] = [] ;
  pageNumber!:number
  pagesize!:Number
  paginateResult!:IPaginatedResult<IVacancy>
  firstIndex = 0;
  rows = 4;
  constructor(private confirmationService:ConfirmationService,private service:MainService,private authService:AuthService,private router:Router,private messageService:MessageService) 
  { 
  }

  ngOnInit(): void {
    this.pageNumber=1
    this.service.getVacancyList(this.pageNumber,this.rows).subscribe({
      next: (res) => {

        this.vacancies = res.data;
        this.paginateResult=res
          },
      error: (e) => {
            this.messageService.add({
          severity: 'error',
          detail: e.error.Message,
        });
      },
      complete: () => {
  
      },
    });
  }
  deletevacancy(vacancy: IVacancy) {

    this.confirmationService.confirm({
      message:
        'Are you sure you want to delete ' +
        vacancy.name+
        '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.service.deleteVacancy(vacancy.id).subscribe({
          next: (v: any) => {
            this.vacancies = this.vacancies.filter((val) => val.id !== vacancy.id);

            console.log(vacancy);
            this.messageService.add({
              severity: 'success',
              summary: 'Successful',
              detail: 'Product Deleted',
              life: 3000,
            });
          },
          error: (e) => {},
          complete: () => {},
        });
      },
    });
  }
previous() {
    // Set first index of page to firstIndex - rows
    if ((this.firstIndex - this.rows) < 0) return;
    this.firstIndex = this.firstIndex - this.rows;
    this.pageNumber=this.pageNumber-1;
    this.service.getVacancyList(this.pageNumber,this.rows).subscribe({
      next: (res) => {

        this.vacancies = <IVacancy[]>res.data;
          },
      error: (e) => {
            this.messageService.add({
          severity: 'error',
          detail: e.error.Message,
        });
      },
      complete: () => {
  
      },
    });
}

next() {
    // Set first index of page to firstIndex + rows
    if ((this.firstIndex + this.rows) >
        this.paginateResult.count) return;
    this.firstIndex = this.firstIndex + this.rows;
    this.pageNumber=this.pageNumber+1;
    this.service.getVacancyList(this.pageNumber,this.rows).subscribe({
      next: (res) => {

        this.vacancies = <IVacancy[]>res.data;
          },
      error: (e) => {
            this.messageService.add({
          severity: 'error',
          detail: e.error.Message,
        });
      },
      complete: () => {
  
      },
    });
}

isFirst(): boolean {
    return this.vacancies ? 
        this.firstIndex === 0 : true;
}

isLast(): boolean {
    return this.vacancies ?
        (this.firstIndex + this.rows) >
        this.vacancies.length : true;
}
onSubmit(){}
}
