var trackTruck = angular.module('trackTruck', ['ui.bootstrap']);

trackTruck.config([
    '$locationProvider',
    'datepickerPopupConfig',
    'datepickerConfig',
    function ($locationProvider, datepickerPopupConfig, datepickerConfig) {
        $locationProvider.html5Mode(false);
        datepickerPopupConfig.datepickerPopup = 'MMMM dd, yyyy';
        datepickerConfig.showWeeks = false;
    }
]);

trackTruck.value('_', window._);
