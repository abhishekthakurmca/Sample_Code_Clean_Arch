import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FreightCompanyClient,AddUpdatePermissionsCommand } from 'src/anubis-api/Anubis-api';
import { LodderService } from '../../lodder.service';
import { ReloadService } from '../../reload.service';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.css']
})
export class PermissionsComponent implements OnInit {

  @Input() id: string;
  queryParams: number;
  permissionform: FormGroup;
  submitted: boolean = false;
  form: FormGroup;  
  totalPermissions:any[];
  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private titleService: Title,
    private companyClient: FreightCompanyClient,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private lodder: LodderService,
    private reloadService: ReloadService) { 
      titleService.setTitle("Permissions");
    //   this.permissionform = this.formBuilder.group({        
    //     isactive: [],
                
    // });

    // const controls = this.totalPermissions.map(c => new FormControl(false));
    // controls[0].setValue(true);

    // this.permissionform = this.formBuilder.group({
    //   permissionArray: new FormArray(controls, this.minSelectedCheckboxes(1))
    // });

    // this.permissionform = this.formBuilder.group({
    //   permissions: this.formBuilder.array([this.addPermissionsGroup()]),
    // });

    }

  ngOnInit(): void {
    this.queryParams = +this.route.snapshot.queryParamMap.get("id");
    this.getPermissions();
  }

get formArray() {
  return this.permissionform.get("permissionArray") as FormArray;
}

 minSelectedCheckboxes(min = 1) {
  const validator: ValidatorFn = (formArray: FormArray) => {
    const totalSelected = formArray.controls
      .map(control => control.value)
      .reduce((prev, next) => next ? prev + next : prev, 0);

    return totalSelected >= min ? null : { required: true };
  };

  return validator;
}

getPermissions(): void {
  var isauthenticated = this.reloadService.checkAuthenticated();
  if (!isauthenticated) {
      return;
  }
  this.lodder.showLodder();
  this.companyClient.getPermissions(this.queryParams).subscribe(
      (response) => {
           this.totalPermissions = response;
           const controls = this.totalPermissions.map(c => new FormControl(false));
             for (let i = 0; i < this.totalPermissions.length; i++) {
              for (let j = 0; j < this.totalPermissions[i].freightPermissions.length; j++) {
                      if(this.totalPermissions[i].id == this.totalPermissions[i].freightPermissions[j].permission_Id)
                      {
                       controls[i].setValue(this.totalPermissions[i].freightPermissions[j].isActive)
                      }
              }
          }
           this.permissionform = this.formBuilder.group({
             permissionArray: new FormArray(controls, this.minSelectedCheckboxes(1))
           });
          this.lodder.hideLodder();
      },
      (error) => {
          this.lodder.hideLodder();
          this.toastr.error("ERROR");
      }
  );
}


updatePermissions() {
  var isauthenticated = this.reloadService.checkAuthenticated();
  if (!isauthenticated) {
      return;
  }

  this.lodder.showLodder();
  let ids = [];
  this.totalPermissions.forEach(element => {
    ids.push(element.id);
  });
 
  const selectedOrderIds = this.permissionform.value.permissionArray
      .map((v, i) => v ? this.totalPermissions[i].id : null)
      .filter(v => v !== null);

  this.companyClient
      .addUpdatePermissions(<AddUpdatePermissionsCommand>{ 
        freightPermissionIds: ids,
        permissionIds:selectedOrderIds,
        company_Id:this.queryParams})
      .subscribe(
          (response) => {
              this.lodder.hideLodder();
              this.toastr.success("SUCCESS");
          },
          (error) => {
              this.lodder.hideLodder();
              this.toastr.error("ERROR");
          }
      );
}



}
