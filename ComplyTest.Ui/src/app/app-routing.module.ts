import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ItemListComponent } from './components/item-list/item-list.component';
import { ItemFormComponent } from './components/item-form/item-form.component';

const routes: Routes = [
  {
    path: '',
    component: ItemListComponent,
  },
  {
    path: 'create',
    component: ItemFormComponent,
  },
  {
    path: ':id/edit',
    component: ItemFormComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
