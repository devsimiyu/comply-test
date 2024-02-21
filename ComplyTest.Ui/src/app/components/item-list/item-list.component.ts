import { Component, OnDestroy } from '@angular/core';
import { Subscription, catchError, finalize, ignoreElements, map, of, shareReplay } from 'rxjs';
import { ItemListService } from '../../services/item-list/item-list.service';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrl: './item-list.component.scss',
  providers: [ItemListService],
})
export class ItemListComponent implements OnDestroy {
  items$ = this.service.list().pipe(shareReplay(1));
  error$ = this.items$.pipe(
    ignoreElements(),
    catchError(() => of('Oops! Failed to load item list!'))
  );
  subscriptions = new Subscription();
  isCalculatingFactorial = false;

  constructor(private service: ItemListService) { }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  delete(id: string): void {
    if (confirm('Are you sure to delete item?')) {
      this.subscriptions.add(this.service.delete(id)
        .subscribe({
          next: () => alert('Item deleted successfully!'),
          error: () => alert('Oops! Failed to delete item!')
        }));
    }
  }

  factorial(): void {
    this.isCalculatingFactorial = true;
    this.items$ = this.service.factorial()
      .pipe(finalize(() => this.isCalculatingFactorial = false))
      .pipe(shareReplay(1));
  }
}
