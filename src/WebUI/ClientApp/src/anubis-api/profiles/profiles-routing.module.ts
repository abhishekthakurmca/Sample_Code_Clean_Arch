
import { FreightCodeComponent } from "./freight-code/freight-code.component";
import { LTLComponent } from "./ltl/ltl.component";
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthorizeGuard } from "../../api-authorization/authorize.guard";
import { FreightcompanyComponent } from "./freight-company/freight-company.component";
import { AddCompany } from "./add-company/add-company.component";
import { EditCompany } from "./edit-company/edit-company.component";
import { ContactComponent } from "./contact/contact.component";
import { FTLComponent } from "./ftl/ftl.component";
import { WhiteGloveComponent } from "./white-glove/white-glove.component";
import { FreightCategoryComponent } from "./freight-category/freight-category.component";
import { HistoricFreightRatesComponent } from "./historic-freight-rates/historic-freight-rates.component";
import { PermissionsComponent } from "./permissions/permissions.component";
const routes: Routes = [
  {
    path: "freight-company",
    component: FreightcompanyComponent,
    canActivate: [AuthorizeGuard],
  },
  {
    path: "freight-category",
    component: FreightCategoryComponent,
    canActivate: [AuthorizeGuard],
  },
  { path: "add-company", component: AddCompany, canActivate: [AuthorizeGuard] },
  {
    path: "edit-company",
    component: EditCompany,
    canActivate: [AuthorizeGuard],
  },
  {
    path: "contact",
    component: ContactComponent,
    canActivate: [AuthorizeGuard],
  },
  {
    path: "ftl",
    component: FTLComponent,
    canActivate: [AuthorizeGuard],
  },
  {
    path: "ltl",
    component: LTLComponent,
    canActivate: [AuthorizeGuard],
  },
  {
    path: "freight-code",
    component: FreightCodeComponent,
    canActivate: [AuthorizeGuard],
  },
  {
    path: "white-glove",
    component: WhiteGloveComponent,
    canActivate: [AuthorizeGuard],
  },
  
  {
    path: "historic-freight",
    component: HistoricFreightRatesComponent,
    canActivate: [AuthorizeGuard],
  },
  
 
  {
    path: "permissions",
    component: PermissionsComponent,
    canActivate: [AuthorizeGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProfilesRoutingModule {}
