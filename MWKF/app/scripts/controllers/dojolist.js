'use strict';

angular.module('auskfApp')
  .controller('DojolistCtrl', function ($scope, $http, API_URL) {

    $http.get(API_URL + 'dojos/paged/1').then(
      function (success) {
        $scope.dojolist = success.data.currentPage;
        $scope.federationSelect = "";
        $scope.stateSelect = "";
      }, function (error) {
        //alert('danger', 'Sorry   ',  error.data.message, 2000);
      });

    $http.get(API_URL + 'federations').then(
      function (success) {
        $scope.federationlist = success.data;
      }, function (error) {
        alert(error.data.message);
        //alert('danger', 'Sorry   ',  error.data.message, 2000);
      });

    $http.get(API_URL + 'dojos/states').then(
      function (success) {
        $scope.stateslist = success.data;
      }, function (error) {
        //alert('danger', 'Sorry   ',  error.data.message, 2000);
      });

    $scope.selectDojosByState = function () {
      $scope.federationSelect = "";
      $http.get(API_URL + 'dojos/paged/1?federationId=&state=' + $scope.stateSelect).then(
        function (success) {
          $scope.dojolist = success.data.currentPage;
        }, function (error) {
          //alert('danger', 'Sorry   ',  error.data.message, 2000);
        });
    };

    $scope.selectDojoByFederation = function () {
      $scope.stateSelect = "";
      $http.get(API_URL + 'dojos/paged/1?federationId=' + $scope.federationSelect + '&state=').then(
        function (success) {
          $scope.dojolist = success.data.currentPage;
        }, function (error) {
          //alert('danger', 'Sorry   ',  error.data.message, 2000);
        });
    };

  });
