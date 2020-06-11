import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BookTable } from '../ModelView/book-table';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private _http: HttpClient) { }
  insertBooks(file:FormData){
    let headers = new HttpHeaders();  
    headers.append('Content-Type', 'multipart/form-data');  
    headers.append('Accept', 'application/json');
    const httpOptions = { headers: headers }; 

return this._http.post(environment.api+"/AddExcel",file,httpOptions);
  }
  insertBook(form:FormData){
    let headers = new HttpHeaders();  
    headers.append('Content-Type', 'multipart/form-data');  
    headers.append('Accept', 'application/json');
    const httpOptions = { headers: headers }; 
    
return this._http.post(environment.api+"/Books",form,httpOptions);
  }
  getBooks(startIndex:number,numberOfBooks:number){
    return this._http.get<BookTable[]>(environment.api+"/Books?"+"StartIndex="+startIndex+"&NumberOfBooks="+numberOfBooks);
  }
  getOne(id){
    return this._http.get(environment.api+"/GetOne/"+id);
  }
}
