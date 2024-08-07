import { Component, OnInit, TemplateRef } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Title } from "@angular/platform-browser";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";
import {
    AddUpdateTeamMemberCommand,
    ExportClient,
    GridQuery,
    RemoveTeamMemberCommand,
    TeamMemberClient,
} from "../Anubis-api";
import { GlobalConstants } from "../Common/GlobalConstants";
import { LodderService } from "../lodder.service";
import { ReloadService } from "../reload.service";

@Component({
    selector: "team-members",
    templateUrl: "./team-members.component.html",
    styleUrls: ["./team-members.component.css"],
})
export class TeamMembersComponent implements OnInit {
    PageRecord: number = GlobalConstants.PerPageRecords;
    teamMembers: any;
    editData: any;
    teamForm: FormGroup;
    submitted: boolean = false;
    addUpdateModal: BsModalRef;
    deleteModal: BsModalRef;
    config = {
        backdrop: false,
        ignoreBackdropClick: true,
    };
    constructor(
        private team: TeamMemberClient,
        private formBuilder: FormBuilder,
        private exportClient: ExportClient,
        private modalService: BsModalService,
        private toastr: ToastrService,
        private titleService: Title,
        private lodder: LodderService,
        private reloadService: ReloadService
    ) {
        titleService.setTitle("Team Members");
        this.teamForm = this.formBuilder.group({
            name: ["", [Validators.required, Validators.maxLength(100)]],
        });
    }

    get form() {
        return this.teamForm.controls;
    }

    ngOnInit(): void {
        this.query = { ...this.query, ...this.init };
        this.getAllTeamMembers();
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
        this.getAllTeamMembers();
    }

    showModal(template: TemplateRef<any>, editData: any) {
        this.teamForm.reset();
        this.addUpdateModal = this.modalService.show(template, this.config);
        this.editData = editData;
        if (editData) {
            this.teamForm.patchValue({
                name: editData.name,
            });
        }
    }
    hideModal() {
        this.addUpdateModal.hide();
        this.teamForm.reset();
        this.submitted = false;
    }

    getAllTeamMembers() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.team.getAllTeamMembers(this.query).subscribe(
            (response) => {
                this.teamMembers = response;
                this.lodder.hideLodder();
            },
            (error) => {
                this.lodder.hideLodder();
                this.toastr.error("ERROR");
            }
        );
    }

    addNewMember() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.teamForm.invalid) return;
        this.lodder.showLodder();
        this.team
            .addUpdateTeamMember(<AddUpdateTeamMemberCommand>{
                id: 0,
                name: this.form.name.value,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    if (response.succeeded) {
                        this.hideModal();
                        this.toastr.success("SUCCESS");
                        this.getAllTeamMembers();
                    } else this.toastr.info(response.errors.toString());
                },
                (error) => {
                    this.lodder.hideLodder();
                    this.toastr.error("ERROR");
                }
            );
    }

    updateMember() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.submitted = true;
        if (this.teamForm.invalid) return;
        this.lodder.showLodder();
        this.team
            .addUpdateTeamMember(<AddUpdateTeamMemberCommand>{
                id: +this.editData.id,
                name: this.form.name.value,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    if (response.succeeded) {
                        this.hideModal();
                        this.toastr.success("SUCCESS");
                        this.getAllTeamMembers();
                    } else this.toastr.info(response.errors.toString());
                },
                (error) => {
                    this.lodder.hideLodder();
                    this.toastr.error("ERROR");
                }
            );
    }

    showDeleteModal(template: TemplateRef<any>, id: string) {
        this.deleteModal = this.modalService.show(template, this.config);
        this.editData = id;
    }
    hideDeleteModel() {
        this.deleteModal.hide();
    }

    deleteTeamMember() {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.lodder.showLodder();
        this.team
            .removeTeamMember(<RemoveTeamMemberCommand>{
                id: this.editData,
            })
            .subscribe(
                (response) => {
                    this.lodder.hideLodder();
                    if (response.succeeded) {
                        this.hideDeleteModel();
                        this.toastr.success("SUCCESS");
                        this.getAllTeamMembers();
                    }
                },
                (error) => {
                    this.lodder.hideLodder();
                    this.toastr.error("ERROR");
                }
            );
    }

    exportTeamMember(): void {
        var isauthenticated = this.reloadService.checkAuthenticated();
        if (!isauthenticated) {
            return;
        }
        this.query.pageSize = this.teamMembers.total;
        this.exportClient.exportTeamMembers(this.query).subscribe((result) => {
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
