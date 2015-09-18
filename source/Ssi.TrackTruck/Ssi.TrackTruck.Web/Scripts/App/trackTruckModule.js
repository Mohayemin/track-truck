var trackTruck = angular.module('trackTruck', ['ui.bootstrap', 'tableSort', 'ngRoute']);

trackTruck.config([
    '$locationProvider',
    'datepickerPopupConfig',
    'datepickerConfig',
    'timepickerConfig',
    '$routeProvider',
    function ($locationProvider, datepickerPopupConfig, datepickerConfig, timepickerConfig, $routeProvider) {
        $locationProvider.html5Mode(false);
        datepickerPopupConfig.datepickerPopup = 'MMMM dd, yyyy';
        datepickerConfig.showWeeks = false;
        datepickerPopupConfig.appendToBody = true;
        timepickerConfig.showSpinners = false;

        function createClosedTag(tagName) {
            return '<' + tagName + '>' + '</' + tagName + '>';
        }

        $routeProvider
            .when('/hello', {
                template: '<div>hello</div>'
                , caseInsensitiveMatch: true
            }).when('/truck/add', {
                template: createClosedTag('add-truck')
                , caseInsensitiveMatch: true
            }).when('/truck/report', {
                template: createClosedTag('truck-status-report')
                , caseInsensitiveMatch: true
            });
    }
]);

trackTruck.value('_', window._);
trackTruck.value('dateFormat', 'MMMM dd, yyyy');
trackTruck.value('designation', {
    driver: 'driver',
    helper: 'helper'
});
