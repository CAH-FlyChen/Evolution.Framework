/// <reference path="../../../typings/globals/jquery/index.d.ts" />

import { Component, AfterContentInit, Input } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'organize-add',
    template: `
    <div class="modal-header">
      <button type="button" class="close" aria-label="Close" (click)="activeModal.dismiss('Cross click')">
        <span aria-hidden="true">&times;</span>
      </button>
      <h4 class="modal-title">Hi there!</h4>
    </div>
    <div class="modal-body">
      <p>Hello, {{name}}!</p>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-secondary" (click)="activeModal.close('Close click')">Close</button>
    </div>
  `
})
export class OrganizeAddComponent implements AfterContentInit{

    @Input() name;

    constructor(public activeModal: NgbActiveModal) { }


    ngAfterContentInit() {
    }

}
