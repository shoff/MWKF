'use strict';

describe('Service: kendoranks', function () {

  // load the service's module
  beforeEach(module('mwkfApp'));

  // instantiate service
  var kendoranks;
  beforeEach(inject(function (_kendoranks_) {
    kendoranks = _kendoranks_;
  }));

  it('should do something', function () {
    expect(!!kendoranks).toBe(true);
  });

});
