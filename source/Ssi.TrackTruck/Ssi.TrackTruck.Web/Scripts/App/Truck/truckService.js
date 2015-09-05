trackTruck.factory('truckService', [
    '$http',
    'url',
    truckService
]).factory('truckStatus', truckStatusFactory);

function truckService($http, url) {
    function calculateReportSummary(trucks) {
        var summary = {
            trucks: { Total: trucks.length },
            items: {}
        };

        ['Moving', 'Not In Use', 'Loading', 'Unloading'].forEach(function (status) {
            var trucksWithStatus = _.filter(trucks, { Status: status });
            summary.trucks[status] = trucksWithStatus.length;
            summary.items[status] = trucksWithStatus.reduce(function (memo, truck) {
                return memo + truck.ItemsCarrying;
            }, 0);
        });

        summary.items.Total = trucks.reduce(function (memo, truck) {
            return memo + truck.ItemsCarrying;
        }, 0);

        return summary;
    }

    function getCurrentStatus() {
        return $http.get(url('Truck', 'GetCurrentStatus')).then(function(respoonse) {
            return respoonse.data;
        });
    }

    return {
        calculateReportSummary: calculateReportSummary,
        getCurrentStatus: getCurrentStatus
    }
}

function truckStatusFactory() {
    function obj(cssClass) {
        return {
            cssClass: cssClass
        };
    }
    return {
        'Moving': obj('success'),
        'Not In Use': obj('danger'),
        'Loading':obj('info'),
        'Unloading': obj('warning')
    }
}