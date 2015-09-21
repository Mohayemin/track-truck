employeeModule.factory('employeeService', [
    'repository',
    'designation',
    function employeeService(repository, designation) {
        var _employees = [];

        return {
            getTruckEmployees: function () {
                return repository.get('Employee', 'GetByDesignations', {
                    Designations: [designation.driver, designation.helper]
                });
            },
            add: function (request) {
                return repository.post('Employee', 'Add', request).then(function (client) {
                    _employees.push(client);
                    return client;
                });
            }
        }
    }
]);