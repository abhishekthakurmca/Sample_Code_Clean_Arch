import { Component, OnInit, TemplateRef, Input, Output, EventEmitter } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
//import { ErrorService } from '../../error.service';

@Component({
    selector: 'app-confirm-button',
    templateUrl: './confirm-button.component.html',
    styleUrls: ['./confirm-button.component.css']
})
export class ConfirmButtonComponent implements OnInit {
    modalRef: BsModalRef;

    @Input() title: string = "Title";
    @Input() actionVerb: string = "Action";
    @Input() cancelVerb: string = "Cancel";
    @Input() confirmVerb: string = "Confirm";

    @Output() confirmEvent = new EventEmitter();
    @Output() cancelEvent = new EventEmitter();

    constructor(
        private modalService: BsModalService,
       // private errorService: ErrorService
    ) { }

    ngOnInit(): void {
    }

    show(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template);
    }

    confirm() {
        this.confirmEvent.emit(this);
    }

    cancel() {
        this.cancelEvent.emit();
        this.modalRef.hide();
      //  this.errorService.setError(null);
    }

}
