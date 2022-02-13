import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {ContainerComponent} from './container/container.component';
import {SummaryComponent} from './summary/summary.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from "@angular/common/http";
import {ListComponent} from './list/list.component';
import {CommonModule} from "@angular/common";
import { HighlightDirective } from './highlight.directive';
import {SweetAlert2Module} from "@sweetalert2/ngx-sweetalert2";
import { GenericModalComponent } from './generic-modal/generic-modal.component';
import { SummaryFormComponent } from './summary/summary-form/summary-form.component';

@NgModule({
  declarations: [
    AppComponent,
    ContainerComponent,
    SummaryComponent,
    ListComponent,
    HighlightDirective,
    GenericModalComponent,
    SummaryFormComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    SweetAlert2Module.forRoot(),
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
