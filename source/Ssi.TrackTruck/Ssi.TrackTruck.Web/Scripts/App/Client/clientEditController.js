clientModule.controller('clientEditController', [
    '$scope',
    '$routeParams',
    'clientService',
    'userService',
    'crudStatus',
    'globalMessage',
    'userRoles',
    '$location',
    function ($scope,
        $routeParams,
        clientService,
        userService,
        crudStatus,
        globalMessage,
        userRoles,
        $location) {

        clientService.get($routeParams['id']).then(function (client) {
            $scope.request = JSON.parse(JSON.stringify(client));
            $scope.request.Branches.forEach(function (branch) {
                setBranchStatus(branch);
            });
        });

        $scope.addBranch = function () {
            var branch = {
                ModificationStatus: crudStatus.added
            };
            setBranchStatus(branch);
            $scope.request.Branches.push(branch);
        };

        var statusClass = {};
        statusClass[crudStatus.unchanged] = 'label-success';
        statusClass[crudStatus.edited] = 'label-warning';
        statusClass[crudStatus.added] = 'label-info';

        function setBranchStatus(branch) {
            branch.ModificationStatus = branch.ModificationStatus || crudStatus.unchanged;
            branch.isDeleted = crudStatus.allDeleted.indexOf(branch.ModificationStatus) >= 0;
            branch.cssClass = statusClass[branch.ModificationStatus];
        }

        $scope.deleteBranch = function (branch) {
            branch.ModificationStatus = crudStatus.getStatusOnDelete(branch.ModificationStatus);
            setBranchStatus(branch);
        };

        $scope.undeleteBranch = function (branch) {
            branch.ModificationStatus = crudStatus.getStatusOnUndelete(branch.ModificationStatus);
            setBranchStatus(branch);
        };

        $scope.editBranch = function (branch) {
            branch.ModificationStatus = crudStatus.getStatusOnEdit(branch.ModificationStatus);
            setBranchStatus(branch);
        };

        $scope.save = function () {
            globalMessage.info('editing client', 0);
            clientService.edit($scope.request).then(function () {
                $location.url('client/list');
                globalMessage.success('client edited');
            }).catch(function (message) {
                globalMessage.error(message);
            });
        };
    }
]);