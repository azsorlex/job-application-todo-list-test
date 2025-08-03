import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import Todo from '../models/todo.model';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  private readonly apiUrl = 'https://localhost:7071/api/todos';

  constructor(private http: HttpClient) {}

  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.apiUrl);
  };

  addTodo(todo: Todo): Observable<Todo> {
    return this.http.post<Todo>(`${this.apiUrl}/add`, todo)
  }

  toggleTodoCompleted(todo: Todo): Observable<Object> {
    return this.http.patch(`${this.apiUrl}/toggle/${todo.id}`, todo);
  }

  deleteTodo(todo: Todo): Observable<Object> {
    return this.http.delete(`${this.apiUrl}/delete/${todo.id}`);
  }
};
