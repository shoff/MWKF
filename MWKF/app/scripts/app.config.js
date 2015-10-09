/**
 * Created by shoff on 10/9/2015.
 */
'use strict';

angular
  .module('mwkfApp').config(function ($stateProvider) {
    $stateProvider
      .state('main', {
        url: '/',
        templateUrl: '/views/main.html'
      })
      .state('dojos', {
        url: '/dojolist',
        templateUrl: '/views/dojos.html'
      })
      .state('login', {
      url: '/login',
      templateUrl: '/views/login.html'
    });
  })
