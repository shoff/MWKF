'use strict';

describe('Controller: FederationcontrollerCtrl', function () {

  // load the controller's module
  beforeEach(module('mwkfApp'));

  var FederationcontrollerCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    FederationcontrollerCtrl = $controller('FederationcontrollerCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(FederationcontrollerCtrl.awesomeThings.length).toBe(3);
  });
});
