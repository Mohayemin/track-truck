authModule.directive('changePassword', [
    'url',
    function(url) {
        return {
            templateUrl: url.template('Auth', 'changePassword'),
            scope: {},
            controller: [
                '$scope',
                function ($scope) {
                    $scope.request = {};

                    $scope.changePassword = function() {

                    };
                }
            ]
        };
    }
]);