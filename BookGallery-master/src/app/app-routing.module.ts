import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NewBookComponent } from './Components/Book/new-book/new-book.component';
import { AddExcelBooksComponent } from './Components/Book/add-excel-books/add-excel-books.component';
import { BooksComponent } from './Components/Book/books/books.component';
import { HomeComponent } from './Components/Book/home/home.component';


const routes: Routes = [
  {path:'excel',component:AddExcelBooksComponent},
  {path:'add',component:NewBookComponent},
  {path:'books',component:BooksComponent},
  {path:'Home',component:HomeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
