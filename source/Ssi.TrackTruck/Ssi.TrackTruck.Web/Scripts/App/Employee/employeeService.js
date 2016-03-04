employeeModule.factory('employeeService', [
    'repository',
    'collection',
    'buildIdMap',
    '$q',
    '_',
    function employeeService(
        repository,
        collection,
        buildIdMap,
        $q,
        _) {
        var _employees = [];
        var _employeeById = {};
        var _loadPromise = null;

        function getAll(force) {
            if (!_loadPromise || force) {
                _loadPromise = repository.get('Employee', 'All').then(function (employees) {
                    _employees.length = 0;
                    _employees.push.apply(_employees, employees);
                    _employeeById = buildIdMap(_employees);

                    return _employees;
                });
            }
            return _loadPromise;
        }

        var service = {
            getAllByDesignation: function(designation) {
                return service.getAll().then(function() {
                    return _.where(_employees, { Designation: designation });
                });
            },
            getAll: getAll,
            get: function (id) {
                return getAll().then(function () {
                    return _.find(_employees, { Id: id });
                });
            },
            getSalaryReport: function (filter) {
                return repository.post('Employee', 'SalaryReport', filter).then(function (response) {
                    var employees = response.EmployeeSalaries;
                    var total = {
                        Allowance: collection.sum(employees, 'TotalAllowance'),
                        Salary: collection.sum(employees, 'TotalSalary'),
                        Adjustment: collection.sum(employees, 'TotalAdjustment'),
                        Payable: collection.sum(employees, 'TotalPayable'),
                    }
                    return {
                        employees: employees,
                        total: total
                    };
                });
            },
            'delete': function (id) {
                return repository.post('Employee', 'Delete', { id: id }).then(function (response) {
                    if (!response.IsError) {
                        var index = _.findIndex(_employees, { Id: id });
                        if (index >= 0) {
                            _employees.splice(index, 1);
                            delete _employeeById[id];
                        }
                        return response;
                    }

                    return $q.reject(response.Message || response.status || 'could not delete client');
                });
            },
            add: function (request) {
                return repository.post('Employee', 'Add', request).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message || response.Status || 'Could not add employee');
                    }
                    var employee = response.Data;
                    _employees.push(employee);
                    _employeeById[employee.Id] = employee;

                    return employee;
                });
            },
            edit: function(request) {
                return repository.post('Employee', 'Save', request).then(function(response) {
                    if (response.IsError) {
                        return $q.reject(response.Message || response.Status || 'Could not edit employee');
                    }

                    var employee = _employeeById[request.Id];
                    angular.extend(employee, response.Data);
                    return employee;
                });
            },
            isDesignationEmpty: function(designation) {
                return designation === undefined || designation === '';
            },
            getIndexedEmployees: function () {
                return service.getAll().then(function () {
                    return _employeeById;
                });
            }
        }

        return service;
    }
]);