import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectsDelayComponent } from './projects-delay.component';

describe('ProjectsDelayComponent', () => {
  let component: ProjectsDelayComponent;
  let fixture: ComponentFixture<ProjectsDelayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProjectsDelayComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProjectsDelayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
