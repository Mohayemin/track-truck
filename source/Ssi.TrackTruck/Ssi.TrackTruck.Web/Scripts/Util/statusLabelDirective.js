utilModule.directive('statusLabel', [
    function() {
        return {
            restrict: 'E',
            scope: {
                status: '='
            },
            templateUrl: '/Scripts/Util/statusLabel.html'
        };
    }
]);