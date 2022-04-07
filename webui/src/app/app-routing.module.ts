import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {PageListComponent} from "./page-list/page-list.component";
import {PageSearchComponent} from "./page-search/page-search.component";

const routes: Routes = [
  {path: 'list', component: PageListComponent},
  {path: 'search', component: PageSearchComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
