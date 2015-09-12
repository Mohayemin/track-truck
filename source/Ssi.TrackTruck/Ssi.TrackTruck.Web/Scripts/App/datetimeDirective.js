trackTruck.directive('datetime', [
    datetimeDirective
]);

function datetimeDirective() {
    return {
        templateUrl: '/Templates/datetimeDirective.html',
        scope: {
            model: '='
        },
        controller: [
            '$scope',
            function ($scope) {
                var model = $scope.model;

                $scope.$watch('datePickerDate', function (dpd) {
                    if (dpd) {
                        model.year = dpd.getFullYear();
                        model.month = dpd.getMonth();
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