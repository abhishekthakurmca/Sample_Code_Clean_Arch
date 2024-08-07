import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { ApiAuthorizationModule } from "src/api-authorization/api-authorization.module";
import { AuthorizeGuard } from "src/api-authorization/authorize.guard";
import { AuthorizeInterceptor } from "src/api-authorization/authorize.interceptor";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ModalModule } from "ngx-bootstrap/modal";
import { SidebarMenuComponent } from "./sidebar-menu/sidebar-menu.component";

import { from } from "rxjs";
import { ToastrModule } from "ngx-toastr";
import { CommonNavBarComponent } from "./common-nav-bar/common-nav-bar.component";
// import { MatMomentDateModule } from "@angular/material-moment-adapter";
import { TabsModule } from "ngx-bootstrap/tabs";
import { LodderComponent } from "../anubis-api/lodder/lodder.component";
import { LodderService } from "../anubis-api/lodder.service";
import { ReloadService } from "../anubis-api/reload.service";
import { FileUploadService } from "../anubis-api/file-upload.service";
import { TimepickerModule } from "ngx-bootstrap/timepicker";
import { DatePipe } from "@angular/common";
import { TeamMembersComponent } from "../anubis-api/team-members/team-members.component";
import { AnubisApiModule } from "src/anubis-api/anubis-api.module";
@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        SidebarMenuComponent,
        CommonNavBarComponent,
        LodderComponent,
        TeamMembersComponent
    ],

    imports: [
        BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
        FontAwesomeModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        ApiAuthorizationModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        RouterModule.forRoot([
            { path: "", redirectTo: "/freight-company", pathMatch: "full" },
            
            
            {
                path: "team-members",
                component: TeamMembersComponent,
                canActivate: [AuthorizeGuard],
            },
           
           
            


            
        ]),
        ModalModule.forRoot(),
        TabsModule.forRoot(),
        TimepickerModule.forRoot(),
        AnubisApiModule
    ],
    providers: [
        LodderService,
        ReloadService,
        DatePipe,
        FileUploadService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthorizeInterceptor,
            multi: true,
        },
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
