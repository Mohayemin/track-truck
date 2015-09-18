var trackTruck = angular.module('trackTruck', [
    'navigation',
    'utilModule',
    'truck',
    'client',
    'ui.bootstrap',
    'tableSort',
    'ngRoute']);

trackTruck.config([
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

trackTruck.value('_', window._);
trackTruck.value('dateFormat', 'MMMM dd, yyyy');
trackTruck.value('designation', {
    driver: 'driver',
    helper: 'helper'
});
