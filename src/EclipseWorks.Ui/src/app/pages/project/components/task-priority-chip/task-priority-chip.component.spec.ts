import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskPriorityChipComponent } from './task-priority-chip.component';

describe('TaskPriorityChipComponent', () => {
  let component: TaskPriorityChipComponent;
  let fixture: ComponentFixture<TaskPriorityChipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskPriorityChipComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskPriorityChipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
