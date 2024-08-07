import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import {
    FreightCompanyClient,
    GridQuery,
    GridResultOfFreightCompany,
    CreateCompanyCommand,
    BasicInformationCompanyDto,    
    Payment,
    LU_Country,
    LU_State,
} from "../../Anubis-api";
import { Title } from "@angular/platform-browser";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { LodderService } from "../../lodder.service";
import { ReloadService } from "../../reload.service";

@Component({
    selector: "app-add-company",
    templateUrl: "./add-company.component.html",
    styleUrls: ["./add-company.component.css"],
})
export class AddCompany implements OnInit {
    @Input() id: string;

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
    //shipmentFreightTypes: LU_ShipmentFreightTypes[];
    //shipmentFreightType: LU_ShipmentFreightTypes;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private titleService: Title,
        private companyClient: FreightCompanyClient,
        private toastr: ToastrService,
        private lodder: LodderService,
        private reloadService: ReloadService
    ) {
        titleService.setTitle("Add Company");
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
            isactive: [true],
            ftl: [true],
            ltl: [true],
            wgs: [true],
        });
    }

    ngOnInit(): void {
        this.query = { ...this.query, ...this.init };
        this.basicInfo();
    }

    get form() {
        return this.companyform.controls;
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
                //this.shipmentFreightTypes = this.Binfo.shipmentFreightTypes;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }

    addCompany() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;

        if (this.companyform.invalid) {
            return;
        }
        this.lodder.showLodder();

        this.companyClient
            .addCompany(<CreateCompanyCommand>{
                id: 0,
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
              //  shipmentFreightTypes: this.selectedShipment,
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

    @Output() selectShipmentEvent = new EventEmitter();
    selectedShipment: number[] = [];

    selectShipment(id: number): boolean {
        if (this.selectedShipment.indexOf(id) !== -1)
            this.selectedShipment.splice(this.selectedShipment.indexOf(id), 1);
        else this.selectedShipment.push(id);

        this.selectShipmentEvent.emit(this.selectedShipment);
        return false;
    }
}
