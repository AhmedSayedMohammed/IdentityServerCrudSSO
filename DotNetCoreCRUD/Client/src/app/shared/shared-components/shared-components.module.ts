import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { PrimengComponentsModule } from 'src/app/components/primeng-components/primeng-components.module';



@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PrimengComponentsModule,

  ],
  exports:[
    ReactiveFormsModule,

  ]
})
export class SharedComponentsModule { }
