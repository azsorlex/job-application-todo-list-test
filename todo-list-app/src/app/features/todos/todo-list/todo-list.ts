import { Component, OnInit } from '@angular/core';
import Todo from '../../../core/models/todo.model';
import { TodoService } from '../../../core/services/todo.service';

@Component({
  selector: 'app-todo-list',
  imports: [],
  templateUrl: './todo-list.html',
  styleUrl: './todo-list.css'
})
export class TodoList implements OnInit {
  todos: Todo[] = [];

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.todoService.getTodos().subscribe({
      next: (data) => {
        this.todos = data;
      },
      error: (err: unknown) => {
        console.error('Failed to load todos:', err);
      },
    });
  }
}