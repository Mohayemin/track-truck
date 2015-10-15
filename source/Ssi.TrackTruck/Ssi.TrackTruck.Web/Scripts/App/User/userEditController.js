userModule.controller('userEditController', [
    '$scope',
    '$routeParams',
    'userService',
    'userRoles',
    'clientService',
    '$location',
    'globalMessage',
    function ($scope,
        $routeParams,
        userService,
        userRoles,
        clientService,
        $location,
        globalMessage) {
        
        $scope.userRoles = userRoles;

        clientService.getAll().then(function (clients) {
            $scope.clients = clients;
        });

        userService.get($routeParams['id']).then(function (user) {
            $scope.request = JSON.parse(JSON.stringify(user));
        });

        $scope.showBranchSelect = function () {
            return $scope.request && $scope.request.Role == userRoles.branchCustodian;
        };

        $scope.User = {
            isUsernameReadonly: false,
            isEditing: true
        };

        $scope.setUsername = function () {
            if ($scope.User.isUsernameReadonly) {
                $scope.request.Username = ($scope.request.FirstName || '') +
                ($scope.request.LastName || '');
            }
        };

        $scope.save = function() {
            userService.edit($scope.request).then(function () {
                globalMessage.success('Successfully edited');
                $location.url('user/list');
            }).catch(function (message) {
                globalMessage.error(message);
            });
        }
    }
]);