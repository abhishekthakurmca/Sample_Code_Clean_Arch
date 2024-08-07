import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilesModule } from './profiles/profiles.module';
import { SharedModule } from './shared/shared.module';



@NgModule({
  declarations: [
    
  ],
  imports: [
    CommonModule,
    ProfilesModule,
    SharedModule
  ],
  exports: [
    SharedModule
  ]
})
export class AnubisApiModule { }