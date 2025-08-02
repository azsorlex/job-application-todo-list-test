import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import Todo from '../models/todo.model';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  private readonly apiUrl = 'https://localhost:7071/';

  constructor(private http: HttpClient) {}

  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(`${this.apiUrl}todos`);
  };
};