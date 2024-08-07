import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import {
    FreightCompanyClient,
    GridQuery,
    GridResultOfFreightCompany,
    CreateCompanyCommand,
    UpdateCompanyCommand,
    BasicInformationCompanyDto,
    LU_Country,
    LU_State,
    Payment,
} from "../../Anubis-api";
import { Title } from "@angular/platform-browser";
import { ActivatedRoute } from "@angular/router";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import {
    FormBuilder,
    FormGroup,
    Validators,
    FormArray,
    FormControl,
} from "@angular/forms";
import { LodderService } from "../../lodder.service";
import { ReloadService } from "../../reload.service";

@Component({
    selector: "app-edit-company",
    templateUrl: "./edit-company.component.html",
    styleUrls: ["./edit-company.component.css"],
})
export class EditCompany implements OnInit {
    @Input() id: string;
    queryParams: number;

    companyform: FormGroup;
    submitted: boolean = false;

    Binfo: BasicInformationCompanyDto;
    countrys: LU_Country[];
    country: LU_Country;
    states: LU_State[];
    state: LU_State;
    paymentMethods: Payment[];
    paymentMethod: Payment;
    paymentTerms: Payment[];
    paymentTerm: Payment;
   // shipmentFreightTypes: MultiCheckboxShipment[];


   // multiCheckboxShipments: MultiCheckboxShipment[];
    //multiCheckboxShipments: LU_ShipmentFreightTypes;

