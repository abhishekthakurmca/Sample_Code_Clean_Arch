import { Component, OnInit, Input } from "@angular/core";
import {
    FreightCompanyClient,
    GridQuery,
    GridResultOfFreightCompany,
    ExportClient,
} from "../../Anubis-api";
import { Title } from "@angular/platform-browser";
import { ToastrService } from "ngx-toastr";
import { GlobalConstants } from "src/anubis-api/Common/GlobalConstants";
import { LodderService } from "../../lodder.service";
import { ReloadService } from "../../reload.service";

@Component({
    selector: "app-freight-company",
    templateUrl: "./freight-company.component.html",
    styleUrls: ["./freight-company.component.css"],
})
export class FreightcompanyComponent implements OnInit {
    @Input() PageRecord: number = GlobalConstants.PerPageRecords;
    vm: GridResultOfFreightCompany;
    showActiveCompanies: boolean = true;

    constructor(
        private titleService: Title,
        private companyClient: FreightCompanyClient,
        private toastr: ToastrService,
        private exportClient: ExportClient,
        private lodder: LodderService,
        private reloadService: ReloadService
    ) {
        titleService.setTitle("Freight Company");
    }

    ngOnInit(): void {
        this.query = { ...this.query, ...this.init };
        this.query.filter.isActive = "true";
        this.reload();
        
    }

    init: any = {
        sort: "id",
        pageSize: this.PageRecord,
    };

    query: GridQuery = <GridQuery>{
        page: 1,
        pageSize: null,
        filter: {},
        ascending: false,
        sort: "",
    };

    gridUpdate(e: any): void {
        this.query = { ...this.query, ...e };
        this.query.pageSize = this.PageRecord;
        this.reload();
    }

    reload(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.query["isActive"] = this.showActiveCompanies;
        this.companyClient.getCompanys(this.query).subscribe(
            (response) => {
                this.vm = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }
    export(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.query.pageSize = this.vm.total;
        this.exportClient.exportCompany(this.query).subscribe((result) => {
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

    toggleActiveCompanies(): void {
        this.showActiveCompanies = !this.showActiveCompanies;
        this.query.filter.isActive = this.showActiveCompanies ? "true" : null;
        this.query.pageSize = this.PageRecord;
        this.reload();
    }
}
