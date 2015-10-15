var appModule = angular.module('trackTruck', [
    'ngAnimate',
    'navigation',
    'utilModule',
    'attendance',
    'truck',
    'client',
    'trip',
    'employee',
    'warehouse',
    'user',
    'auth',
    'ui.bootstrap',
    'tableSort',
    'ngRoute']);

appModule.config([
    '$locationProvider',
    'datepickerPopupConfig',
    'datepickerConfig',
    'timepickerConfig',
    function ($locationProvider, datepickerPopupConfig, datepickerConfig, timepickerConfig) {
        console.log('configuring app');

        $locationProvider.html5Mode(false);
        datepickerPopupConfig.datepickerPopup = 'MMMM dd, yyyy';
        datepickerConfig.showWeeks = false;
        datepickerPopupConfig.appendToBody = true;
        timepickerConfig.showSpinners = false;
    }
]);

appModule.value('_', window._);
appModule.value('dateFormat', 'MMMM dd, yyyy');