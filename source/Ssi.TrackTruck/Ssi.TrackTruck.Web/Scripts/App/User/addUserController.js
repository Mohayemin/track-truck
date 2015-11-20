userModule.controller('addUserController', [
    '$scope',
    'userService',
    'userRoles',
    '$location',
    'globalMessage',
    function($scope, userService, userRoles, $location, globalMessage) {

        $scope.userRoles = userRoles;
        
        $scope.generatePassword = function() {
            $scope.request.InitialPassword = userService.generateRandomPassword();
        };

        $scope.request = {};
        $scope.generatePassword();

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