app.factory('truckRepository', ['$http', truckRepository]);

function truckRepository($http){
    return {
        getCurrentStatus: function (){
            return $http.get('data/truck-status.json');
        }
    };
}