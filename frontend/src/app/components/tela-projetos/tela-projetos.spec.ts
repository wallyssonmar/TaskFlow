import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelaProjetos } from './tela-projetos';

describe('TelaProjetos', () => {
  let component: TelaProjetos;
  let fixture: ComponentFixture<TelaProjetos>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelaProjetos]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TelaProjetos);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
