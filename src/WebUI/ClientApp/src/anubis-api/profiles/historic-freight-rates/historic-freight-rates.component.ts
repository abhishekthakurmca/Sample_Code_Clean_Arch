import { Component, OnInit, Input } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import {
    FreightCompanyClient,
    GetHistoricFreightRatesQuery,
    GridQuery,
    ExportClient,
} from "src/anubis-api/Anubis-api";
import { ToastrService } from "ngx-toastr";
import { LodderService } from "../../lodder.service";
import { ReloadService } from "../../reload.service";

@Component({
    selector: "app-historic-freight-rates",
    templateUrl: "./historic-freight-rates.component.html",
    styleUrls: ["./historic-freight-rates.component.css"],
})
export class HistoricFreightRatesComponent implements OnInit {
    @Input() queryPrams: number;
    @Input() freightRates: any;
    constructor(
        private route: ActivatedRoute,
        private companyClient: FreightCompanyClient,
        private toastr: ToastrService,
        private exportClient: ExportClient,
        private lodder: LodderService,
        private reloadService: ReloadService
    ) {}

    ngOnInit(): void {
        this.queryPrams = +this.route.snapshot.queryParamMap.get("id");
        this.init = { id: this.queryPrams, sort: "GrandTotal" };
        this.query = { ...this.query, ...this.init };
        this.getHistoricFreightRates();
    }

    init: any = {};

    query: GridQuery = <GridQuery>{
        page: 1,
        pageSize: 10,
        filter: {},
        ascending: false,
        sort: "",
        id: null,
    };

    gridUpdate(e: any): void {
        this.query = { ...this.query, ...e };
        this.getHistoricFreightRates();
    }

    getHistoricFreightRates() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getHistoricFreightRates(this.query).subscribe(
            (response) => {
                this.lodder.hideLodder();
                if (response.data) this.freightRates = response.data;
                else
                    this.toastr.info(
                        "Freight rates are not available for this company."
                    );
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }

    export() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.exportClient
            .exportHistoricFreightRates(this.query)
            .subscribe((result) => {
                let a = document.createElement("a");
                document.body.appendChild(a);
                a.style.display = "none";
                let url = URL.createObjectURL(result.data);
                a.href = url;
                a.download = result.fileName;
                a.click();
                window.URL.revokeObjectURL(url);
            });
    }
}
