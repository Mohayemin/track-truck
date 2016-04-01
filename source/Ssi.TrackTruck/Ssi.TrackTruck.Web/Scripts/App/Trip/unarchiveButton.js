tripModule.directive('unarchiveButton', [
    'url',
    function (url) {
        return {
            scope: {
                trip: '=',
                cssClass: '=?'
            },
            templateUrl: url.template('Trip', 'unarchiveButton'),
            controller: [
                '$scope',
                'userRoles',
                'tripService',
                '$window',
                function ($scope, userRoles, tripService, $window) {
                    $scope.roles = userRoles;
                    $scope.unarchive = function() {
                        var confirm = $window.confirm('Are you sure you want to unarchive this trip?');
                        if (confirm) {
                            tripService.unarchive($scope.trip);
                        }
                    };
                }
            ]
        };
    }
]);