'use strict';

describe('Controller: DojolistCtrl', function () {

  // load the controller's module
  beforeEach(module('auskfApp'));

  var DojolistCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    DojolistCtrl = $controller('DojolistCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(DojolistCtrl.awesomeThings.length).toBe(3);
  });
});
