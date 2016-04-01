utilModule.directive('statusLabel', [
    function() {
        return {
            restrict: 'E',
            scope: {
                status: '='
            },
            templateUrl: 'Util/statusLabel.html'
        };
    }
]);