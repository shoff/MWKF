'use strict';

describe('Service: DojoService', function () {

  // load the service's module
  beforeEach(module('mwkfApp'));

  // instantiate service
  var DojoService;
  beforeEach(inject(function (_DojoService_) {
    DojoService = _DojoService_;
  }));

  it('should do something', function () {
    expect(!!DojoService).toBe(true);
  });

});
