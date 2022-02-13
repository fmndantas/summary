import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {IModal} from "../../generic-modal/generic-modal.model";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Observable} from "rxjs";
import {AppSummary, NullAppSummary, ISummary} from "../summary.model";
import {Container, IContainer} from "../../container/container.model";

@Component({
  selector: 'app-summary-form',
  templateUrl: './summary-form.component.html',
  styleUrls: ['./summary-form.component.css']
})
export class SummaryFormComponent implements OnChanges, IModal {
  @Input() summary!: AppSummary;

  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = fb.group({
      title: ["", Validators.required],
      author: ["", Validators.required],
      year: [""]
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (!this.summary.empty) {
      this.form.patchValue(this.summary.serialize);
    }
  }

  getFormControl(controlName: string): FormControl {
    return this.form.get(controlName) as FormControl;
  }

  invalidValues(): Array<[string, string]> {
    return [["", ""]];
  }

  serialize(): ISummary {
    let title: string = this.getFormControl("title").value;
    let year: string = this.getFormControl("year").value;
    let root: IContainer = this.summary
      .root
      .getState();
    root.Title = title;
    debugger
    return {
      id: this.summary.id,
      title: title,
      author: this.getFormControl("author").value,
      root: JSON.stringify(root.serialize()),
      year: year ? Number(year) : null
    };
  }

  valid(): boolean {
    return this.form.valid;
  }

  changed(): Observable<any> {
    return this.form.valueChanges;
  }
}
