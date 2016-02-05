// Map with Ssi.TrackTruck.Bussiness.DAL.Constants.TripStatus
tripModule.factory('tripStatus', [function tripStatusFactory() {
    function obj(id, cssClass, text) {
        return {
            id: id,
            cssClass: cssClass,
            text: text
        };
    }

    var factory = {
        'New': obj(1001, 'info', 'New'),
        'InProgress': obj(1002, 'warning', 'In progress'),
        'DoneWithPartialDelivery': obj(1003, 'danger', 'Part delivered'),
        'DoneWithFullDelivery': obj(1004, 'success', 'Full delivered')
    };

    return factory;
}]);