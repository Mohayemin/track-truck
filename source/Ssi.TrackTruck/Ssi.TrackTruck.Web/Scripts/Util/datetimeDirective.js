utilModule.directive('datetime', [
    function datetimeDirective() {
        return {
            templateUrl: 'Util/datetimeDirective.html',
            scope: {
                ngModel: '=',
                hideTime: '=',
                options: '='
            },
            controller: [
                '$scope', '$rootScope',
                function ($scope, $rootScope) {
                    $scope.state = {
                        isOpen : false
                    };
                    $scope.dateFormat = $rootScope.dateFormat;
                    $scope.dateTimeFormat = $rootScope.dateTimeFormat;

                    console.log($scope.hideTime ? $scope.dateFormat : $scope.dateTimeFormat);
                }
            ]
        };
    }
]);
