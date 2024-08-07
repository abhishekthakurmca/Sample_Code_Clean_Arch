import { Component, OnInit, Input, TemplateRef } from "@angular/core";
import {
    FreightCompanyClient,
    GridQuery,
    CreateFTLCommand,
    RemoveAndDeactiveFTLCommand,
    ExportClient,
    OptionVm,
    GridResultOfFtlDTO,
} from "../../Anubis-api";
import { ActivatedRoute } from "@angular/router";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";
import { Title } from "@angular/platform-browser";
import { GlobalConstants } from "src/anubis-api/Common/GlobalConstants";
import { LodderService } from "../../lodder.service";
import { ReloadService } from "../../reload.service";
import { FileUploadService } from "../../file-upload.service";

@Component({
    selector: "app-ftl",
    templateUrl: "./ftl.component.html",
    styleUrls: ["./ftl.component.css"],
})
export class FTLComponent implements OnInit {
    @Input() PageRecord: number = GlobalConstants.PerPageRecords;
    @Input() queryPrams: number;

    ftldetails: GridResultOfFtlDTO;
    addFtlModal: BsModalRef;
    deleteModal: BsModalRef;
    uploadModal: BsModalRef;
    confirmModal: BsModalRef;

    config = {
        backdrop: false,
        ignoreBackdropClick: true,
    };

    submitted: boolean = false;
    errorList: any[] = [];
    ftlform: FormGroup;
    OriginCity: string;
    DestinationCity: string;
    Price: number;
    states: any[] = [];
    editid: string;

