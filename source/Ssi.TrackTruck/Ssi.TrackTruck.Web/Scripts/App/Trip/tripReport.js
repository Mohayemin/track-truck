tripModule.directive('tripReport', [
    'url',
    function (url) {
        return {
            templateUrl: url.template('Trip', 'tripReport'),
            scope: {},
            controller: [
                '$scope',
                'tripService',
                'globalMessage',
                function orderTripController(
                    $scope,
                    tripService,
                    globalMessage) {

                    tripService.getReport().then(function(report) {
                        $scope.report = report;
                    });
                }
            ]
        }
    }
]);