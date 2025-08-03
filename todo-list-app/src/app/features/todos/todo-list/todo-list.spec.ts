import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TodoList } from './todo-list';
import { TodoService } from '../../../core/services/todo.service';
import { of, throwError } from 'rxjs';
import Todo from '../../../core/models/todo.model';
import { FormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { TodoItem } from '../todo-item/todo-item';

const mockTodos: Todo[] = [
  { id: 'dbcfbc0d-a880-4279-be88-f407bbf13b50', name: 'Test 1', isCompleted: false },
  { id: '2849fb58-a7fd-4250-af39-c125cc7a3268', name: 'Test 2', isCompleted: true },
];

describe('TodoList', () => {
  let component: TodoList;
  let fixture: ComponentFixture<TodoList>;
  let todoServiceSpy: jasmine.SpyObj<TodoService>;

  beforeEach(async () => {
    todoServiceSpy = jasmine.createSpyObj('TodoService', ['getTodos', 'addTodo', 'toggleTodoCompleted', 'deleteTodo']);
    await TestBed.configureTestingModule({
      imports: [FormsModule, MatTabsModule, MatButtonModule, MatProgressSpinnerModule, TodoItem, TodoList],
      providers: [
        { provide: TodoService, useValue: todoServiceSpy }
      ]
    }).compileComponents();
    fixture = TestBed.createComponent(TodoList);
    component = fixture.componentInstance;
  });

  it('should load todos on init', () => {
    todoServiceSpy.getTodos.and.returnValue(of(mockTodos));
    fixture.detectChanges();
    expect(component.todos.length).toBe(2);
    expect(component.loading).toBeFalse();
  });

  it('should handle load error', () => {
    todoServiceSpy.getTodos.and.returnValue(throwError(() => new Error('fail')));
    fixture.detectChanges();
    expect(component.loadError).toBe('Failed to load items.');
    expect(component.loading).toBeFalse();
  });

  it('should add a todo', () => {
    todoServiceSpy.getTodos.and.returnValue(of([]));
    todoServiceSpy.addTodo.and.returnValue(of({ id: '3', name: 'New', isCompleted: false }));
    fixture.detectChanges();
    component.addTodo('New');
    expect(component.todos.length).toBe(1);
    expect(component.todos[0].name).toBe('New');
  });

  it('should not add empty todo', () => {
    todoServiceSpy.getTodos.and.returnValue(of([]));
    fixture.detectChanges();
    component.addTodo('   ');
    expect(component.todos.length).toBe(0);
  });

  it('should toggle todo completed', () => {
    todoServiceSpy.getTodos.and.returnValue(of([mockTodos[0]]));
    todoServiceSpy.toggleTodoCompleted.and.returnValue(of({}));
    fixture.detectChanges();
    component.toggleTodoCompleted(component.todos[0]);
    expect(component.todos[0].isCompleted).toBeTrue();
  });

  it('should handle toggle error', () => {
    todoServiceSpy.getTodos.and.returnValue(of([mockTodos[0]]));
    todoServiceSpy.toggleTodoCompleted.and.returnValue(throwError(() => new Error('fail')));
    fixture.detectChanges();
    component.toggleTodoCompleted(component.todos[0]);
    expect(component.loadError).toBe('Failed to update item.');
  });

  it('should delete a todo', () => {
    todoServiceSpy.getTodos.and.returnValue(of([mockTodos[0]]));
    todoServiceSpy.deleteTodo.and.returnValue(of({}));
    spyOn(window, 'confirm').and.returnValue(true);
    fixture.detectChanges();
    component.onDeleteButtonClick(component.todos[0]);
    expect(component.todos.length).toBe(0);
  });

  it('should handle delete error', () => {
    todoServiceSpy.getTodos.and.returnValue(of([mockTodos[0]]));
    todoServiceSpy.deleteTodo.and.returnValue(throwError(() => new Error('fail')));
    spyOn(window, 'confirm').and.returnValue(true);
    fixture.detectChanges();
    component.onDeleteButtonClick(component.todos[0]);
    expect(component.loadError).toBe('Failed to delete item.');
  });
});