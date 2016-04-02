utilModule.directive('promiseLoader', [
    function () {
        return {
            replace: true,
            scope: {
                promise: '=',
                minShowTimeSeconds: '=?'
            },
            templateUrl: 'Util/promiseLoader.html',
            controller: [
                '$scope',
                '$timeout',
                function ($scope, $timeout) {
                    var lastPromise;
                    $scope.minTimeEllapsed = false;
                    $scope.$watch('promise', function (promise) {
                        if (promise && lastPromise !== promise) {
                            lastPromise = promise;
                            $scope.minTimeEllapsed = false;
                            $timeout(function() {
                                $scope.minTimeEllapsed = true;
                            }, ($scope.minShowTimeSeconds || 1) * 1000);
                        }
                    });
                }
            ]
        };
    }
]);