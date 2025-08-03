import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import Todo from '../../../core/models/todo.model';

@Component({
  selector: 'app-todo-item',
  imports: [
    MatIconModule,
    MatCheckboxModule,
  ],
  templateUrl: './todo-item.html',
  styleUrl: './todo-item.css',
})
export class TodoItem {
  @Input() todo!: Todo;
  @Output() toggleCompleted = new EventEmitter<void>();
  @Output() delete = new EventEmitter<void>();

  onToggleCompleted() {
    this.toggleCompleted.emit();
  }

  onDelete() {
    this.delete.emit();
  }
}
