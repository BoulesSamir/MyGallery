import { Component, OnInit, ViewChild } from '@angular/core';
import { BookService } from 'src/app/Services/book.service';

@Component({
  selector: 'app-add-excel-books',
  templateUrl: './add-excel-books.component.html',
  styleUrls: ['./add-excel-books.component.css']
})
export class AddExcelBooksComponent implements OnInit {
  selectFile:File;
  @ViewChild('fileInput') fileInput;
  @ViewChild('nameFile') fileName;  
  constructor(private _service:BookService) { }
  readExcel(event){
    
    this.selectFile = <File>event.target.files[0];
 
    this.fileName.nativeElement.value=this.selectFile.name;
  }

  ngOnInit(): void {
  }
  add(){
    let formData = new FormData();  
    formData.append('upload', this.fileInput.nativeElement.files[0]) 
    this._service.insertBooks(formData).subscribe(
      res=>console.log(res),
      err=>console.log(err)
    );
  }
}
