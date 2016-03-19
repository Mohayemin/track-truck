var appModule = angular.module('trackTruck', [
    'ngAnimate',
    'navigation',
    'utilModule',
    'attendance',
    'truck',
    'client',
    'trip',
    'employee',
    'user',
    'auth',
    'ui.bootstrap',
    'tableSort',
    'ngTagsInput',
    'ngRoute']);

appModule.config([
    '$locationProvider',
    'uibDatepickerPopupConfig',
    'uibDatepickerConfig',
    'uibTimepickerConfig',
    'dateFormat',
    function ($locationProvider, uibDatepickerPopupConfig, uibDatepickerConfig, uibTimepickerConfig, dateFormat) {
        console.log('configuring app');

        $locationProvider.html5Mode(false);
        uibDatepickerPopupConfig.datepickerPopup = dateFormat;
        uibDatepickerConfig.showWeeks = false;
        uibDatepickerPopupConfig.appendToBody = true;
        uibTimepickerConfig.showSpinners = false;
    }
]);

appModule.run([
    '$rootScope',
    'tripStatus',
    function($rootScope, tripStatus) {
        $rootScope.tripStatus = tripStatus;
    }
]);

appModule.value('_', window._);
appModule.constant('dateFormat', 'MMMM dd, yyyy');
