employeeModule.factory('employeeService', [
    'repository',
    'designation',
    function employeeService(repository, designation) {
        return {
            getTruckEmployees: function () {
                return repository.get('Employee', 'GetByDesignations', {
                    Designations: [designation.driver, designation.helper]
                });
            }
        }
    }
]);