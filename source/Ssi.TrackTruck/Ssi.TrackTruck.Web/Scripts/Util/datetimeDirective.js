utilModule.directive('datetime', [
    function datetimeDirective() {
        return {
            templateUrl: 'Util/datetimeDirective.html',
            scope: {
                ngModel: '=',
                hideTime: '=',
                options: '='
            }
        };
    }
]);
