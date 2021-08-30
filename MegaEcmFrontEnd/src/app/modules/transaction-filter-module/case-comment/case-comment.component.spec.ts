import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CaseCommentComponent } from './case-comment.component';

describe('CaseCommentComponent', () => {
  let component: CaseCommentComponent;
  let fixture: ComponentFixture<CaseCommentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CaseCommentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CaseCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
