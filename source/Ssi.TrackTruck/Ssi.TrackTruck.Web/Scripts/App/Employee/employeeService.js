employeeModule.factory('employeeService', [
    'repository',
    '$q',
    '_',
    function employeeService(
        repository,
        $q,
        _) {
        var _employees = [];
        var _loadPromise;

        var service = {
            getAllByDesignation: function(designation) {
                return service.getAll().then(function() {
                    return _.where(_employees, { Designation: designation });
                });
            },
            getAll: function (force) {
                if (!_loadPromise || force) {
                    _loadPromise = repository.get('Employee', 'All').then(function (employees) {
                        _employees.length = 0;
                        _employees.push.apply(_employees, employees);
                        return employees;
                    });
                }
                return _loadPromise;
            },
            add: function (request) {
                return repository.post('Employee', 'Add', request).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message || response.Status || 'Could not add employee');
                    }
                    var employee = response.Data;
                    _employees.push(employee);
                    return employee;
                });
            },
            isDesignationEmpty: function(designation) {
                return designation === undefined || designation === '';
            }
        }

        return service;
    }
]);