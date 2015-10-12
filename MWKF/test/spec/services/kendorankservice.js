'use strict';

describe('Service: KendoRankService', function () {

  // load the service's module
  beforeEach(module('mwkfApp'));

  // instantiate service
  var KendoRankService;
  beforeEach(inject(function (_KendoRankService_) {
    KendoRankService = _KendoRankService_;
  }));

  it('should do something', function () {
    expect(!!KendoRankService).toBe(true);
  });

});
