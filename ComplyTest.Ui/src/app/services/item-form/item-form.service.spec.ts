import { TestBed } from '@angular/core/testing';

import { ItemFormService } from './item-form.service';

describe('ItemFormService', () => {
  let service: ItemFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ItemFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
