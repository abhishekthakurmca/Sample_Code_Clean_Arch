import { Component, OnInit, TemplateRef, Input } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { ActivatedRoute } from "@angular/router";
import {
    FreightCompanyClient,
    CreateWGSWeightCommand,
    GridWGSWeightQuery,
    GridResultOfWGSCompanyWeights,
    GridQuery,
    GridWGSMilesQuery,
    CreateWGSMilesCommand,
    GridWGSResultOfWGSCompanyMilesAndWGSCompanyWeightsAndWGSCompanyPrice,
    GridWGSQuery,
    UpdateWGSWeightPricesCommand,
    GridWGSAccesrailsQuery,
    CreateWGSAccesrailsCommand,
    RemoveWGSAccesrailsCommand,
    GridResultOfFreightCompanyCodes,
    GridResultOfWGSAccesrailsDto,
    FreightCompanyCodes,
    BasicInformationCompanyDto,
    InsertLabourQuoteCommand,
    ExportClient,
    OptionVm,
    GridResultOfWGSMilesDto
} from "../../Anubis-api";

import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import {
    FormBuilder,
    FormGroup,
    Validators,
    FormControl,
} from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { GlobalConstants } from "src/anubis-api/Common/GlobalConstants";
import { ReloadService } from "../../reload.service";
import { LodderService } from "../../lodder.service";

@Component({
    selector: "app-white-glove",
    templateUrl: "./white-glove.component.html",
    styleUrls: ["./white-glove.component.css"],
})
export class WhiteGloveComponent implements OnInit {
    @Input() PageRecord: number = GlobalConstants.PerPageRecords;
    wg: GridResultOfWGSCompanyWeights;
    mg: GridResultOfWGSMilesDto;
    wgcGrid: GridWGSResultOfWGSCompanyMilesAndWGSCompanyWeightsAndWGSCompanyPrice;
    wgFCode: GridResultOfFreightCompanyCodes;
    wgAcces: GridResultOfWGSAccesrailsDto;

    Binfo: BasicInformationCompanyDto;
    @Input() queryPrams: number;
    weightdetails: GridWGSWeightQuery;
    weight: any = {};
    weightModel: BsModalRef;
    clientweighttypes: any;
    editid: string;
    editid1: string;

    miledetails: GridWGSMilesQuery;
    mile: any = {};
    mileModel: BsModalRef;
    price: any = {};
    priceModel: BsModalRef;
    accesrailsdetails: GridWGSMilesQuery;
    accesrail: any = {};
    accesrailModel: BsModalRef;
    deleteModel: BsModalRef;

    labourCost = new FormControl("");

    freightCodes: FreightCompanyCodes[];
    freightCode: FreightCompanyCodes;
    submitted: boolean = false;
    LC: number;

    ID: number;
    //wgGrid: boolean;
    //mgGrid: boolean;
    whiteGloveform: FormGroup;
    wWGform: FormGroup;
    mWGform: FormGroup;
    pWGform: FormGroup;
    aWGform: FormGroup;

    config = {
        backdrop: false,
        ignoreBackdropClick: true,
    };
    truckSizes: OptionVm[];

    constructor(
        private formBuilder: FormBuilder,
        private titleService: Title,
        private route: ActivatedRoute,
        private companyClient: FreightCompanyClient,
        private modalService: BsModalService,
        private modalService1: BsModalService,
        private toastr: ToastrService,
        private exportClient: ExportClient,
        private reloadService: ReloadService,
        private lodder: LodderService
    ) {
        titleService.setTitle("Edit Weight");

        this.wWGform = this.formBuilder.group({
            wfrom: ["", Validators.required],
            wto: ["", Validators.required],
        });

        this.mWGform = this.formBuilder.group({
            mfrom: ["", Validators.required],
            mto: ["", Validators.required],
            mprice: [null,Validators.pattern(/^\d+(\.\d{1,2})?$/)],
            truckSize: ["0"]
        });

        this.pWGform = this.formBuilder.group({
            pprice: ["", Validators.required],
            pId: [],
            WlabelValue: [],
            MlabelValue: [],
        });

        this.aWGform = this.formBuilder.group({
            accsdefaultPrice: [""],
            freightCodeid: ["0", [Validators.required, Validators.min(1)]],
        });
    }

