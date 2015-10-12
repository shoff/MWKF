'use strict';

angular.module('mwkfApp')
  .controller('DojolistCtrl', function ($scope, $http, API_URL) {

    $http.get(API_URL + 'dojos').then(
      function (success) {
        $scope.jobs = success.data;
      }, function (error) {
        //alert('danger', 'Sorry   ',  error.data.message, 2000);
      });


  });
