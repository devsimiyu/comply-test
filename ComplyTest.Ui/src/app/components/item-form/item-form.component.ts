import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Item, ItemFormService } from '../../services/item-form/item-form.service';
import { Subscription, finalize } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-form',
  templateUrl: './item-form.component.html',
  styleUrl: './item-form.component.scss',
  providers: [ItemFormService],
})
export class ItemFormComponent implements OnInit, OnDestroy {
  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
  });
  isLoading = false;
  isSubmitting = false;
  subscriptions = new Subscription();

  constructor(private route: ActivatedRoute, private service: ItemFormService) { }

  ngOnInit(): void {
    this.load();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  hasError(field: string, error: string): boolean {
    const control = this.form.get(field)!;
    return control.touched && control.hasError(error);
  }

  load(): void {
    const id = this.route.snapshot.params['id'];
    if (!id) return;
    this.isLoading = true
    this.subscriptions.add(this.service.details(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: item => this.form.patchValue(item),
        error: () => alert('Oops! Failed to load item!')
      }));
  }

  submit(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid) return;
    const item = this.form.value as Item;
    const id = this.route.snapshot.params['id'];
    this.isSubmitting = true;
    id ? this.update(id, item) : this.create(item);
  }

  create(item: Item): void {
    this.subscriptions.add(this.service.create(item)
      .pipe(finalize(() => this.isSubmitting = false))
      .subscribe({
        next: () => alert('Item added successfully!'),
        error: () => alert('Oops! Failed to add item!'),
      }));
  }
  
  update(id: string, item: Item): void {
    this.subscriptions.add(this.service.update(id, item)
      .pipe(finalize(() => this.isSubmitting = false))
      .subscribe({
        next: () => alert('Item updated successfully!'),
        error: () => alert('Oops! Failed to update item!'),
      }));
  }
}
