app.controller('truckReportController', ['$scope', 'truckRepository', truckReportController]);

function truckReportController($scope, repository){
    $scope.trucks = [];

    repository.getCurrentStatus().then(function (response) {
        $scope.trucks = response.data;
    });
}