utilModule.directive('pageTitle', [
    'pageTitle',
    function (pageTitle) {
        var directive = {
            templateUrl: '/Scripts/Util/pageTitle.html',
            transclude: true,
            controller: [
                '$scope',
                function($scope) {
                    $scope.getTitle = function() {
                        return pageTitle.getTitle();
                    };
                }
            ]
        };

        return directive;
    }
]);