import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembershipBanner } from './membership-banner';

describe('MembershipBanner', () => {
  let component: MembershipBanner;
  let fixture: ComponentFixture<MembershipBanner>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MembershipBanner]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MembershipBanner);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
