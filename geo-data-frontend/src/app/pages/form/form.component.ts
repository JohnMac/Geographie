import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GeographieDataService, GeographieData } from '../../services/geographie-data.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss'
})
export class FormComponent implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  id!: number;

  constructor(
    private fb: FormBuilder,
    private geoService: GeographieDataService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      id: [0],
      openbareruimte: [''],
      huisnummer: [0],
      huisletter: [''],
      huisnummertoevoeging: [0],
      postcode: [''],
      woonplaats: [''],
      gemeente: [''],
      provincie: [''],
      nummeraanduiding: [''],
      verblijfsobjectgebruiksdoel: [''],
      oppervlakteverblijfsobject: [0],
      verblijfsobjectstatus: [''],
      objectId: [''],
      objectType: [''],
      nevenadres: [''],
      pandId: [''],
      pandstatus: [''],
      pandbouwjaar: [0],
      x: [0],
      y: [0],
      lon: [0],
      lat: [0]
    });

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.id = +idParam;
      this.isEditMode = true;
      this.geoService.get(this.id).subscribe(data => {
        this.form.patchValue(data);
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    const model = this.form.value as GeographieData;
    if (this.isEditMode) {
      this.geoService.update(this.id, model).subscribe(() => {
        this.router.navigate(['/'])
      });
    } else {
      this.geoService.create(model).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/']);
  }
}
