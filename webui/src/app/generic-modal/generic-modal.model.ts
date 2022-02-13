import {Observable} from "rxjs";

export interface Serializable {
  serialize(): any;
}

export interface IModal extends Serializable {
  invalidValues(): Array<[string, string]>;

  valid(): boolean;

  changed(): Observable<any>;
}
