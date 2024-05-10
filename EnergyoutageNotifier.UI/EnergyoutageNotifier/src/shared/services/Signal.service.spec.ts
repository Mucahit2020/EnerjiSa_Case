/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SignalService } from './Signal.service';

describe('Service: SignalR', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SignalService]
    });
  });

  it('should ...', inject([SignalService], (service: SignalService) => {
    expect(service).toBeTruthy();
  }));
});
