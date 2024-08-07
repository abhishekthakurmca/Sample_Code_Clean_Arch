import { Injectable } from "@angular/core";
import { AuthorizeService } from "../../api-authorization/authorize.service";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable({
    providedIn: "root",
})
export class CookieService {
    userId: any;
    userName : any
    cookieExpireTime = new Date();
    private gridDataSubject = new BehaviorSubject<{ [key: string]: any }>({});
    gridData$ = this.gridDataSubject.asObservable();

    constructor(private authService: AuthorizeService) { }

    setCookie(name, value, cookieExpireTime = new Date()) {
        const encodedCookieValue = btoa(value);
        document.cookie = `${name}=${encodedCookieValue}; expires=${cookieExpireTime.toUTCString()}; path=/`;
    }
    getCookieBykey(key) {
        let match = document.cookie.match(new RegExp('(^| )' + key + '=([^;]+)'));
        if (match && match.length >= 2) return match[2];
        return ""
    }

    getUserId() {
        this.authService.getUser().subscribe((userId) => {
            this.userId = userId["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"] || [];
        });
        return this.userId
    }

    expandGrid(gridName: string) {
        this.getUserId()
        this.cookieExpireTime.setTime(this.cookieExpireTime.getTime() + (30 * 24 * 60 * 60 * 1000))
        let cookieValue = this.getCookieBykey("DashBoardGridState_" + this.userId);
        if (cookieValue) {
            cookieValue = atob(cookieValue);

            if (cookieValue.indexOf(gridName) !== -1) {
                cookieValue = cookieValue.replace("," + gridName, "");
            } else {
                cookieValue += "," + gridName;
            }
        } else {
            cookieValue = "," + gridName;
        }
        this.setCookie("DashBoardGridState_" + this.userId, cookieValue, this.cookieExpireTime);
    }

    removeCookie(cookieName: string) {
        document.cookie = `${cookieName}=; expires=${this.cookieExpireTime.toUTCString()}; path=/;`;
    }

    getUserName() {
        this.authService.getUser().subscribe((userName) => {
            this.userName = userName["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || [];
        });
        return this.userName
    }
}