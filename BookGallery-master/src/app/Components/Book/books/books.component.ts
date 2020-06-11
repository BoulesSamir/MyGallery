import { Component, OnInit } from '@angular/core';
import { BookService } from 'src/app/Services/book.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  constructor(private _service:BookService) { }

  ngOnInit(): void {
    this.getAll();
  }
  getAll(){
    this._service.getBooks(0,3).subscribe(
      res=>console.log(res),
      err=>console.log("aa"+JSON.stringify(err))
      )
  }

}
