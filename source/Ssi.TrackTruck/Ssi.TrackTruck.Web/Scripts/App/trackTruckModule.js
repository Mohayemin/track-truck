var trackTruck = angular.module('trackTruck', ['ui.bootstrap']);

trackTruck.config([
    '$locationProvider',
    function($locationProvider) {
        $locationProvider.html5Mode(false);
    }
]);

trackTruck.value('_', window._);
trackTruck.value('dateFormat', 'MMMM dd, yyyy');