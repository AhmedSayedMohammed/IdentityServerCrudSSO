import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrimengComponentsModule } from '../primeng-components/primeng-components.module';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { SharedComponentsModule } from 'src/app/shared/shared-components/shared-components.module';

@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    PrimengComponentsModule,
    ReactiveFormsModule,
    SharedComponentsModule,
    FormsModule,
    PrimengComponentsModule

  ]
})
export class AuthModule { }
