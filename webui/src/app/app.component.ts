import {Component} from '@angular/core';
import {GenericModalService} from "./generic-modal/generic-modal.service";
// import {IModalRegistry} from "./generic-modal/generic-modal.model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private modalService: GenericModalService) {
  }

  // get Modals(): IModalRegistry {
  //   return this.modalService.registry;
  // }
}
