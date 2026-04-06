import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelaTarefa } from './tela-tarefa';

describe('TelaProjetos', () => {
  let component: TelaTarefa;
  let fixture: ComponentFixture<TelaTarefa>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelaTarefa]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TelaTarefa);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
