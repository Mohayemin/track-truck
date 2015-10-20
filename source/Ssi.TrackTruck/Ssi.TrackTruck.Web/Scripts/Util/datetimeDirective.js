utilModule.directive('datetime', [
    function datetimeDirective() {
        return {
            templateUrl: '/Templates/datetimeDirective.html',
            scope: {
                ngModel: '=',
                hideTime: '=',
                options: '='
            }
        };
    }
]);
