import {
  AfterContentChecked,
  Component,
  ContentChild, EventEmitter,
  Input,
  OnInit, Output,
} from '@angular/core';
import {IModal, Serializable} from "./generic-modal.model";

@Component({
  selector: 'app-generic-modal',
  templateUrl: './generic-modal.component.html',
  styleUrls: ['./generic-modal.component.css']
})
export class GenericModalComponent implements OnInit, AfterContentChecked, Serializable {
  @Input() visible!: boolean;
  @Input() title!: string;
  @Input() message: string = "";
  @Input() showConfirmButton: boolean = true;
  @Input() confirmButtonText: string = "OK";
  @Input() showCancelButton: boolean = true;
  @Input() cancelButtonText: string = "Cancel";
  @Input() width: number = 512;
  @ContentChild("content") innerModal!: IModal;
  @Output() handleConfirm: EventEmitter<void>;
  @Output() handleCancel: EventEmitter<void>;

  constructor() {
    this.handleConfirm = new EventEmitter<void>();
    this.handleCancel = new EventEmitter<void>();
  }

  ngOnInit(): void {
  }

  ngAfterContentChecked(): void {
    this.enableConfirmButton(this.innerModal.valid());
    this.innerModal
      .changed()
      .subscribe(_ => this.enableConfirmButton(this.innerModal.valid()))
  }

  enableConfirmButton(enable: boolean): void {
    let confirmButton: HTMLInputElement | null = this.confirmButton;
    if (confirmButton != null) {
      confirmButton.disabled = !enable;
    }
  }

  get confirmButton(): HTMLInputElement | null {
    return document.querySelector(".swal2-confirm");
  }

  _handleConfirm(): void {
    this.handleConfirm.emit();
  }

  _handleCancel(): void {
    this.handleCancel.emit();
  }

  serialize(): any {
    return this.innerModal.serialize();
  }
}
