import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MainService } from '../../service/main.service';
import { IVacancy } from '../../shared/wrapper/ivacancy';
import {MessageService} from 'primeng/api';
@Component({
  selector: 'app-add-edit-vacancy',
  templateUrl: './add-edit-vacancy.component.html',
  styleUrls: ['./add-edit-vacancy.component.css']
})
export class AddEditVacancyComponent implements OnInit {
  IsAddPage!:boolean
  id!:any
  vacancy$:IVacancy= {} as IVacancy
 appForm = new FormGroup({
    name: new FormControl(''),
    responsibilities: new FormControl(''),
    jobCategory: new FormControl(''),
    skills: new FormControl(''),
  });
  constructor(private service:MainService,private activRoute:ActivatedRoute,private route:Router,private messageService:MessageService) { }

  ngOnInit(): void {
    this.id = this.activRoute.snapshot.paramMap.get('id');
    if(this.id==null)
    { 
      this.IsAddPage=true
      return
    }
    this.service.getVacancyById(this.id).subscribe({
      next: (res) => {

        this.vacancy$ = <IVacancy>res.data;
        this.appForm.patchValue({
          jobCategory:this.vacancy$.jobCategory,
          responsibilities:this.vacancy$.responsibilities,
          name:this.vacancy$.name,
          skills:this.vacancy$.skills,
        })
        console.log(this.vacancy$)
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
onSubmit()
{
  this.vacancy$.name=this.appForm.value.name as string
  this.vacancy$.skills=this.appForm.value.skills as string
  this.vacancy$.responsibilities=this.appForm.value.responsibilities as string
  this.vacancy$.jobCategory=this.appForm.value.jobCategory as string
  if(this.IsAddPage)
  {
    this.service.addVacancy(this.vacancy$).subscribe({
      next: (res) => {
          },
      error: (e) => {
        console.log(e)
        this.messageService.add({
          severity: 'error',
          detail: e.error.Message,
        });
      },
      complete: () => {
        this.messageService.add({
          severity: 'success',
          detail: "Added Successfully",
        });
        setTimeout(() => {
          this.route.navigate(['/vacancies']);
      }, 1000);  //5s
      },
    });
  
    return   
  }
  this.service.editVacancy(this.vacancy$).subscribe({
  next: (res) => {
      },
  error: (e) => {
    console.log(e)
    this.messageService.add({
      severity: 'error',
      detail: e.error.Message,
    });
  },
  complete: () => {
    this.messageService.add({
      severity: 'success',
      detail: "Edited Successfully",
    });

    setTimeout(() => {
      this.route.navigate(['/vacancies']);
  }, 1000);  //5s

  },
});

}
}
