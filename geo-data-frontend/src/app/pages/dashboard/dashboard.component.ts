import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { GeographieDataService, GeographieData } from '../../services/geographie-data.service';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  data: GeographieData[] = [];
  displayedColumns: string[] = ['openbareruimte', 'huisnummer', 'woonplaats', 'postcode', 'acties'];

  constructor(private geoService: GeographieDataService, private router: Router) { }

  ngOnInit(): void {
    this.geoService.getAll().subscribe({
      next: result => this.data = result,
      error: err => console.error('Fout bij het ophalen: ', err)
    });
  }

  goToForm(id: number): void {
    this.router.navigate(['/form', id]);
  }

  deleteRecord(id: number): void {
    if (confirm('Weet je zeker dat je deze record wilt verwijderen?')) {
      this.geoService.delete(id).subscribe(() => {
        this.data = this.data.filter(d => d.id !== id);
      });
    }
  }
}
