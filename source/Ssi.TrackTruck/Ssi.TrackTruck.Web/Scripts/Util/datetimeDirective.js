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
                '$filter',
                function ($scope
                    , $filter) {
                    var model = $scope.model;

                    if (model && model.year) {
                        $scope.datePickerDate = new Date(
                            model.year || 0
                            , model.month || 0
                            , model.day || 0
                            , model.hour || 0
                            , model.minute || 0
                            , 0);
                    }

                    $scope.$watch('datePickerDate', function (dpd) {
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

                    $scope.$watch('timePickerTime', function (tpt) {
                        if (tpt) {
                            model.hour = tpt.getHours();
                            model.minute = tpt.getMinutes();
                            $scope.timeDisplay = model.hour + ":" + model.minute;
                        } else {
                            model.hour = null;
                            model.minute = null;
                            $scope.timeDisplay = null;
                        }
                        $scope.timeDisplay = $filter('date')(tpt, 'hh:mm a');
                    });
                }
            ]
        };
    }
]);
