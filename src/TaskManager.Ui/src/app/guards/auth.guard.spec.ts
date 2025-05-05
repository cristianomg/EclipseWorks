import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { authorizeGuard } from './auth.guard';

describe('authorizeGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => authorizeGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
