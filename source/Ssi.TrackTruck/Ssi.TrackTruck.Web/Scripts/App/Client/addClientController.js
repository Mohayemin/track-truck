clientModule.controller('addClientController', [
    '$scope',
    '$location',
    'globalMessage',
    'clientService',
    function($scope, $location, globalMessage, clientService) {
        $scope.request = {
            Branches: []
        };

        $scope.add = function() {
            globalMessage.info('adding client', 0);
            clientService.add($scope.request).then(function() {
                $location.url('client/list');
                globalMessage.success('client added');
            }).catch(function(message) {
                globalMessage.error(message);
            });
        };

        $scope.addBranch = function() {
            $scope.request.Branches.push({});
        };

        $scope.deleteBranch = function(index) {
            $scope.request.Branches.splice(index, 1);
        };
    }
]);
