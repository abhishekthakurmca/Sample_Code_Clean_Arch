import { Injectable, Input } from "@angular/core";
import { Observable, Subject } from "rxjs";

@Injectable({
    providedIn: "root",
})
export class LodderService {
    @Input() loading = new Subject<boolean>();
    constructor() {}

    showLodder() {
        this.loading.next(true);
    }

    hideLodder() {
        this.loading.next(false);
    }
}
