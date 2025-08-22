import { Component, AfterViewInit, OnDestroy } from '@angular/core';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface Task {
  label: string;
  completed: boolean;
}

@Component({
  selector: 'app-videocall',
  templateUrl: './videocall.component.html',
  styleUrls: ['./videocall.component.css'],
  imports: [CheckboxModule, FormsModule, CommonModule],
  standalone: true,
})
export class VideocallComponent implements AfterViewInit, OnDestroy {
  expertName = '';
  clientName = '';

  tasks: Task[] = [
    { label: 'Task 1', completed: false },
    { label: 'Task 2', completed: false },
    { label: 'Task 3', completed: false },
    { label: 'Task 4', completed: false },
    { label: 'Task 5', completed: false },
    { label: 'Task 6', completed: false },
    { label: 'Task 7', completed: false },
    { label: 'Task 8', completed: false },
    { label: 'Task 9', completed: false },
    { label: 'Task 10', completed: false },
  ];

  ngAfterViewInit(): void {
    const closeBtn = document.querySelector(
      '.btn-close-right'
    ) as HTMLButtonElement;
    const rightSide = document.querySelector('.right-side') as HTMLElement;
    const expandBtn = document.querySelector(
      '.expand-btn'
    ) as HTMLButtonElement;

    closeBtn?.addEventListener('click', () => {
      rightSide.classList.remove('show');
      expandBtn.classList.add('show');
    });

    expandBtn?.addEventListener('click', () => {
      rightSide.classList.add('show');
      expandBtn.classList.remove('show');
    });

    this.handleResize();
    window.addEventListener('resize', this.handleResize);
  }

  ngOnDestroy(): void {
    window.removeEventListener('resize', this.handleResize);
  }

  handleResize = (): void => {
    const expandBtn = document.querySelector(
      '.expand-btn'
    ) as HTMLButtonElement;
    if (window.innerWidth > 900) {
      expandBtn.classList.remove('show');
    } else {
      expandBtn.classList.add('show');
    }
  };

  toggleTaskCompletion(task: Task): void {
    console.log('I clicked ');
    task.completed = !task.completed;
  }
}
