import { Component, OnInit, Input, TemplateRef } from "@angular/core";
import {
    GridQuery,
    GridFTLQuery,
    CreateCategoryCommand,
    FreightCategoryClient,
    RemoveCategoryCommand,
    GridResultOfFreightCategory,
    ExportClient,
} from "../../Anubis-api";
import { ActivatedRoute } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";
import { GlobalConstants } from "src/anubis-api/Common/GlobalConstants";
import { LodderService } from "../../lodder.service";
import { Title } from "@angular/platform-browser";
import { ReloadService } from "../../reload.service";

@Component({
    selector: "app-freight-category",
    templateUrl: "./freight-category.component.html",
    styleUrls: ["./freight-category.component.css"],
})
export class FreightCategoryComponent implements OnInit {
    @Input() PageRecord: number = GlobalConstants.PerPageRecords;
    @Input() queryPrams: number;

    categorydetails: GridResultOfFreightCategory;
    addCatModel: BsModalRef;
    deleteModel: BsModalRef;
    config = {
        backdrop: false,
        ignoreBackdropClick: true,
    };
    submitted: boolean = false;

    categoryform: FormGroup;
    Id: number;
    Name: string;
    isDeleted: false;

    editid: string;

    constructor(
        private titleService: Title,
        private formBuilder: FormBuilder,
        private categoryClient: FreightCategoryClient,
        private route: ActivatedRoute,
        private modalService: BsModalService,
        private toastr: ToastrService,
        private exportClient: ExportClient,
        private lodder: LodderService,
        private reloadService: ReloadService
    ) {
        titleService.setTitle("Freight Category");
        this.categoryform = this.formBuilder.group({
            name: ["", Validators.required],
        });
    }

    ngOnInit(): void {
        this.query = { ...this.query, ...this.init };
        this.reload();
    }

    init: any = {
        sort: "id",
        pageSize: this.PageRecord,
    };

    get form() {
        return this.categoryform.controls;
    }

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
        this.categoryClient.getCategories(this.query).subscribe(
            (response) => {
                this.categorydetails = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }

    addFreightCatgory() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.categoryform.invalid) {
            return;
        }
        this.lodder.showLodder();
        this.categoryClient
            .addEditCategory(<CreateCategoryCommand>{
                id: 0,
                name: this.form.name.value,
                isDeleted: false,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    if (response.succeeded == true) {
                        this.categoryform.reset();
                        this.toastr.success("SUCCESS");
                        this.reload();
                        this.hideAddCatModel();
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

    updateFreightCatgory() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.categoryform.invalid) {
            return;
        }
        this.lodder.showLodder();
        this.categoryClient
            .addEditCategory(<CreateCategoryCommand>{
                id: parseInt(this.editid),
                name: this.form.name.value,
                isDeleted: false,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();

                    if (response.succeeded == true) {
                        this.categoryform.reset();
                        this.toastr.success("SUCCESS");
                        this.reload();
                        this.hideAddCatModel();
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

    deleteCategory() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();

        this.categoryClient
            .removeCategory(<RemoveCategoryCommand>{
                id: +this.editid,
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
    showAddCatModal(template: TemplateRef<any>, id: string, editdata: any) {
        this.addCatModel = this.modalService.show(template, this.config);
        this.editid = id;
        if (this.editid != "") {
            this.Name = editdata.name;
        }
    }

    hideAddCatModel() {
        this.addCatModel.hide();
        this.categoryform.reset();
        this.submitted = false;
    }

    showDeleteModel(template: TemplateRef<any>, id: string) {
        this.deleteModel = this.modalService.show(template, this.config);
        this.editid = id;
    }

    hideDeleteModel() {
        this.deleteModel.hide();
    }

}
