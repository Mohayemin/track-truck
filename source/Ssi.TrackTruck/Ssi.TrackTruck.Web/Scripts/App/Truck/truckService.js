truckModule.factory('truckService', [
    'repository',
    'buildIdMap',
    '$q',
    'employeeService',
    function truckService(repository
        , buildIdMap
        , $q
        , employeeService) {

        var _trucks = [];
        var _trucksById = [];
        var _loadPromise = null;

        function add(request) {
            var formattedRequest = {
                RegistrationNumber: request.RegistrationNumber,
                DriverId: request.driver.Id,
                HelperId: request.helper.Id
            };
            return repository.post('Truck', 'Add', formattedRequest).then(function(response) {
                if (response.IsError) {
                    return $q.reject(response.Message);
                }
                return response;
            });
        }

        function getAll(forse) {
            if (!_loadPromise || forse) {
                _loadPromise = repository.get('Truck', 'All').then(function (trucks) {
                    employeeService.getIndexedEmployees().then(function (employees) {
                        trucks.forEach(function (truck) {
                            truck.DriverName = (employees[truck.DriverId] || {}).FullName;
                            truck.HelperName = (employees[truck.HelperId] || {}).FullName;
                        });
                    });

                    _trucks.length = 0;
                    _trucks.push.apply(_trucks, trucks);
                    _trucksById = buildIdMap(_trucks);

                    return _trucks;
                });
            }
            return _loadPromise;
        }

        var service = {
            add: add,
            getAll: getAll,
            get: function(id) {
                return getAll().then(function () {
                    return _.find(_trucks, { Id: id });
                });
            },
            getIndexedTrucks: function() {
                return service.getAll().then(function() {
                    return _trucksById;
                });
            },
            'delete': function (id) {
                return repository.post('Truck', 'Delete', { id: id }).then(function (response) {
                    if (!response.IsError) {
                        var index = _.findIndex(_trucks, { Id: id });
                        if (index >= 0) {
                            _trucks.splice(index, 1);
                            delete _trucksById[id];
                        }
                        return response;
                    }

                    return $q.reject(response.Message || response.status || 'could not delete client');
                });
            },
            edit: function(request) {
                var formattedRequest = {
                    Id: request.Id,
                    RegistrationNumber: request.RegistrationNumber,
                    DriverId: request.driver.Id,
                    HelperId: request.helper.Id
                };
                return repository.post('Truck', 'Save', formattedRequest).then(function(response) {
                    if (response.IsError) {
                        return $q.reject(response.Message || response.Status || 'Could not edit truck');
                    }

                    return employeeService.get(response.Data.DriverId).then(function(driver) {
                        response.Data.DriverName = driver.FullName;
                        employeeService.get(response.Data.HelperId).then(function(helper) {
                            response.Data.HelperName = helper.FullName;
                            angular.extend(_trucksById[request.Id], response.Data);
                        });
                        return _trucksById[request.Id];
                    });
                });
            }
        };

        return service;
    }
]);
