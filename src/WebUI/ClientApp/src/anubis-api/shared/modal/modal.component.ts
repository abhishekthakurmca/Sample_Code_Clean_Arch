import { Component, OnInit, Input, ViewChild, TemplateRef, EventEmitter, Output } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-modal',
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
    @ViewChild('modalTemplate') modalTemplate: TemplateRef<any>;
    modalRef: BsModalRef;

    _show: boolean = false;
    @Input()
    set show(value: boolean) {
        this._show = value;
        if (value) {
            this.modalRef = this.modalService.show(this.modalTemplate);
        } else if (this.modalRef) {
            this.modalRef.hide();
        }
        this.showChange.emit(value);
    }

    @Output() showChange = new EventEmitter();


    constructor(
        private modalService: BsModalService
    ) { }

    ngOnInit(): void {
    }

    cancel() {
        this.modalRef.hide();
        this.show = false;
    }

}