    //shipmentFreightTypes1: any;
    //shipmentFreightTypes2: any[];

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private titleService: Title,
        private companyClient: FreightCompanyClient,
        private route: ActivatedRoute,
        private toastr: ToastrService,
        private lodder: LodderService,
        private reloadService: ReloadService
    ) {
        titleService.setTitle("Edit Company");
        this.companyform = this.formBuilder.group({
            name: ["", [Validators.required, Validators.maxLength(100)]],
            shortname: ["", [Validators.required, Validators.maxLength(50)]],
            phone: [],
            tenderemail: [
                "",
                [
                    Validators.required,
                    Validators.email,
                    Validators.pattern("[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$"),
                ],
            ],
            fax: [""],
            address: ["", Validators.required],
            address2: [],
            city: ["", Validators.required],
            state: ["0", [Validators.required, Validators.min(1)]],
            zip: ["", Validators.required],
            region: ["", Validators.required],
            country: ["0", [Validators.required, Validators.min(1)]],
            payment: ["-1", [Validators.required, Validators.min(0)]],
            paymentterm: ["-1", [Validators.required, Validators.min(0)]],
            isactive: [],
            ftl: [],
            ltl: [],
            wgs: [],
            // shipmentFreightTypes:[]
            //orders: new FormArray([]),
            //ordersData:[]
        });

        // this.addCheckboxes();
    }

    //private addCheckboxes() {
    //  this.ordersData.forEach(() => this.ordersFormArray.push(new FormControl(false)));
    //}

    //get ordersFormArray() {
    //  return this.form.controls.orders as FormArray;
    //}

    ngOnInit(): void {
        this.queryParams = +this.route.snapshot.queryParamMap.get("id");
        // this.title.setTitle("Lot #" + this.id);
        this.query = { ...this.query, ...this.init };
        this.basicInfo();
        this.reload();
        //    this.basicInfo();
    }

    init: any = {
        sort: "created",
    };

    query: GridQuery = <GridQuery>{
        page: 1,
        pageSize: 10,
        filter: {},
        ascending: false,
        sort: "",
    };

    gridUpdate(e: any): void {
        this.query = { ...this.query, ...e };
    }

    get form() {
        return this.companyform.controls;
    }

    reload(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getCompanyDetails(this.queryParams).subscribe(
            (response) => {
               // console.log("Edit", response);

                //this.shipmentFreightTypes = response.multiCheckboxShipments;
                //if (this.selectedShipment.length == 0) {
                //    for (
                //        let i = 0;
                //        i < this.shipmentFreightTypes.length - 1;
                //        i++
                //    ) {
                //        if (this.shipmentFreightTypes[i].isSelected === true) {
                //            this.selectedShipment.push(
                //                this.shipmentFreightTypes[i].id
                //            );
                //        }
                //    }
                //}
                this.companyform.patchValue({
                    id: response.id,
                    name: response.name,
                    shortname: response.shortName,
                    phone: response.phone,
                    tenderemail: response.tenderEmail,
                    fax: response.fax,
                    address: response.address,
                    address2: response.address1,
                    city: response.city,
                    state: response.state,
                    zip: response.zipCode,
                    region: response.region,
                    country: response.country,
                    payment: response.paymentMethod,
                    paymentterm: response.paymentTerm,
                    isactive: response.isActive,
                    ftl: response.ftl,
                    ltl: response.ltl,
                    wgs: response.wgs,
                    //    shipmentFreightTypes: response.shipmentFreightTypes,
                  //  multiCheckboxShipments: response.multiCheckboxShipments,
                    //     shipmentFreightTypes2:response.multiCheckboxShipments
                });

                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }
    updateCompany() {
        //this.shipmentFreightTypes;
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.companyform.invalid) {
            return;
        }

        this.lodder.showLodder();
        // this.shipmentFreightTypes = this.selectedShipment;
        this.companyClient
            .editCompany(<UpdateCompanyCommand>{
                id: this.queryParams,
                name: this.form.name.value,
                shortName: this.form.shortname.value,
                phone: this.form.phone.value,
                tenderEmail: this.form.tenderemail.value,
                fax: this.form.fax.value,
                address: this.form.address.value,
                address1: this.form.address2.value,
                city: this.form.city.value,
                state: this.form.state.value,
                zipCode: this.form.zip.value,
                region: this.form.region.value,
                country: this.form.country.value,
                paymentMethod: this.form.payment.value,
                paymentTerm: this.form.paymentterm.value,
                isActive: this.form.isactive.value,
                ftl: this.form.ftl.value,
                ltl: this.form.ltl.value,
                wgs: this.form.wgs.value,
            //    shipmentFreightTypes: this.selectedShipment,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    this.toastr.success("SUCCESS");
                    this.router.navigate(["/", "freight-company"]);
                },
                (error) => {
                    this.lodder.hideLodder();
                    this.toastr.error("ERROR");
                }
            );
    }

    basicInfo(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.companyClient.getCompanyBasicInformation("basic").subscribe(
            (response) => {
                this.Binfo = response;
                this.countrys = this.Binfo.countries;
                this.states = this.Binfo.states;
                this.paymentMethods = this.Binfo.paymentMethods;
                this.paymentTerms = this.Binfo.paymentTerms;
                //     this.shipmentFreightTypes = this.Binfo.shipmentFreightTypes;
                //    this.shipmentFreightTypes1 = this.Binfo.shipmentFreightTypes;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }

    @Output() selectShipmentEvent = new EventEmitter();
    selectedShipment: number[] = [];

    selectShipment(id: number): boolean {
        //  alert(1);
        //if (this.shipmentFreightTypes[id].isSelected === true) {
        //  this.shipmentFreightTypes[id].isSelected = false;
        //} else {
        //  this.shipmentFreightTypes[id].isSelected = true;
        //}
        let check = 0;
        for (let i = 0; i < this.selectedShipment.length - 1; i++) {
            if (this.selectedShipment[i] === id) {
                check = 1;
                this.selectedShipment.splice(i, 1);
            }
        }
        if (check === 0) {
            this.selectedShipment.push(id);
        }

        //if (this.selectedShipment.indexOf(id) !== -1)
        //  this.selectedShipment.splice(this.selectedShipment.indexOf(id), 1);
        //else this.selectedShipment.push(id);

        this.selectShipmentEvent.emit(this.selectedShipment);
        return false;
    }
}
