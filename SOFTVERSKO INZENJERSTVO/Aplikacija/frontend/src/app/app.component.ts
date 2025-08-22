import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from './app.state';
import { Subscription } from 'rxjs';
import { getLoadingPage } from './shared/store/loading.selectors';

@Component({
  selector: 'app-root',
  styles: [`
  .p-progress-spinner-circle {
    stroke: var(--primary-color);
    animation: none;
    }
  `],
  templateUrl: './app.component.html',
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit, OnDestroy {
  private isLoadingPageSub: Subscription = new Subscription();

  public isLoadingPage: boolean = false;

  constructor(private readonly _store: Store<AppState>) { }

  public ngOnInit(): void {
    this.isLoadingPageSub = this._store
      .select(getLoadingPage)
      .subscribe((isLoading) => {
        this.isLoadingPage = isLoading;
      });
  }

  public ngOnDestroy(): void {
    if (this.isLoadingPageSub) {
      this.isLoadingPageSub.unsubscribe();
    }
  }
}
