import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TodoItem } from './todo-item';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { By } from '@angular/platform-browser';
import Todo from '../../../core/models/todo.model';
import { Component } from '@angular/core';

// Host component to test outputs
@Component({
  template: `<app-todo-item [todo]="todo" (toggleCompleted)="onToggle()" (delete)="onDelete()"></app-todo-item>`,
  imports: [TodoItem],
})
class TestHostComponent {
  todo: Todo = { id: "129ae3f0-449e-4752-a98f-8566e5dcbbd3", name: 'Test Todo', isCompleted: false };
  toggled = false;
  deleted = false;
  onToggle() { this.toggled = true; }
  onDelete() { this.deleted = true; }
}

describe('TodoItem', () => {
  let fixture: ComponentFixture<TestHostComponent>;
  let host: TestHostComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatCheckboxModule, MatIconModule, TodoItem, TestHostComponent]
    }).compileComponents();
    fixture = TestBed.createComponent(TestHostComponent);
    host = fixture.componentInstance;
    await fixture.whenStable();
    fixture.detectChanges();
  });

  it('should display todo name', async () => {
    const label = fixture.debugElement.query(By.css('label'));
    console.log(label);
    expect(label && label.nativeElement.textContent.trim()).toContain('Test Todo');
  });

  it('should emit toggleCompleted when checkbox is changed', () => {
    const checkbox = fixture.debugElement.query(By.css('mat-checkbox'));
    checkbox.triggerEventHandler('change', {});
    expect(host.toggled).toBeTrue();
  });

  it('should emit delete when delete button is clicked', () => {
    const button = fixture.debugElement.query(By.css('.delete-btn'));
    button.triggerEventHandler('click', {});
    expect(host.deleted).toBeTrue();
  });
});