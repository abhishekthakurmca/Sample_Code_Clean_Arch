import { PhoneMaskDirective } from "./phone-mask.directive";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ProfilesRoutingModule } from "./profiles-routing.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ModalModule } from "ngx-bootstrap/modal";
import { FreightcompanyComponent } from "./freight-company/freight-company.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { BsDatepickerModule } from "ngx-bootstrap/datepicker";
import { BrowserModule } from "@angular/platform-browser";
import { SharedModule } from "../shared/shared.module";
import { AddCompany } from "./add-company/add-company.component";
import { EditCompany } from "./edit-company/edit-company.component";
import { ContactComponent } from "./contact/contact.component";
import { FTLComponent } from "./ftl/ftl.component";
import { LTLComponent } from "./ltl/ltl.component";
import { WhiteGloveComponent } from "./white-glove/white-glove.component";
import { FreightCategoryComponent } from "./freight-category/freight-category.component";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { FreightCodeComponent } from "./freight-code/freight-code.component";
import { Tw0DigitsDecimal } from "./two-digits-decimal.directive";
import { ThreeDigitsDecimal } from "./three-digit-decimal.directive";
import { MatExpansionModule } from "@angular/material/expansion";
import { HistoricFreightRatesComponent } from "./historic-freight-rates/historic-freight-rates.component";
//import { TabsModule } from "ngx-bootstrap/tabs";
import { TabsModule } from "ngx-bootstrap/tabs";
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { PermissionsComponent } from './permissions/permissions.component';

@NgModule({
    declarations: [
        FreightcompanyComponent,
        AddCompany,
        EditCompany,
        ContactComponent,
        WhiteGloveComponent,
        FreightCategoryComponent,
        PhoneMaskDirective,
        Tw0DigitsDecimal,
        ThreeDigitsDecimal,
        FreightCodeComponent,
        HistoricFreightRatesComponent,
        PermissionsComponent,
    ],
    exports: [PhoneMaskDirective, Tw0DigitsDecimal, ThreeDigitsDecimal],
    imports: [
        MatProgressSpinnerModule,
        CommonModule,
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        MatExpansionModule,
        TabsModule.forRoot(),
        ModalModule.forRoot(),
        ProfilesRoutingModule,
        BrowserAnimationsModule,
        BsDatepickerModule.forRoot(),
        TimepickerModule.forRoot()
    ],
})
export class ProfilesModule {}
