import { Component, Input } from "@angular/core";
import { ReloadService } from "../anubis-api/reload.service";

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
})
export class AppComponent {
    @Input() isSmartPage: boolean = false;
    loading: boolean = true;
    title = "app";

    constructor(private service: ReloadService) {
        this.service.pagetype$.subscribe((data) => {
            this.isSmartPage = data;
        });
    }
}
