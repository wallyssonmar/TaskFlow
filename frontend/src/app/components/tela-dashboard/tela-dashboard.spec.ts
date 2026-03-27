import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelaDashboard } from './tela-dashboard';

describe('TelaDashboard', () => {
  let component: TelaDashboard;
  let fixture: ComponentFixture<TelaDashboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelaDashboard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TelaDashboard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
