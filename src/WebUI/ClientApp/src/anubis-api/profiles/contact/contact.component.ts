import {
    FreightCompanyClient,
    GridCompanyContactQuery,
    GridQuery,
    CreateCompanyContactCommand,
    RemoveContactCommand,
    ExportClient,
    GridResultOfCompanyContactDto,
} from "../../Anubis-api";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { ActivatedRoute } from "@angular/router";
import { Component, OnInit, Input, TemplateRef } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { Title } from "@angular/platform-browser";
import { GlobalConstants } from "src/anubis-api/Common/GlobalConstants";
import { LodderService } from "../../lodder.service";
import { ReloadService } from "../../reload.service";

@Component({
    selector: "app-contact",
    templateUrl: "./contact.component.html",
    styleUrls: ["./contact.component.css"],
})
export class ContactComponent implements OnInit {
    @Input() queryPrams: number;
    @Input() PageRecord: number = GlobalConstants.PerPageRecords;
    contactdetails: GridResultOfCompanyContactDto;
    contactModel: BsModalRef;
    deleteModel: BsModalRef;

    config = {
        backdrop: false,
        ignoreBackdropClick: true,
    };

    submitted: boolean = false;

    contactform: FormGroup;
    clientcontacttypes: any;
    editid: string;
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private companyClient: FreightCompanyClient,
        private modalService: BsModalService,
        private toastr: ToastrService,
        private titleService: Title,
        private exportClient: ExportClient,
        private lodder: LodderService,
        private reloadService: ReloadService
    ) {
        titleService.setTitle("Contact");
        this.contactform = this.formBuilder.group({
            firstname: ["", [Validators.required, Validators.maxLength(100)]],
            lastname: ["", [Validators.required, Validators.maxLength(100)]],
            title: ["", [Validators.required, Validators.maxLength(100)]],
            type: ["0", [Validators.maxLength(100), Validators.min(1)]],
            mobile: ["", [Validators.minLength(14), Validators.maxLength(15)]],
            extention: [],
            fax: [""],
            email: [
                "",
                [
                    Validators.required,
                    Validators.maxLength(100),
                    Validators.email,
                    Validators.pattern("[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$"),
                ],
            ],
            notes: ["", Validators.maxLength(256)],
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
        return this.contactform.controls;
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

    showModal(template: TemplateRef<any>, id: string, editdata: any) {
        this.contactModel = this.modalService.show(template, this.config);
        this.editid = id;
        if (this.editid != "") {
            this.contactform.patchValue({
                type: editdata.contactType_Id,
                firstname: editdata.fName,
                lastname: editdata.lName,
                title: editdata.title,
                mobile: editdata.mobile,
                fax: editdata.fax,
                extention: editdata.ext,
                email: editdata.email,
                notes: editdata.notes,
            });
        }
    }

    hideModel() {
        this.contactModel.hide();
        this.submitted = false;
        this.contactform.reset();
    }

    showDeleteModel(template: TemplateRef<any>, id: string) {
        this.deleteModel = this.modalService.show(template, this.config);
        this.editid = id;
    }
    hideDeleteModel() {
        this.deleteModel.hide();
    }

    reload(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getCompanyContacts(this.query).subscribe(
            (response) => {
                this.contactdetails = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
        this.companyClient.getCompanyBasicInformation("contact").subscribe(
            (response) => {
                this.clientcontacttypes = response.clientContactTypes;
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }

    addContact() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.contactform.invalid) {
            return;
        }
        this.lodder.showLodder();

        this.companyClient
            .addCompanyContact(<CreateCompanyContactCommand>{
                id: 0,
                company_Id: this.queryPrams,
                contactType_Id: parseInt(this.form.type.value),
                fName: this.form.firstname.value,
                lName: this.form.lastname.value,
                title: this.form.title.value,
                mobile: this.form.mobile.value,
                fax: this.form.fax.value,
                ext: this.form.extention.value,
                email: this.form.email.value,
                notes: this.form.notes.value,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    if (response.succeeded == true) {
                        this.toastr.success("SUCCESS");
                        this.reload();
                        this.hideModel();
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

    updateContact() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.contactform.invalid) {
            return;
        }

        this.lodder.showLodder();

        this.companyClient
            .addCompanyContact(<CreateCompanyContactCommand>{
                id: parseInt(this.editid),
                company_Id: this.queryPrams,
                contactType_Id: parseInt(this.form.type.value),
                fName: this.form.firstname.value,
                lName: this.form.lastname.value,
                title: this.form.title.value,
                mobile: this.form.mobile.value,
                fax: this.form.fax.value,
                ext: this.form.extention.value,
                email: this.form.email.value,
                notes: this.form.notes.value,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    if (response.succeeded == true) {
                        this.toastr.success("SUCCESS");
                        this.reload();
                        this.hideModel();
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

    deleteContact() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();

        this.companyClient
            .removeContact(<RemoveContactCommand>{
                contactId: +this.editid,
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

    export(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.query.pageSize = this.contactdetails.total;
        this.exportClient.exportContact(this.query).subscribe((result) => {
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

    // validator functions
    ValidNumberSymbol(event: any) {
        var charCode = event.which ? event.which : event.keyCode;
        if (charCode < 33 || charCode > 57) return false;

        return true;
    }
}
