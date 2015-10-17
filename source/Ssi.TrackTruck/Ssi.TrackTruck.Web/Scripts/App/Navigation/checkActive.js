navigationModule.directive('checkActive', [
    '$location',
    function ($location) {
        var directive = {
            link: function(scope, element) {
                var anchors = element.find('a');
                angular.forEach(anchors, function(anchor) {
                    console.log(anchor);
                });
            }
        };

        return directive;
    }
]);