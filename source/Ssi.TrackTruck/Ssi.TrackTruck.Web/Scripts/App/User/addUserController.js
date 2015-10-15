userModule.controller('addUserController', [
    '$scope',
    'clientService',
    'userService',
    'userRoles',
    '$location',
    'globalMessage',
    function($scope, clientService, userService, userRoles, $location, globalMessage) {

        $scope.userRoles = userRoles;

        clientService.getAll().then(function(clients) {
            $scope.clients = clients;
            console.log(clients);
        });

        $scope.showBranchSelect = function() {
            return $scope.request.Role == userRoles.branchCustodian;
        };

        $scope.request = {
            InitialPassword: userService.generateInitialPassword()
        };

        $scope.User = {
            isUsernameReadonly: true,
            isEditing: false
        };

        $scope.setUsername = function() {
            if ($scope.User.isUsernameReadonly) {
                $scope.request.Username = ($scope.request.FirstName || '') +
                ($scope.request.LastName || '');
            }
        };

        $scope.add = function() {
            globalMessage.info('adding user', 0);
            userService.add($scope.request).then(function() {
                $location.url('user/list');
                globalMessage.success('user added');
            }).catch(function(message) {
                globalMessage.error(message);
            });
        };
    }
]);