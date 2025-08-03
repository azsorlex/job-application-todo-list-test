import { Component, OnInit } from '@angular/core';
import Todo from '../../../core/models/todo.model';
import { TodoService } from '../../../core/services/todo.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-todo-list',
  imports: [
    FormsModule
  ],
  templateUrl: './todo-list.html',
  styleUrl: './todo-list.css'
})
export class TodoList implements OnInit {
  todos: Todo[] = [];
  newTodoName: string = '';
  loading: boolean = true;
  loadError: string | null = null;

  constructor(private todoService: TodoService) { }

  get incompleteTodos(): Todo[] {
    return this.todos.filter(todo => !todo.isCompleted);
  }

  get completeTodos(): Todo[] {
    return this.todos.filter(todo => todo.isCompleted);
  }

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.loading = true;
    this.loadError = null;
    this.todoService.getTodos().subscribe({
      next: (data) => {
        this.todos = data;
        this.loading = false;
      },
      error: (err: unknown) => {
        this.loading = false;
        this.loadError = 'Failed to load items.'
        console.error('Failed to load todos:', err);
      },
    });
  }

  addTodo(name: string): void {
    const newTodo: Todo = { name };

    this.todoService.addTodo(newTodo).subscribe({
      next: (data) => {
        if (data && data.id) {
          this.todos.push(data);
        } else {

        }
      },
      error: (err: unknown) => {
        this.loadError = 'Failed to add item.';
        console.error("Failed to add todo:", err);
      }
    })
  }

  toggleTodoCompleted(todo: Todo): void {
    this.todoService.toggleTodoCompleted(todo).subscribe({
      next: () => {
        todo.isCompleted = !todo.isCompleted;
      },
      error: (err: unknown) => {
        this.loadError = 'Failed to update item.';
        console.error('Failed to toggle todo:', err);
      }
    });
  }
}