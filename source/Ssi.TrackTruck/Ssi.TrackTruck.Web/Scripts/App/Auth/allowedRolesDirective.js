// TODO: May be cause flickering on page load because initially the element is shown.
authModule.directive('allowedRoles', [
    'signedInUser',
    function (signedInUser) {
        var directive = {
            restrict: 'A',
            scope: {
                allowedRoles: '='
            },
            link: function (scope, element) {
                var roles = scope.allowedRoles;
                if (!angular.isDefined(roles)) {
                    roles = [];
                }

                if (!Array.isArray(roles)) {
                    roles = [roles];
                }

                signedInUser.load().then(function () {
                    if (roles.indexOf(signedInUser.Role) < 0) {
                        element.remove();
                    }
                });
            }
        };

        return directive;
    }
]);