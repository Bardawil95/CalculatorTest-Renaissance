import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule]
})
export class AppComponent {
  start: number = 0;
  amount: number = 0;
  result: number | null = null;
  error: string | null = null;
  showModal: boolean = false;

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) { }

  toggleModal() {
    this.showModal = !this.showModal;
  }

  calculate(op: string) {
    this.error = null;
    this.result = null;

    // Frontend validation
    if (this.start === null || this.start === undefined || isNaN(this.start)) {
      this.error = "Start value must be a valid number";
      return;
    }

    if (this.amount === null || this.amount === undefined || isNaN(this.amount)) {
      this.error = "Amount must be a valid number";
      return;
    }

    if (!Number.isInteger(this.start) || !Number.isInteger(this.amount)) {
      this.error = "Only whole numbers (integers) are allowed";
      return;
    }

    const url = `https://localhost:7106/api/calculator/${op}`;
    const payload = {
      start: Number(this.start),
      amount: Number(this.amount)
    };

    this.http.post<any>(url, payload).subscribe({
      next: (res) => {
        this.result = res.result;
        this.cdr.detectChanges();
      },
      error: (err) => {
        if (err.error && err.error.message) {
          this.error = err.error.message;
          if (err.error.details) {
            this.error += ` - ${err.error.details}`;
          }
        } else {
          this.error = "Calculation error occurred";
        }
        this.cdr.detectChanges();
      }
    });
  }
}
