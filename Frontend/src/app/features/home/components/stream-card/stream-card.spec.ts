import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StreamCard } from './stream-card';

describe('StreamCard', () => {
  let component: StreamCard;
  let fixture: ComponentFixture<StreamCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StreamCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StreamCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
