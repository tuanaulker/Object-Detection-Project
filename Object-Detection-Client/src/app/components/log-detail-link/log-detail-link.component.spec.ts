import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogDetailLinkComponent } from './log-detail-link.component';

describe('LogDetailLinkComponent', () => {
  let component: LogDetailLinkComponent;
  let fixture: ComponentFixture<LogDetailLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LogDetailLinkComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LogDetailLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
