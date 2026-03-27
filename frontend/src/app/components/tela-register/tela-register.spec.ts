import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelaRegister } from './tela-register';

describe('TelaRegister', () => {
  let component: TelaRegister;
  let fixture: ComponentFixture<TelaRegister>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelaRegister]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TelaRegister);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
