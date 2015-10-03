utilModule.directive('datetime', [
    function datetimeDirective() {
        return {
            templateUrl: '/Templates/datetimeDirective.html',
            scope: {
                model: '=',
                hideTime: '=',
                options: '='
            },
            controller: [
                '$scope',
                function($scope) {
                    var model = $scope.model;

                    $scope.$watch('datePickerDate', function(dpd) {
                        if (dpd) {
                            model.year = dpd.getFullYear();
                            model.month = dpd.getMonth() + 1;
                            model.day = dpd.getDate();
                        } else {
                            model.year = null;
                            model.month = null;
                            model.day = null;
                        }
                    });
                }
            ]
        };
    }
]);
