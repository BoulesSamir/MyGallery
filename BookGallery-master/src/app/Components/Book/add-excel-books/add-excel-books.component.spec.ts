import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddExcelBooksComponent } from './add-excel-books.component';

describe('AddExcelBooksComponent', () => {
  let component: AddExcelBooksComponent;
  let fixture: ComponentFixture<AddExcelBooksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddExcelBooksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddExcelBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
