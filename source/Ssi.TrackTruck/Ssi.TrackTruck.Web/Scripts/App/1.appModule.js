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
    'datepickerPopupConfig',
    'datepickerConfig',
    'timepickerConfig',
    'dateFormat',
    function ($locationProvider, datepickerPopupConfig, datepickerConfig, timepickerConfig, dateFormat) {
        console.log('configuring app');

        $locationProvider.html5Mode(false);
        datepickerPopupConfig.datepickerPopup = dateFormat;
        datepickerConfig.showWeeks = false;
        datepickerPopupConfig.appendToBody = true;
        timepickerConfig.showSpinners = false;
    }
]);

appModule.value('_', window._);
appModule.constant('dateFormat', 'MMMM dd, yyyy');
