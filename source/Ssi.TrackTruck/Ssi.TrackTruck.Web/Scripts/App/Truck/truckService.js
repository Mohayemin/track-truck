truckModule.factory('truckService', [
    'repository',
    '$q',
    'employeeService',
    function truckService(repository
        , $q
        , employeeService) {
        function calculateReportSummary(trucks) {
            var summary = {
                trucks: { total: trucks.length },
                items: {}
            };

            [0, 1001, 1002, 1003].forEach(function(status) {
                var trucksWithStatus = _.filter(trucks, { Status: status });
                summary.trucks[status] = trucksWithStatus.length;
                summary.items[status] = trucksWithStatus.reduce(function(memo, truck) {
                    return memo + truck.ItemsCarrying;
                }, 0);
            });

            summary.items.total = trucks.reduce(function(memo, truck) {
                return memo + truck.ItemsCarrying;
            }, 0);

            return summary;
        }

        function getCurrentStatus() {
            return repository.get('Truck', 'GetCurrentStatus');
        }

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

        function getAll() {
            return repository.get('Truck', 'All').then(function(trucks) {
                employeeService.getIndexedEmployees().then(function(employees) {
                    trucks.forEach(function(truck) {
                        truck.DriverName = (employees[truck.DriverId] || {}).FullName;
                        truck.HelperName = (employees[truck.HelperId] || {}).FullName;
                    });
                });
                return trucks;
            });
        }

        return {
            calculateReportSummary: calculateReportSummary,
            getCurrentStatus: getCurrentStatus,
            add: add,
            getAll: getAll
        }
    }
]);

// Map with Ssi.TrackTruck.Bussiness.DAL.Constants.TripStatus
truckModule.factory('truckStatus', [function truckStatusFactory() {
    function obj(id, cssClass, text) {
        return {
            id: id,
            cssClass: cssClass,
            text: text
        };
    }

    var factory = {
        0: obj(0, 'danger', 'Not In Use'),
        1001: obj(1001, 'success', 'Moving'),
        1002: obj(1002, 'info', 'Loading'),
        1003: obj(1003, 'warning', 'Unloading')
    };

    factory.notInUse = factory[0];
    factory.moving = factory[1001];
    factory.loading = factory[1002];
    factory.unloading = factory[1003];

    return factory;
}]);