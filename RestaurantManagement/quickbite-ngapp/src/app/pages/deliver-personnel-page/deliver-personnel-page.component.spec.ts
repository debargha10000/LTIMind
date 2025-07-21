import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliverPersonnelPageComponent } from './deliver-personnel-page.component';

describe('DeliverPersonnelPageComponent', () => {
  let component: DeliverPersonnelPageComponent;
  let fixture: ComponentFixture<DeliverPersonnelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeliverPersonnelPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliverPersonnelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
