app.controller('truckReportController', ['$scope', 'truckRepository', truckReportController]);

function truckReportController($scope, repository){
    $scope.trucks = [];

    repository.getCurrentStatus().then(function (response) {
        $scope.trucks = response.data;

        
    });

    $scope.getStatusClass = function (truck) {
        if(truck.Status === 'Moving'){
            return 'success';
        }
        if(truck.Status === 'Not In Use'){
            return 'danger';
        }
        if(truck.Status === 'Loading'){
            return 'info';
        }
        if(truck.Status === 'Unloading'){
            return 'warning';
        }
    };
}