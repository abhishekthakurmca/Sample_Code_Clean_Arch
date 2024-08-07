import {
    GridFreightCodeQuery,
    FreightCompanyClient,
    GridQuery,
    FreightCodeCommand,
    RemoveFreightCodeCommand,
    ExportClient,
    GridResultOfFreightCompanyCodes,
} from "../../Anubis-api";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { ActivatedRoute } from "@angular/router";
import { Component, OnInit, Input, TemplateRef } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { GlobalConstants } from "src/anubis-api/Common/GlobalConstants";
import { LodderService } from "../../lodder.service";
import { ReloadService } from "../../reload.service";

@Component({
    selector: "app-freight-code",
    templateUrl: "./freight-code.component.html",
    styleUrls: ["./freight-code.component.css"],
})
export class FreightCodeComponent implements OnInit {
    @Input() PageRecord: number = GlobalConstants.PerPageRecords;
    @Input() queryPrams: number;

    freightcodedetails: GridResultOfFreightCompanyCodes;
    codeAddModal: BsModalRef;
    deleteModal: BsModalRef;

    config = {
        backdrop: false,
        ignoreBackdropClick: true,
    };

    submitted: boolean = false;
    editid: string;
    freightCategorys: any;
    codeform: FormGroup;
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private companyClient: FreightCompanyClient,
        private modalService: BsModalService,
        private toastr: ToastrService,
        private exportClient: ExportClient,
        private lodder: LodderService,
        private reloadService: ReloadService
    ) {
        this.codeform = this.formBuilder.group({
            name: ["", Validators.required],
            shortdescription: ["", Validators.required],
            description: ["", Validators.required],
            freightCategory_Id: ["0", [Validators.required, Validators.min(1)]],
            defaultprice: ["", [Validators.required]],
        });
    }

    ngOnInit(): void {
        this.queryPrams = +this.route.snapshot.queryParamMap.get("id");
        this.init = {
            id: this.queryPrams,
            sort: "id",
            pageSize: this.PageRecord,
        };
        this.query = { ...this.query, ...this.init };
        this.reload();
    }
    get form() {
        return this.codeform.controls;
    }

    init: any = {};

    query: GridQuery = <GridQuery>{
        page: 1,
        pageSize: null,
        filter: {},
        ascending: false,
        sort: "",
        id: null,
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
        this.companyClient.getFreightCompanyCodes(this.query).subscribe(
            (response) => {
                this.freightcodedetails = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
        this.companyClient
            .getCompanyBasicInformation("freightcategory")
            .subscribe(
                (response) => {
                    this.freightCategorys = response.freightCategories;
                },
                (error) => {
                    this.lodder.hideLodder();
                    this.toastr.error("ERROR");
                }
            );
    }

    addFreightCode() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.codeform.invalid) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient
            .addEditFreightCode(<FreightCodeCommand>{
                id: 0,
                freightCategory_Id: +this.form.freightCategory_Id.value,
                company_Id: this.queryPrams,
                name: this.form.name.value,
                shortDescription: this.form.shortdescription.value,
                description: this.form.description.value,
                defaultPrice: this.form.defaultprice.value,
                isDeleted: false,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();

                    if (response.succeeded == true) {
                        this.toastr.success("SUCCESS");
                        this.reload();
                        this.submitted = false;
                        this.hideAddModel();
                    } else if (
                        response.succeeded == false &&
                        response.errors.length > 0
                    ) {
                        this.toastr.info(response.errors.toString());
                    }
                },
                (error) => {
                    this.lodder.hideLodder();
                    this.toastr.error("ERROR");
                }
            );
    }

    updateFreightCode() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.codeform.invalid) {
            return;
        }
        this.lodder.showLodder();

        this.companyClient
            .addEditFreightCode(<FreightCodeCommand>{
                id: +this.editid,
                freightCategory_Id: +this.form.freightCategory_Id.value,
                company_Id: this.queryPrams,
                name: this.form.name.value,
                shortDescription: this.form.shortdescription.value,
                description: this.form.description.value,
                defaultPrice: this.form.defaultprice.value,
                isDeleted: false,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();

                    if (response.succeeded == true) {
                        this.toastr.success("SUCCESS");
                        this.reload();
                        this.submitted = false;
                        this.hideAddModel();
                    } else if (
                        response.succeeded == false &&
                        response.errors.length > 0
                    ) {
                        this.toastr.info(response.errors.toString());
                    }
                },
                (error) => {
                    this.lodder.hideLodder();
                    this.toastr.error("ERROR");
                }
            );
    }

    deletefreightcode() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();

        this.companyClient
            .removeCompanyCode(<RemoveFreightCodeCommand>{
                freightCodeId: +this.editid,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    if (response.succeeded == true) {
                        this.toastr.success("SUCCESS");
                        this.reload();
                        this.hideDeleteModel();
                    } else if (
                        response.succeeded == false &&
                        response.errors.length > 0
                    ) {
                        this.toastr.error(response.errors.toString(), "ERROR");
                    }
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
        this.query.pageSize = this.freightcodedetails.total;
        this.exportClient.exportFreightCode(this.query).subscribe((result) => {
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

    showAddModal(template: TemplateRef<any>, id: string, editdata: any) {
        this.codeAddModal = this.modalService.show(template, this.config);
        this.editid = id;
        if (this.editid != "") {
            this.codeform.patchValue({
                name: editdata.name,
                shortdescription: editdata.shortDescription,
                description: editdata.description,
                freightCategory_Id: editdata.freightCategory_Id,
                defaultprice: editdata.defaultPrice,
            });
        }
    }

    hideAddModel() {
        this.codeAddModal.hide();
        this.codeform.reset();
        this.submitted = false;
    }

    showDeleteModel(template: TemplateRef<any>, id: string) {
        this.deleteModal = this.modalService.show(template, this.config);
        this.editid = id;
    }

    hideDeleteModel() {
        this.deleteModal.hide();
    }
}
