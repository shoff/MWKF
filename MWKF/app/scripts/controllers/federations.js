'use strict';

angular.module('auskfApp')
  .controller('FederationCtrl', function ($scope, $http, API_URL) {

    $http.get(API_URL + 'federations').then(
      function (success) {
        $scope.federationlist = success.data;
      }, function (error) {
        alert(error.data.message);
          //alert('danger', 'Sorry   ',  error.data.message, 2000);
      });

  });
