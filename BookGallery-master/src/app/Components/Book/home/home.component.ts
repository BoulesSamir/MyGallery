import { Component, OnInit, ViewChild } from '@angular/core';
import { BookService } from 'src/app/Services/book.service';
import { BookTable } from 'src/app/ModelView/book-table';
import { MatTableDataSource } from '@angular/material/table';
import {Sort, MatSortModule} from '@angular/material/sort';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
books:BookTable[];
  constructor(private _service:BookService) { }
  displayedColumns: string[] =
  ['Title' , 'Category','Author', 
  'PublisherName','ISBN','Price','Year','ID'];
  dataSource;
  @ViewChild(MatPaginator)paginator:MatPaginator;
  @ViewChild (MatSortModule) sort:MatSortModule;
  ngOnInit(): void {
    this.getBooks(0,10);
  }
  getBooks(start,numberOfbooks){
this._service.getBooks(start,numberOfbooks).subscribe(
  res=>{
    console.log("succ"+res)
    this.books=res;
    this.dataSource=new MatTableDataSource(this.books); 
        this.dataSource.sort = this.sort;
        this.dataSource.paginator=this.paginator;
},
  err=>console.log("failed"+err)
)
  }
changePage(event){
//console.log(event);
this.getBooks((event.pageIndex*event.pageSize),10)
}
details(id){
  console.log(id);
  this._service.getOne(id).subscribe(res=>console.log(res),err=>console.log(JSON.stringify(err)+"faled"));
}
}
