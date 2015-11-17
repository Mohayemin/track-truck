// Map with Ssi.TrackTruck.Bussiness.DAL.Constants.TripStatus
tripModule.factory('tripStatus', [function truckStatusFactory() {
    function obj(id, cssClass, text) {
        return {
            id: id,
            cssClass: cssClass,
            text: text
        };
    }

    var factory = {
        1001: obj(1001, 'success', 'On The Way'),
        1004: obj(1002, 'info', 'Not Started'),
        1003: obj(1003, 'warning', 'Unloading')
    };

    factory.notInUse = factory[0];
    factory.moving = factory[1001];
    factory.loading = factory[1002];
    factory.unloading = factory[1003];

    return factory;
}]);