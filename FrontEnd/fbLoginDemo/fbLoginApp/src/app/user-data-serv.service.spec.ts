import { TestBed } from '@angular/core/testing';

import { UserDataServService } from './user-data-serv.service';

describe('UserDataServService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserDataServService = TestBed.get(UserDataServService);
    expect(service).toBeTruthy();
  });
});
