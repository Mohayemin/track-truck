tripModule.directive('tripLink', [
    'url',
    function (url) {
        return {
            scope: {
                trip: '=',
                showStatus: '=?'
            },
            templateUrl: url.template('Trip', 'tripLink'),
            controller: [
                '$scope',
                function ($scope) {
                    if (!angular.isDefined($scope.showStatus)) {
                        $scope.showStatus = true;
                    }
                    $scope.url = url;
                }
            ]
        };
    }
])