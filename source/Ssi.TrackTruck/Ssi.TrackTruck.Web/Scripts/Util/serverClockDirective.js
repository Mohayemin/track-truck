utilModule.directive('serverClock', [
    function () {
        return {
            templateUrl: '/Scripts/Util/serverClock.html',
            replace: true,
            scope: {
                url: '=',
                serverSyncIntervalMs: '=',
                localSyncIntervalMs: '=',
                format: '='
            },
            controller: [
                '$scope',
                '$interval',
                '$http',
                function ($scope,
                    $interval,
                    $http) {
                    var localSync;

                    function getTime() {
                        $http.get($scope.url).then(function(response) {
                            $scope.time = Date.parse(response.data);

                            $interval.cancel(localSync);
                            localSync = $interval(function() {
                                $scope.time += $scope.localSyncIntervalMs;
                            }, $scope.localSyncIntervalMs);
                        });
                    }

                    getTime();

                    $interval(getTime, $scope.serverSyncIntervalMs);
                }
            ]
        };
    }
]);