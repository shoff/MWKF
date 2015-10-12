'use strict';

describe('Directive: ValidateEquals', function () {

  // load the directive's module
  beforeEach(module('mwkfApp'));

  var element,
    scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));

  it('should make hidden element visible', inject(function ($compile) {
    element = angular.element('<-validate-equals></-validate-equals>');
    element = $compile(element)(scope);
    expect(element.text()).toBe('this is the ValidateEquals directive');
  }));
});