    //get form() {
    //  return this.whiteGloveform.controls;
    //}

    get formwWG() {
        return this.wWGform.controls;
    }

    get formmWG() {
        return this.mWGform.controls;
    }
    get formpWG() {
        return this.pWGform.controls;
    }
    get formaWG() {
        return this.aWGform.controls;
    }

    ngOnInit(): void {
        this.queryPrams = +this.route.snapshot.queryParamMap.get("id");
        this.init = {
            id: this.queryPrams,
            sort: "Id",
            pageSize: this.PageRecord,
        };
        this.query = { ...this.query, ...this.init };
        this.queryMiles = { ...this.queryMiles, ...this.init };
        this.queryAcces = { ...this.queryMiles, ...this.init };
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.reload();
        this.showEditMile();
        this.showEditWeight();
        this.getLabourCost();
        this.getTruckList();
        // this.init = { id: this.queryPrams, sort: "id" };
        //this.showAccessorialModal();
    }
    init: any = {};
    query: GridWGSWeightQuery = <GridWGSWeightQuery>{
        page: 1,
        pageSize: null,
        filter: {},
        ascending: false,
        sort: "",
    };
    queryMiles: GridWGSMilesQuery = <GridWGSMilesQuery>{
        page: 1,
        pageSize: null,
        filter: {},
        ascending: false,
        sort: "",
    };
    queryAcces: GridWGSMilesQuery = <GridWGSMilesQuery>{
        page: 1,
        pageSize: null,
        filter: {},
        ascending: false,
        sort: "",
    };
    // loading: boolean = false;
    gridUpdate(e: any): void {
        this.query = { ...this.query, ...e };
        this.query.pageSize = this.PageRecord;
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.reload();
        this.lodder.showLodder();
        this.companyClient.getWeights(this.query).subscribe(
            (response) => {
                this.wg = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );
    }
    gridUpdateMiles(e: any): void {
        this.queryMiles = { ...this.queryMiles, ...e };
        this.queryMiles.pageSize = this.PageRecord;
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getMiles(this.queryMiles).subscribe(
            (response) => {
                this.mg = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );
    }
    gridUpdateAcces(e: any): void {
        this.queryAcces = { ...this.queryAcces, ...e };
        this.queryAcces.pageSize = this.PageRecord;
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getWGSAccesrails(this.queryAcces).subscribe(
            (response) => {
                this.wgAcces = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );
    }
    showModal(template: TemplateRef<any>, id: string, editdata: any) {
        this.weightModel = this.modalService.show(template, this.config);
        this.editid = id;
        if (this.editid != "") {
            this.weight = editdata;
        }
        if (this.editid != "") {
            this.weight = editdata;
            this.wWGform.patchValue({
                wfrom: editdata.from,
                wto: editdata.to,
            });
        }
    }
    showMileModal(template: TemplateRef<any>, id: string, editdata: any) {
        this.mileModel = this.modalService1.show(template, this.config);
        this.editid = id;
        if (this.editid != "") {
            this.mile = editdata;
            const selectedOption = this.truckSizes.find(option => option.text === editdata.truckSize);
            this.mWGform.patchValue({
                mfrom: editdata.from,
                mto: editdata.to,
                mprice: editdata.price,
                truckSize : selectedOption ? selectedOption.value : 0
            });
        }
    }
    showAccessorialModal(
        template: TemplateRef<any>,
        id: string,
        editdata: any
    ) {
        this.accesrailModel = this.modalService1.show(template, this.config);
        this.editid = id;
        if (this.editid != "") {
            this.accesrail = editdata;
            this.aWGform.patchValue({
                //accsdefaultPrice: editdata.defaultPrice,
                freightCodeid: editdata.freightCode_Id,
            });
        }
    }
    showPriceModal(
        template: TemplateRef<any>,
        id: string,
        editdata: any,
        Mlabel: string,
        Wlabel: string
    ) {
        this.priceModel = this.modalService1.show(template, this.config);
        this.editid = id;
        if (this.editid != "") {
            this.price = editdata;
            this.pWGform.patchValue({
                pprice: editdata.price,
                pId: editdata.id,
                WlabelValue: Wlabel,
                MlabelValue: Mlabel,
            });
        }
    }
    showDeleteModel(template: TemplateRef<any>, id: string) {
        this.deleteModel = this.modalService.show(template, this.config);
        this.editid = id;
    }

    closeModel() {
        this.mg = null;
        this.wg = null;
    }
    hideModel(MType: string) {
        this.submitted = false;
        if (MType === "W") {
            this.weightModel.hide();
            //this.weight = {};
            this.wWGform.reset();
        } else if (MType === "M") {
            this.mileModel.hide();
            //this.mile = {};
            this.mWGform.reset();
            this.mWGform.patchValue({
                truckSize: 0
            });
        } else if (MType === "P") {
            this.priceModel.hide();
            //this.price = {};
            this.pWGform.reset();
        } else if (MType === "A") {
            this.accesrailModel.hide();
            //this.accesrail = {};
            this.aWGform.reset();
        } else if (MType === "D") {
            this.deleteModel.hide();
        }
        //this.whiteGloveform.reset();
    }
    getLabourCost() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getLabourCost(this.queryPrams).subscribe(
            (response) => {
                this.LC = response;
                this.labourCost.patchValue(this.LC);
                //this.labourCost = this.LC;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );
    }

    showEditMile() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getMiles(this.queryMiles).subscribe(
            (response) => {
                this.mg = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );
    }
    showEditWeight() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getWeights(this.query).subscribe(
            (response) => {
                this.wg = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );
    }
    reload(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getWGCGrid(this.query).subscribe(
            (response) => {
                this.wgcGrid = response;             
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );

        this.lodder.showLodder();
        this.companyClient.getFreightCompanyCodes(this.query).subscribe(
            (response) => {
                this.wgFCode = response;
                this.freightCodes = this.wgFCode.data;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );
        this.lodder.showLodder();
        this.companyClient.getWGSAccesrails(this.queryAcces).subscribe(
            (response) => {
                this.wgAcces = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );
        this.basicInfo();
    }
    basicInfo(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getFreightCodeList(this.queryPrams).subscribe(
            (response) => {
                this.Binfo = response;
                this.freightCodes = this.Binfo.freightCompanyCodes;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
            }
        );
    }

    insertLabourCost() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient
            .insertLabourCost(<InsertLabourQuoteCommand>{
                companyId: +this.queryPrams,
                labourCost:
                    +this.labourCost.value == null ? 0 : +this.labourCost.value,
            })
            .subscribe((response) => {
                this.submitted = false;
                this.lodder.hideLodder();
                if (response.succeeded == true) {
                    this.toastr.success("SUCCESS");
                    this.reload();
                    // this.showEditWeight();
                    //this.hideModel('W');
                } else if (
                    response.succeeded == false &&
                    response.errors.length > 0
                ) {
                    this.toastr.error(response.errors.toString(), "ERROR");
                }
            },
                (error) => {
                    this.lodder.hideLodder();
                });
    }

    addWeight() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.wWGform.invalid) {
            return;
        }

        this.ID = 0;
        if (this.weight.id === undefined) {
        } else {
            this.ID = this.weight.id;
        }
        this.lodder.showLodder();
        this.companyClient
            .addEditWGSWeight(<CreateWGSWeightCommand>{
                id: this.ID,
                company_Id: this.queryPrams,
                from:
                    this.formwWG.wfrom.value == null
                        ? 0
                        : this.formwWG.wfrom.value,
                to: this.formwWG.wto.value == null ? 0 : this.formwWG.wto.value,
                //    labelValue: this.weight.labelValue
            })
            .subscribe((response) => {
                this.submitted = false;
                this.lodder.hideLodder();
                if (response.succeeded == true) {
                    this.toastr.success("SUCCESS");
                    this.reload();
                    this.showEditWeight();
                    this.hideModel("W");
                } else if (
                    response.succeeded == false &&
                    response.errors.length > 0
                ) {
                    this.toastr.error(response.errors.toString(), "ERROR");
                }
            },
                (error) => {
                    this.lodder.hideLodder();
                });
    }
    addMile() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.mWGform.invalid) {
            return;
        }

        this.ID = 0;
        if (this.mile.id === undefined) {
        } else {
            this.ID = this.mile.id;
        }
        this.lodder.showLodder();
        this.companyClient
            .addEditWGSMiles(<CreateWGSMilesCommand>{
                id: this.ID,
                company_Id: this.queryPrams,
                from:
                    this.formmWG.mfrom.value == null
                        ? 0
                        : this.formmWG.mfrom.value,
                to: this.formmWG.mto.value == null ? 0 : this.formmWG.mto.value,
                price:
                    this.formmWG.mprice.value == null
                        ? 0
                        : this.formmWG.mprice.value,
                truck_Id : +this.formmWG.truckSize.value
            })
            .subscribe((response) => {
                this.lodder.hideLodder();
                if (response.succeeded == true) {
                    this.toastr.success("SUCCESS");
                    this.submitted = false;
                    this.reload();
                    this.showEditMile();
                    this.hideModel("M");
                } else if (
                    response.succeeded == false &&
                    response.errors.length > 0
                ) {
                    this.toastr.error(response.errors.toString(), "ERROR");
                }
            },
                (error) => {
                    this.lodder.hideLodder();
                });
    }
    editPrice() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.pWGform.invalid) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient
            .updateWGCPrice(<UpdateWGSWeightPricesCommand>{
                id: this.formpWG.pId.value == null ? 0 : this.formpWG.pId.value,
                price:
                    this.formpWG.pprice.value == null
                        ? 0
                        : this.formpWG.pprice.value,
            })
            .subscribe((response) => {
                this.submitted = false;
                this.lodder.hideLodder();
                if (response.succeeded == true) {
                    this.toastr.success("SUCCESS");
                    this.reload();
                    this.hideModel("P");
                } else if (
                    response.succeeded == false &&
                    response.errors.length > 0
                ) {
                    this.toastr.error(response.errors.toString(), "ERROR");
                }
            },
                (error) => {
                    this.lodder.hideLodder();
                });
    }
    addEditAccesrail() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.aWGform.invalid) {
            return;
        }
        this.ID = 0;
        if (this.accesrail.id === undefined) {
        } else {
            this.ID = this.accesrail.id;
        }
        this.lodder.showLodder();
        this.companyClient
            .addEditWGSAccesrail(<CreateWGSAccesrailsCommand>{
                id: this.ID,
                company_Id: this.queryPrams,
                isFixedPrice: true,
                freightCode_Id: parseInt(this.formaWG.freightCodeid.value),
            })
            .subscribe((response) => {
                this.submitted = false;
                this.lodder.hideLodder();
                if (response.succeeded == true) {
                    this.toastr.success("SUCCESS");
                    this.reload();
                    this.hideModel("A");
                } else if (
                    response.succeeded == false &&
                    response.errors.length > 0
                ) {
                    this.toastr.error(response.errors.toString(), "ERROR");
                }
            },
                (error) => {
                    this.lodder.hideLodder();
                });
    }
    deleteAccessorial() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();

        this.companyClient
            .removeWGSAccesrail(<RemoveWGSAccesrailsCommand>{
                id: +this.editid,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    if (response.succeeded == true) {
                        this.toastr.success("SUCCESS");
                        this.reload();
                        //         this.hideDeleteModel();
                        this.hideModel("D");
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

    exportWG(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.query.pageSize = this.wg.total;
        this.exportClient.exportWeight(this.query).subscribe((result) => {
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

    exportMiles(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.query.pageSize = this.mg.total;
        this.exportClient.exportMiles(this.query).subscribe((result) => {
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


    exportwgAccess(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.query.pageSize = this.wgAcces.total;
        this.exportClient.exportAccessorial(this.query).subscribe((result) => {
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

    getTruckList() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
       
    }
}
