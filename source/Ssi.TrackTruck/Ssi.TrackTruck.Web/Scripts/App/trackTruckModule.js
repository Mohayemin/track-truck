var trackTruck = angular.module('trackTruck', ['ui.bootstrap']);

trackTruck.config([
    '$locationProvider',
    'datepickerPopupConfig',
    'datepickerConfig',
    'timepickerConfig',
    function ($locationProvider, datepickerPopupConfig, datepickerConfig, timepickerConfig) {
        $locationProvider.html5Mode(false);
        datepickerPopupConfig.datepickerPopup = 'MMMM dd, yyyy';
        datepickerConfig.showWeeks = false;
        datepickerPopupConfig.appendToBody = true;
        timepickerConfig.showSpinners = false;
    }
]);

trackTruck.value('_', window._);
