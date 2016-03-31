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
    'ui.bootstrap.datetimepicker',
    'tableSort',
    'ngTagsInput',
    'ngRoute']);

appModule.config([
    '$locationProvider',
    'uibDatepickerPopupConfig',
    'uibDatepickerConfig',
    function ($locationProvider, uiDatetimePickerConfig) {
        $locationProvider.html5Mode(false);
        uiDatetimePickerConfig.appendToBody = true;
        uiDatetimePickerConfig.showWeeks = false;
        uiDatetimePickerConfig.closeOnDateSelection = false;
    }
]);

appModule.run([
    '$rootScope',
    'tripStatus',
    'dateFormat',
    'dateTimeFormat',
    'uiDatetimePickerConfig',
    function ($rootScope, tripStatus, dateFormat, dateTimeFormat, uiDatetimePickerConfig) {
        $rootScope.tripStatus = tripStatus;
        $rootScope.dateFormat = dateFormat;
        $rootScope.dateTimeFormat = dateTimeFormat;
    }
]);

appModule.value('_', window._);
appModule.constant('dateFormat', 'MMMM dd, yyyy');
appModule.constant('dateTimeFormat', 'MMMM dd, yyyy hh:mm a');
