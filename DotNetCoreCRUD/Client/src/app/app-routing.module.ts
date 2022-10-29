import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEditVacancyComponent } from './components/add-edit-vacancy/add-edit-vacancy.component';
import { VacancyComponent } from './components/vacancy/vacancy.component';


const routes: Routes = [
  {path:'', redirectTo:'/vacancies', pathMatch:'full'},
  {path:'vacancies', component:VacancyComponent},
  {path:'vacancies/form/vacancy/edit/:id', component:AddEditVacancyComponent},
  {path:'vacancies/form/vacancy/add', component:AddEditVacancyComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
