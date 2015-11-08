/**
 * Created by shoff on 10/9/2015.
 */
'use strict';

angular
  .module('mwkfApp')
  .config(function ($urlRouterProvider, $stateProvider, $translateProvider, $httpProvider) {

    //// if posting to an Asp.Net MVC action then this should be set so that it can grab the values.
    //$httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
    //
    ///**
    // * The workhorse; converts an object to x-www-form-urlencoded serialization.
    // * @param {Object} obj
    // * @return {String}
    // */
    //var param = function(obj) {
    //  var query = '', name, value, fullSubName, subName, subValue, innerObj, i;
    //
    //  for(name in obj) {
    //    value = obj[name];
    //
    //    if(value instanceof Array) {
    //      for(i=0; i<value.length; ++i) {
    //        subValue = value[i];
    //        fullSubName = name + '[' + i + ']';
    //        innerObj = {};
    //        innerObj[fullSubName] = subValue;
    //        query += param(innerObj) + '&';
    //      }
    //    }
    //    else if(value instanceof Object) {
    //      for(subName in value) {
    //        subValue = value[subName];
    //        fullSubName = name + '[' + subName + ']';
    //        innerObj = {};
    //        innerObj[fullSubName] = subValue;
    //        query += param(innerObj) + '&';
    //      }
    //    }
    //    else if(value !== undefined && value !== null)
    //      query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
    //  }
    //  return query.length ? query.substr(0, query.length - 1) : query;
    //};

    //// Override $http service's default transformRequest
    //$httpProvider.defaults.transformRequest = [function(data) {
    //  return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
    //}];


    $urlRouterProvider.otherwise('/');
    $stateProvider
      .state('main', {
        url: '/',
        templateUrl: '/views/main.html'
      })
      .state('dojos', {
        url: '/dojolist',
        templateUrl: '/views/dojos.html',
        controller: 'DojolistCtrl'
      })
      .state('about', {
        url: '/about',
        templateUrl: '/views/about.html'
      })
      .state('contact', {
        url: '/contact',
        templateUrl: '/views/contact.html'
      })
      .state('iado', {
        url: '/iado',
        templateUrl: '/views/iado.html'
      })
      .state('iadoStudy', {
        url: '/iado/study',
        templateUrl: '/views/iado-study.html'
      })
      .state('login', {
        url: '/login',
        templateUrl: '/views/login.html',
        controller: 'LoginCtrl'
      });

    $translateProvider.translations('en', {
      FOOTER_TEXT: ' HD and C for you!!',
      SIGN_IN_OR: 'Please Log In, or',
      SIGN_UP: ' Sign up',
      USERNAME_OR: 'Username or email',
      PASSWORD: 'Password',
      FORGOT_PASSWORD: 'Forgot Password?',
      CONFIRM_PASSWORD: 'Confirm Password',
      REMEMBER_ME: 'Remember me',
      LOGIN: 'LogIn',
      PASSWORDS_NON_MATCH: 'passwords do not match'
    });


    $translateProvider.preferredLanguage('en');
    $translateProvider.useSanitizeValueStrategy('escape');

  })
  .constant('API_URL', 'http://localhost:8999/api/v1/');
