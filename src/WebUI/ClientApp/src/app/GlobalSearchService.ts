import { Injectable, TemplateRef } from '@angular/core';
import { Subject } from 'rxjs';
@Injectable({
    providedIn: 'root'
})
export class GlobalSearchService {
    private callMainDashboardMethodSubject = new Subject<{ type: any, gridSearch: any, inputSearchValue: any }>();
    callMainDashboardMethod$ = this.callMainDashboardMethodSubject.asObservable();
    public autoScheduleDataSubject = new Subject<any>();

    private allExpandMethodSubject = new Subject();
    allExpandMethod$ = this.allExpandMethodSubject.asObservable();

    private allCollapseMethodSubject = new Subject();
    allCollapseMethodMethod$ = this.allCollapseMethodSubject.asObservable();

    private dockRefreshSubject = new Subject<{ isTeamCapacity: any }>();
    dockRefreshMethod$ = this.dockRefreshSubject.asObservable();

    constructor() { }
    triggerCallMainDashboardMethod(type: any, gridSearch: any, inputSearchValue: any) {
        this.callMainDashboardMethodSubject.next({ type, gridSearch, inputSearchValue });
    }

    triggerallExpandMethod() {
        this.allExpandMethodSubject.next({});
    }

    triggerallCollapseMethod() {
        this.allCollapseMethodSubject.next({});
    }

    sendAutoScheduleLoadData(data: any): void {
        this.autoScheduleDataSubject = data;
    }

    dockRefresh(isTeamCapacity) {
        this.dockRefreshSubject.next({ isTeamCapacity });
    }

}