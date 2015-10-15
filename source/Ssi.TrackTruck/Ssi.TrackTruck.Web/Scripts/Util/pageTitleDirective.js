utilModule.directive('pageTitle', [
    function() {
        var directive = {
            templateUrl: '/Scripts/Util/pageTitle.html',
            transclude: true
        };

        return directive;
    }
]);