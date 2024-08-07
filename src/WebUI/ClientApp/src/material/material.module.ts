import { NgModule } from '@angular/core';
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";

@NgModule({
    exports: [
        MatDatepickerModule,
        MatAutocompleteModule,
        MatFormFieldModule,
        MatProgressSpinnerModule,
        MatInputModule
    ]
  })
  export class MaterialModule { }