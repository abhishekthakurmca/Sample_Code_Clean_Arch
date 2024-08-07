import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { TableComponent } from "./table/table.component";
import { ModalComponent } from "./modal/modal.component";
import { ModalModule } from "ngx-bootstrap/modal";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { BsDatepickerModule } from "ngx-bootstrap/datepicker";
import { ConfirmButtonComponent } from "./confirm-button/confirm-button.component";
import { RouterModule } from "@angular/router";
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatExpansionModule } from "@angular/material/expansion";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { AutocompleteLibModule } from "angular-ng-autocomplete";
import { CalendarModule, DateAdapter } from "angular-calendar";
import { adapterFactory } from "angular-calendar/date-adapters/date-fns";
import { TimepickerModule } from "ngx-bootstrap/timepicker";
import { QRCodeModule } from "angularx-qrcode";
import { NgMultiSelectDropDownModule } from "ng-multiselect-dropdown";

@NgModule({
    declarations: [
        TableComponent,
        ModalComponent,
        ConfirmButtonComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        ReactiveFormsModule,
        MatAutocompleteModule,
        MatFormFieldModule,
        MatInputModule,
        MatDatepickerModule,
        MatExpansionModule,
        MatProgressSpinnerModule,
        AutocompleteLibModule,
        CalendarModule.forRoot({
            provide: DateAdapter,
            useFactory: adapterFactory,
        }),
        TimepickerModule.forRoot(),
        ModalModule.forRoot(),
        BrowserAnimationsModule,
        BsDatepickerModule.forRoot(),
        QRCodeModule,
        NgMultiSelectDropDownModule.forRoot()
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        BrowserAnimationsModule,
        TableComponent,
        ModalComponent,
        ConfirmButtonComponent,
        
        AutocompleteLibModule,
        
        QRCodeModule
       
    ],
})
export class SharedModule {}
