import { Component, OnInit, Input, TemplateRef } from "@angular/core";
import {
    FreightCompanyClient,
    GridQuery,
    CreateLTLCommand,
    RemoveAndDeactiveLTLCommand,
    ExportClient,
    OptionVm,
    GridResultOfLtlDto
} from "../../Anubis-api";
import { ActivatedRoute } from "@angular/router";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { Title } from "@angular/platform-browser";
import { GlobalConstants } from "src/anubis-api/Common/GlobalConstants";
import { LodderService } from "../../lodder.service";
import { ReloadService } from "../../reload.service";
import { FileUploadService } from "../../file-upload.service";

@Component({
    selector: "app-ltl",
    templateUrl: "./ltl.component.html",
    styleUrls: ["./ltl.component.css"],
})
export class LTLComponent implements OnInit {
    @Input() PageRecord: number = GlobalConstants.PerPageRecords;
    @Input() queryPrams: number;

    ltldetails: GridResultOfLtlDto;
    addLtlModel: BsModalRef;
    deleteModel: BsModalRef;
    confirmModal: BsModalRef;
    uploadModal: BsModalRef;
    config = {
        backdrop: false,
        ignoreBackdropClick: true,
    };

    submitted: boolean = false;
    validateprice: boolean = false;
    ltlform: FormGroup;
    states: any[] = [];
    editid: string;
    errorList: any[] = [];
    public keyword = "name";
    public placeholder: string = "State Name";
    fileUpload = new FormControl(null, Validators.required);
    truckSizes: OptionVm[];
    constructor(
        private titleService: Title,
        private formBuilder: FormBuilder,
        private companyClient: FreightCompanyClient,
        private route: ActivatedRoute,
        private modalService: BsModalService,
        private toastr: ToastrService,
        private exportClient: ExportClient,
        private lodder: LodderService,
        private fileService: FileUploadService,
        private reloadService: ReloadService
    ) {
        titleService.setTitle("LTL");
        this.ltlform = this.formBuilder.group({
            origincity: ["", Validators.required],
            destinationcity: ["", Validators.required],
            originState: ["", Validators.required],
            destinationState: ["", Validators.required],
            pprice1: [],
            pprice2: [],
            pprice3: [],
            pprice4: [],
            pprice5: [],
            pprice6: [],
            pprice7: [],
            pprice8: [],
            pprice9: [],
            pprice10: [],
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
        return this.ltlform.controls;
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
        this.companyClient.getLTL(this.query).subscribe(
            (response) => {
                this.ltldetails = response;
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
        this.ltlform.patchValue({
            originState: state.name,
        });
    }

    onClearOriginState() {
        this.ltlform.patchValue({
            originState: "",
        });
        this.ltlform.get("originState").enable();
    }

    selectDestinationState(state: any) {
        this.ltlform.patchValue({
            destinationState: state.name,
        });
    }

    onClearDestinationState() {
        this.ltlform.patchValue({
            destinationState: "",
        });
        this.ltlform.get("destinationState").enable();
    }
    addLtl() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;

        if (this.ltlform.invalid) {
            return;
        }

        if (
            (this.form.pprice1.value != null && this.form.pprice1.value > 0) ||
            (this.form.pprice2.value != null && this.form.pprice2.value > 0) ||
            (this.form.pprice3.value != null && this.form.pprice3.value > 0) ||
            (this.form.pprice4.value != null && this.form.pprice4.value > 0) ||
            (this.form.pprice5.value != null && this.form.pprice5.value > 0) ||
            (this.form.pprice6.value != null && this.form.pprice6.value > 0) ||
            (this.form.pprice7.value != null && this.form.pprice7.value > 0) ||
            (this.form.pprice8.value != null && this.form.pprice8.value > 0) ||
            (this.form.pprice9.value != null && this.form.pprice9.value > 0) ||
            (this.form.pprice10.value != null && this.form.pprice10.value > 0)
        ) {
            this.validateprice = false;
            this.lodder.showLodder();
            this.companyClient
                .addEditLTL(<CreateLTLCommand>{
                    id: 0,
                    company_Id: this.queryPrams,
                    originCity:
                        this.form.origincity.value == ""
                            ? "Default"
                            : this.form.origincity.value == null
                            ? "Default"
                            : this.form.origincity.value,
                    destinationCity:
                        this.form.destinationcity.value == ""
                            ? "Default"
                            : this.form.destinationcity.value == null
                            ? "Default"
                            : this.form.destinationcity.value,
                    originState: this.form.originState.value?.name,
                    destinationState: this.form.destinationState.value?.name,
                    isActive: true,
                    isDeleted: false,
                    pPrice1:
                        this.form.pprice1.value == null
                            ? 0
                            : this.form.pprice1.value,
                    pPrice2:
                        this.form.pprice2.value == null
                            ? 0
                            : this.form.pprice2.value,
                    pPrice3:
                        this.form.pprice3.value == null
                            ? 0
                            : this.form.pprice3.value,
                    pPrice4:
                        this.form.pprice4.value == null
                            ? 0
                            : this.form.pprice4.value,
                    pPrice5:
                        this.form.pprice5.value == null
                            ? 0
                            : this.form.pprice5.value,
                    pPrice6:
                        this.form.pprice6.value == null
                            ? 0
                            : this.form.pprice6.value,
                    pPrice7:
                        this.form.pprice7.value == null
                            ? 0
                            : this.form.pprice7.value,
                    pPrice8:
                        this.form.pprice8.value == null
                            ? 0
                            : this.form.pprice8.value,
                    pPrice9:
                        this.form.pprice9.value == null
                            ? 0
                            : this.form.pprice9.value,
                    pPrice10:
                        this.form.pprice10.value == null
                            ? 0
                            : this.form.pprice10.value,
                    truck_Id : +this.form.truckSize.value
                })
                .subscribe(
                    (response) => {
                        this.lodder.hideLodder();

                        if (response.succeeded == true) {
                            this.toastr.success("SUCCESS");
                            this.reload();
                            this.hideAddLtlModel();
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
        } else {
            this.toastr.info("Please add atleast one pallet price.");
            this.validateprice = true;
            return;
        }
    }

    updateLtl() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.ltlform.invalid) {
            return;
        }

        if (
            this.form.pprice1.value != null ||
            this.form.pprice2.value != null ||
            this.form.pprice3.value != null ||
            this.form.pprice4.value != null ||
            this.form.pprice5.value != null ||
            this.form.pprice6.value != null ||
            this.form.pprice7.value != null ||
            this.form.pprice8.value != null ||
            this.form.pprice9.value != null ||
            this.form.pprice10.value != null
        ) {
            this.validateprice = false;
            this.lodder.showLodder();
            this.companyClient
                .addEditLTL(<CreateLTLCommand>{
                    id: parseInt(this.editid),
                    company_Id: this.queryPrams,
                    originCity:
                        this.form.origincity.value == ""
                            ? "Default"
                            : this.form.origincity.value == null
                            ? "Default"
                            : this.form.origincity.value,
                    destinationCity:
                        this.form.destinationcity.value == ""
                            ? "Default"
                            : this.form.destinationcity.value == null
                            ? "Default"
                            : this.form.destinationcity.value,
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
                    pPrice1:
                        this.form.pprice1.value == null
                            ? 0
                            : this.form.pprice1.value,
                    pPrice2:
                        this.form.pprice2.value == null
                            ? 0
                            : this.form.pprice2.value,
                    pPrice3:
                        this.form.pprice3.value == null
                            ? 0
                            : this.form.pprice3.value,
                    pPrice4:
                        this.form.pprice4.value == null
                            ? 0
                            : this.form.pprice4.value,
                    pPrice5:
                        this.form.pprice5.value == null
                            ? 0
                            : this.form.pprice5.value,
                    pPrice6:
                        this.form.pprice6.value == null
                            ? 0
                            : this.form.pprice6.value,
                    pPrice7:
                        this.form.pprice7.value == null
                            ? 0
                            : this.form.pprice7.value,
                    pPrice8:
                        this.form.pprice8.value == null
                            ? 0
                            : this.form.pprice8.value,
                    pPrice9:
                        this.form.pprice9.value == null
                            ? 0
                            : this.form.pprice9.value,
                    pPrice10:
                        this.form.pprice10.value == null
                            ? 0
                            : this.form.pprice10.value,
                    truck_Id : +this.form.truckSize.value
                })
                .subscribe(
                    (response) => {
                        this.lodder.hideLodder();

                        if (response.succeeded == true) {
                            this.toastr.success("SUCCESS");
                            this.reload();
                            this.hideAddLtlModel();
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
        } else {
            this.toastr.info("Please add atleast one pallet price.");
            this.validateprice = true;
            return;
        }
    }

    deleteLtl() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();

        this.companyClient
            .activeDeleteLTL(<RemoveAndDeactiveLTLCommand>{
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

    export() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.query.pageSize = this.ltldetails.total;
        this.exportClient.exportLTL(this.query).subscribe((result) => {
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
        this.query.pageSize = this.ltldetails.total;
        this.exportClient.exportLTLLanes(this.query).subscribe((result) => {
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

    showAddLtlModal(template: TemplateRef<any>, id: string, editdata: any) {
        this.submitted = false;
        this.addLtlModel = this.modalService.show(template, this.config);
        this.editid = id;
        if (this.editid != "") {
            const selectedOption = this.truckSizes.find(option => option.text === editdata.truckSize);
            this.ltlform.patchValue({
                origincity: editdata.originCity,
                destinationcity: editdata.destinationCity,
                originState: editdata.originState,
                destinationState: editdata.destinationState,
                pprice1: editdata.pPrice1,
                pprice2: editdata.pPrice2,
                pprice3: editdata.pPrice3,
                pprice4: editdata.pPrice4,
                pprice5: editdata.pPrice5,
                pprice6: editdata.pPrice6,
                pprice7: editdata.pPrice7,
                pprice8: editdata.pPrice8,
                pprice9: editdata.pPrice9,
                pprice10: editdata.pPrice10,
                truckSize : selectedOption ? selectedOption.value : 0
            });
        }
    }

    hideAddLtlModel() {
        this.addLtlModel.hide();
        this.submitted = false;
        this.ltlform.get("originState").enable();
        this.ltlform.get("destinationState").enable();
        this.ltlform.patchValue({
            origincity: "",
            destinationcity: "",
            originState: "",
            destinationState: "",
            pprice1: "",
            pprice2: "",
            pprice3: "",
            pprice4: "",
            pprice5: "",
            pprice6: "",
            pprice7: "",
            pprice8: "",
            pprice9: "",
            pprice10:"",         
            truckSize: 0
        });
        this.validateprice = false;
    }

    showDeleteModel(template: TemplateRef<any>, id: string) {
        this.deleteModel = this.modalService.show(template, this.config);
        this.editid = id;
    }

    hideDeleteModel() {
        this.deleteModel.hide();
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
        if(this.ltldetails.total > 0){
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
            .postFile(this.fileUpload.value, this.queryPrams, "LTL")
            .subscribe(
                (res) => {
                    this.lodder.hideLodder();
                    this.ltldetails.total > 0 ? this.hideConfirmModal() : this.uploadModal.hide();
                    this.reload();
                    if (res.succeeded) {
                        this.toastr.success("SUCCESS");
                    } else {
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
