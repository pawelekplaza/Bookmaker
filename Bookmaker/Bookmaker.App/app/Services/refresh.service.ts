import { Injectable, EventEmitter, Output } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class RefreshService {
    @Output() refresh: EventEmitter<any> = new EventEmitter();

    emitRefresh(): Observable<any> {
        this.refresh.emit();
        return this.refresh.asObservable();
    }
}