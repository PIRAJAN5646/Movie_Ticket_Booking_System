import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StreamSection } from './stream-section';

describe('StreamSection', () => {
  let component: StreamSection;
  let fixture: ComponentFixture<StreamSection>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StreamSection]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StreamSection);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
