employeeModule.factory('employeeService', [
    'repository',
    'designation',
    function employeeService(repository, designation) {
        var _employees = [];
        var _loadPromise;

        var service = {
            getTruckEmployees: function () {
                return repository.get('Employee', 'GetByDesignations', {
                    Designations: [designation.driver, designation.helper]
                });
            },
            load: function () {
                return repository.get('Employee', 'All').then(function (employees) {
                    _employees.length = 0;
                    _employees.push.apply(_employees, employees);
                    return employees;
                });
            },
            getAll: function () {
                return _loadPromise.then(function () {
                    return _employees;
                });
            },
            add: function (request) {
                return repository.post('Employee', 'Add', request).then(function (employee) {
                    _employees.push(employee);
                    return employee;
                });
            },
            isDesignationEmpty: function(designation) {
                return designation === undefined || designation === '';
            }
        }

        _loadPromise = service.load();

        return service;
    }
]);