    public keyword = "name";
    public placeholder: string = "State Name";
    fileUpload = new FormControl(null, Validators.required);
    truckSizes: OptionVm[];
    constructor(
        private formBuilder: FormBuilder,
        private companyClient: FreightCompanyClient,
        private route: ActivatedRoute,
        private modalService: BsModalService,
        private toastr: ToastrService,
        private titleService: Title,
        private exportClient: ExportClient,
        private lodder: LodderService,
        private fileService: FileUploadService,
        private reloadService: ReloadService
    ) {
        titleService.setTitle("FTL");
        this.ftlform = this.formBuilder.group({
            originCity: ["", Validators.required],
            destinationCity: ["", Validators.required],
            originState: ["", Validators.required],
            destinationState: ["", Validators.required],
            price: ["",  Validators.pattern(/^\d+(\.\d{1,2})?$/)],
            truckSize: ["0"]
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
        this.getState();
        this.getTruckList();
    }

    get form() {
        return this.ftlform.controls;
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
        this.companyClient.getFTL(this.query).subscribe(
            (response) => {
                this.ftldetails = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }

    getState() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getCompanyBasicInformation("basic").subscribe(
            (response) => {
                this.states = [];
                response.states.forEach((element) => {
                    let state = { id: +element.id, name: element.abbr };
                    this.states.push(state);
                });
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }

    selectOriginState(state: any) {
        this.ftlform.patchValue({
            originState: state.name,
        });
    }

    onClearOriginState() {
        this.ftlform.patchValue({
            originState: "",
        });
        this.ftlform.get("originState").enable();
    }

    selectDestinationState(state: any) {
        this.ftlform.patchValue({
            destinationState: state.name,
        });
    }

    onClearDestinationState() {
        this.ftlform.patchValue({
            destinationState: "",
        });
        this.ftlform.get("destinationState").enable();
    }

    addFtl() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.ftlform.invalid) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient
            .addEditFTL(<CreateFTLCommand>{
                id: 0,
                company_Id: this.queryPrams,
                originCity: this.form.originCity.value,
                destinationCity: this.form.destinationCity.value,
                originState: this.form.originState.value?.name,
                destinationState: this.form.destinationState.value?.name,
                isActive: true,
                isDeleted: false,
                price: this.form.price.value == ""
                ? 0 : this.form.price.value,
                truck_Id : +this.form.truckSize.value
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();

                    if (response.succeeded == true) {
                        this.toastr.success("SUCCESS");
                        this.reload();
                        this.submitted = false;
                        this.hideAddFtlModel();
                    } else if (
                        response.succeeded == false &&
                        response.errors.length > 0
                    ) {
                        this.toastr.error(response.errors.toString());
                    }
                },
                (error) => {
                    this.lodder.hideLodder();
                    this.toastr.error("ERROR");
                }
            );
    }

    updateFtl() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.ftlform.invalid) {
            return;
        }

        this.lodder.showLodder();
        this.companyClient
            .addEditFTL(<CreateFTLCommand>{
                id: parseInt(this.editid),
                company_Id: this.queryPrams,
                originCity: this.form.originCity.value,
                destinationCity: this.form.destinationCity.value,
                originState:
                    this.form.originState.value?.name == null
                        ? this.form.originState.value
                        : this.form.originState.value?.name,
                destinationState:
                    this.form.destinationState.value?.name == null
                        ? this.form.destinationState.value
                        : this.form.destinationState.value?.name,
                isActive: true,
                isDeleted: false,
                price: this.form.price.value == null
                ? 0 : this.form.price.value,
                truck_Id : +this.form.truckSize.value
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();

                    if (response.succeeded == true) {
                        this.toastr.success("SUCCESS");
                        this.reload();
                        this.submitted = false;
                        this.hideAddFtlModel();
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

    deleteFtl() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();

        this.companyClient
            .activeDeleteFTL(<RemoveAndDeactiveFTLCommand>{
                id: +this.editid,
                action: "delete",
                isDeleted: "true",
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

    deactivateFtl(id: string, activestatus: boolean) {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        if (activestatus == true) {
            activestatus = false;
        } else if (activestatus == false) {
            activestatus = true;
        }
        this.lodder.showLodder();
        this.companyClient
            .activeDeleteFTL(<RemoveAndDeactiveFTLCommand>{
                id: +id,
                action: "deactivate",
                isDeactive: activestatus,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    if (response.succeeded == true) {
                        this.toastr.success("SUCCESS");
                        this.reload();
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
        this.query.pageSize = this.ftldetails.total;
        this.exportClient.exportFTL(this.query).subscribe((result) => {
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

    exportAlllanes() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.query.pageSize = this.ftldetails.total;
        this.exportClient.exportFTLLanes(this.query).subscribe((result) => {
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

    showAddFtlModal(template: TemplateRef<any>, id: string, editdata: any) {
        this.submitted = false;
        this.addFtlModal = this.modalService.show(template, this.config);
        this.editid = id;
        if (this.editid != "") {
            const selectedOption = this.truckSizes.find(option => option.text === editdata.truckSize);
            this.ftlform.patchValue({
                originCity: editdata.originCity,
                destinationCity: editdata.destinationCity,
                price: editdata.price,
                originState: editdata.originState,
                destinationState: editdata.destinationState,
                truckSize: selectedOption ? selectedOption.value : 0
            });
        }
    }

    hideAddFtlModel() {        
        this.submitted = false;
        this.addFtlModal.hide();
        this.ftlform.get("originState").enable();
        this.ftlform.get("destinationState").enable();
        this.ftlform.patchValue({
            truckSize: 0,
            originState: "",
            destinationState: "",
            originCity : "",
            destinationCity : "",
            price : ""
        });
    }

    showDeleteModel(template: TemplateRef<any>, id: string) {
        this.deleteModal = this.modalService.show(template, this.config);
        this.editid = id;
    }

    hideDeleteModel() {
        this.deleteModal.hide();
    }

    import(template: TemplateRef<any>) {
        this.fileUpload.reset();
        this.errorList = [];
        this.uploadModal = this.modalService.show(template, this.config);
    }

    hideModal() {
        this.uploadModal.hide();
        this.fileUpload.reset();
    }

    onselectFile(files) {
        this.fileUpload.patchValue(files.item(0));
    }

    confirm(template: TemplateRef<any>) {
       if(this.ftldetails.total > 0){
           this.confirmModal = this.modalService.show(template, this.config);
           this.uploadModal.hide();
       }
       else{
        this.upload(); 
       }
    }

    hideConfirmModal() {
        this.confirmModal.hide();
        this.fileUpload.reset();
    }

    upload() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        if (this.fileUpload.invalid) {
            return;
        }
        this.lodder.showLodder();
        this.fileService
            .postFile(this.fileUpload.value, this.queryPrams, "FTL")
            .subscribe(
                (res) => {
                    this.lodder.hideLodder();
                    this.ftldetails.total > 0 ? this.hideConfirmModal() : this.uploadModal.hide();                    
                    this.reload();
                    if (res.succeeded) {
                        this.toastr.success("SUCCESS");
                    } else{
                        this.errorList = res.errors;
                        this.toastr.info(
                            "There are error in some rows while importing."
                        ); 
                    } 
                },
                (error) => {
                    this.lodder.hideLodder();
                }
            );
    }

    getTruckList() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        
    }

}
