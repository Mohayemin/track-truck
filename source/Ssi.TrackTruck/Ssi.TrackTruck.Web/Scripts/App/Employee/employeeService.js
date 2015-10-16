employeeModule.factory('employeeService', [
    'repository',
    'buildIdMap',
    '$q',
    '_',
    function employeeService(
        repository,
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