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

                $scope.dateChanged = function () {
                    var dpd = $scope.datePickerDate;
                    if (dpd) {
                        model.year = dpd.getYear();
                        model.month = dpd.getYear();
                        model.day = dpd.getDate();
                    } else {
                        model.year = null;
                        model.month = null;
                        model.day = null;
                    }
                };
            }
        ]
    };
}