import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookService } from 'src/app/Services/book.service';
@Component({
  selector: 'app-new-book',
  templateUrl: './new-book.component.html',
  styleUrls: ['./new-book.component.css']
})
export class NewBookComponent implements OnInit {
  InsertBook: FormGroup;
  @ViewChild('img') Img;
  @ViewChild('nameFile') fileName;  
  selectFile: File;
 
  constructor(private fb:FormBuilder,private _service:BookService) { }

  ngOnInit(): void {
    this.InsertBook = this.fb.group(
      {
        Author: ['', [Validators.required]],
        Title: ['', [Validators.required]],
        PublicationYear: ['', [Validators.required]],
        Category: ['', [Validators.required]],
        PublicationCountry: ['', [Validators.required]],
        Price: ['', [Validators.required]],
        Currency: ['', [Validators.required]],
        ISBN: ['', [Validators.required]],
        Summary: [''],
        Notes: ['']     
      }
    );

  }
  Add(){
  
    const book=new FormData();
    book.append('Author',this.InsertBook.value.Author);
    book.append('Title',this.InsertBook.value.Title);
    book.append('PublicationYear',this.InsertBook.value.PublicationYear);
    book.append('Category',this.InsertBook.value.Category);
    book.append('PublicationCountry',this.InsertBook.value.PublicationCountry);
    book.append('Price',this.InsertBook.value.Price);
    book.append('Currency',this.InsertBook.value.Currency);
    book.append('ISBN',this.InsertBook.value.ISBN);
    book.append('Summary',this.InsertBook.value.Summary);
    book.append('Notes',this.InsertBook.value.Notes);
    if(this.Img.nativeElement.files[0]!=null){
    book.append("Img",this.Img.nativeElement.files[0]);
    }
    this._service.insertBook(book).subscribe(
      res=>console.log(res),
      err=>console.log("failed",err)
    )
    }
    uploadImage(event){
      this.selectFile = <File>event.target.files[0]; 
      this.fileName.nativeElement.value=this.selectFile.name;
    }

}
