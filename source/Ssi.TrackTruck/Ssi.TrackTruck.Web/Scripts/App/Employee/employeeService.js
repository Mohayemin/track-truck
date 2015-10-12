employeeModule.factory('employeeService', [
    'repository',
    '$q',
    '_',
    function employeeService(
        repository,
        $q,
        _) {
        var _employees = [];
        var _employeeById = {};
        var _loadPromise = null;

        function buildIdMap() {
            _employeeById = {};
            _employees.forEach(function (employee) {
                _employeeById[employee.Id] = employee;
            });
        }

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

                        buildIdMap();

                        return _employees;
                    });
                }
                return _loadPromise;
            },
            get: function (id) {
                return _loadPromise.then(function () {
                    return _.find(_employees, { Id: id });
                });
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