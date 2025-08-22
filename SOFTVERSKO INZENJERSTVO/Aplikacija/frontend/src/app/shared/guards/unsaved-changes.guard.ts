import { CanDeactivateFn } from '@angular/router';
import { Observable } from 'rxjs';

interface CanComponentDeactivate {
    canDeactivate: () => Observable<boolean> | Promise<boolean> | boolean;
}

export const canDeactivateGuard: CanDeactivateFn<CanComponentDeactivate> = (component: CanComponentDeactivate) => {
    return component.canDeactivate ? component.canDeactivate() : true;
};