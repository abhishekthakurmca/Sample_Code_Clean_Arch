import { Injectable, Input } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable, of, Subject, throwError } from "rxjs";
import { catchError, delay, map, take } from "rxjs/operators";
import { AuthorizeService } from "src/api-authorization/authorize.service";

import {
    GridQuery,
} from "../anubis-api/Anubis-api";
import { GlobalConstants } from "../anubis-api/Common/GlobalConstants";
import { ReturnStatement } from "@angular/compiler";

@Injectable({
    providedIn: "root",
})
export class ReloadService {
    @Input() PageRecord: number = GlobalConstants.PerPageRecords;

    @Input() sites: any[] = [];
    @Input() sites1: any[] = [];
    @Input() clients: any[] = [];
    @Input() receivingType: any;
    @Input() truckSizes: any;

    todo$: Observable<any>;
    private todoshipment = new Subject<any>();
    tendered$: Observable<any>;
    private tenderedShipment = new Subject<any>();
    dock$: Observable<any>;
    private dockschedule = new Subject<any>();
    dockerisite$: Observable<any>;
    private dockschedulerisite = new Subject<any>();
    reject$: Observable<any>;
    private rejectedshipment = new Subject<any>();
    approval$: Observable<any>;
    private acceptancewithapproval = new Subject<any>();
    site$: Observable<any>;
    private sitelist = new Subject<any>();
    pagetype$: Observable<any>;
    private isSmartPage = new Subject<any>();
    query: GridQuery = <GridQuery>{
        page: 1,
        pageSize: this.PageRecord,
        filter: {},
        ascending: false,
        sort: "",
    };
    queryTender: GridQuery = <GridQuery>{
        page: 1,
        pageSize: this.PageRecord,
        filter: {},
        ascending: false,
        sort: "",
    };
    queryTodo: GridQuery = <GridQuery>{
        page: 1,
        pageSize: null,
        filter: {},
        ascending: false,
        sort: "",
    };
    queryRejected: GridQuery = <GridQuery>{
        page: 1,
        pageSize: this.PageRecord,
        filter: {},
        ascending: false,
        sort: "",
    };
    queryApprovalPending: GridQuery = <GridQuery>{
        page: 1,
        pageSize: this.PageRecord,
        filter: {},
        ascending: false,
        sort: "",
    };

    init: any = {
        sort: "id",
        pageSize: this.PageRecord,
    };
    constructor(
        private toastr: ToastrService,
        private authorizeService: AuthorizeService,
        private route: Router
    ) {
        this.todo$ = this.todoshipment.asObservable();
        this.tendered$ = this.tenderedShipment.asObservable();
        this.dock$ = this.dockschedule.asObservable();
        this.dockerisite$ = this.dockschedulerisite.asObservable();
        this.reject$ = this.rejectedshipment.asObservable();
        this.approval$ = this.acceptancewithapproval.asObservable();
        this.site$ = this.sitelist.asObservable();
        this.pagetype$ = this.isSmartPage.asObservable().pipe(delay(0));
    }

    todo(data) {
        this.queryTodo = data;
    }
    tendered(data) {
        this.queryTender = data;
    }
    rejected(data) {
        this.queryRejected = data;
    }
    approvalPending(data) {
        this.queryApprovalPending = data;
    }


 

    refreshDockScheduleEriSite() {
        this.dockschedulerisite.next(true);
    }

    SmartPage() {
        this.isSmartPage.next(true);
    }
    checkAuthenticated(): Observable<boolean> {
        return this.authorizeService.isAuthenticated().pipe(
            take(1),
            map(isauthenticated => {
                if (!isauthenticated["__zone_symbol__value"] || isauthenticated["__zone_symbol__state"] === null) {
                    this.toastr.info(
                        "Your session is expired, you will be redirected to login screen in 10 seconds."
                    );
                    setTimeout(() => {
                        this.route.navigateByUrl("/authentication/login");
                    }, 10000);
                    return false;
                } else {
                    return true;
                }
            }),
            catchError(error => {
                this.toastr.error(
                    "Error during authentication check: " + error
                );
                return of(false);
            })
        );
    }


    
    
}
