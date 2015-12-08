'use strict';

angular.module('auskfApp')
  .controller('LoginCtrl', function ($scope, $http, API_URL) {
    $(function() {

      $('#login-form-link').click(function(e) {
        $("#login-form").delay(100).fadeIn(100);
        $("#register-form").fadeOut(100);
        $('#register-form-link').removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
      });
      $('#register-form-link').click(function(e) {
        $("#register-form").delay(100).fadeIn(100);
        $("#login-form").fadeOut(100);
        $('#login-form-link').removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
      });

    });

    this.awesomeThings = [
      'HTML5 Boilerplate',
      'AngularJS',
      'Karma'
    ];

    $http.get(API_URL + 'kendoranks').then(
      function (success) {
        $scope.kendoranklist = success.data;
      }, function (error) {
        alert(error.data.message);
        //alert('danger', 'Sorry   ',  error.data.message, 2000);
      });

    $scope.newUser = null;

    $scope.save = function (){
      $http.post(API_URL + "users", $scope.newUser).then(
        function(success) {
          alert('Saved'); //replace later
          $scope.newUser = null;
        }, function (error) {
          alert('error');
          //alert('danger', 'Sorry   ',  error.data.message, 2000);
        });
    };

  });
