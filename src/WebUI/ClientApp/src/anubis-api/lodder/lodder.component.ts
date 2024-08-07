import { Component, Input } from "@angular/core";
import { Subject } from "rxjs";
import { LodderService } from "../../anubis-api/lodder.service";

@Component({
    selector: "app-lodder",
    templateUrl: "./lodder.component.html",
    styleUrls: ["./lodder.component.css"],
})
export class LodderComponent {
    @Input() loading: Subject<boolean> = this.lodder.loading;
    constructor(private lodder: LodderService) {}
}
