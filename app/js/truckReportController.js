app.controller('truckReportController', ['$scope', 'truckRepository', truckReportController]);

function truckReportController($scope, repository) {
    $scope.Trucks = [];
    $scope.Summary = {};
    repository.getCurrentStatus().then(function (response) {
        $scope.Trucks = response.data;
        updateSummary();
    });

    function updateSummary() {
        var trucks = $scope.Trucks;
        var summary = {
            Trucks: {Total: trucks.length},
            Items: {}
        };

        console.log();
        ['Moving', 'Not In Use', 'Loading', 'Unloading'].forEach(function (status) {
            var trucksWithStatus = _.filter(trucks, {Status: status});
            summary.Trucks[status] = trucksWithStatus.length;
            summary.Items[status] = trucksWithStatus.reduce(function (memo, truck) {
                return memo + truck.ItemsCarrying;
            }, 0);
        });


        summary.Items.Total = trucks.reduce(function (memo, truck) {
            return memo + truck.ItemsCarrying; // return previous total plus current age
        }, 0);

        $scope.Summary = summary;
    }

    $scope.getStatusClass = function (truck) {
        if (truck.Status === 'Moving') {
            return 'success';
        }
        if (truck.Status === 'Not In Use') {
            return 'danger';
        }
        if (truck.Status === 'Loading') {
            return 'info';
        }
        if (truck.Status === 'Unloading') {
            return 'warning';
        }
    };
}