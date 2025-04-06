import { TestBed } from '@angular/core/testing';

import { GeographieDataService } from './geographie-data.service';

describe('GeographieDataService', () => {
  let service: GeographieDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GeographieDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
