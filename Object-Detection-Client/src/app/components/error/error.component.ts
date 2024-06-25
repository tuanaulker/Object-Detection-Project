import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrl: './error.component.scss',
  providers: [DialogService, FormsModule]
})
export class ErrorComponent implements OnInit {
  remainingTime: number = 5

  constructor(private router: Router) { }

  ngOnInit(): void {
    const intervalId = setInterval(() => {
      this.remainingTime--; 
      if (this.remainingTime <= 0) {
        clearInterval(intervalId); 
        this.router.navigate(['/']); 
      }
    }, 1000); 
  }
}
