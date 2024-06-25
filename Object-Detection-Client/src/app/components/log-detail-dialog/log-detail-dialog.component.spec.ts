import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogDetailDialogComponent } from './log-detail-dialog.component';

describe('LogDetailDialogComponent', () => {
  let component: LogDetailDialogComponent;
  let fixture: ComponentFixture<LogDetailDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LogDetailDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LogDetailDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
