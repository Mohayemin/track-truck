// Map with Ssi.TrackTruck.Bussiness.DAL.Constants.TripStatus
tripModule.factory('tripStatus', [function tripStatusFactory() {
    function obj(id, value, cssClass, text) {
        return {
            id: id,
            value: value,
            cssClass: cssClass,
            text: text
        };
    }

    var factory = {
        'New': obj('New', 1001, 'info', 'New'),
        'InProgress': obj('InProgress', 1002, 'warning', 'In progress'),
        'DoneWithPartialDelivery': obj('DoneWithPartialDelivery', 1003, 'danger', 'Part delivered'),
        'DoneWithFullDelivery': obj('DoneWithFullDelivery', 1004, 'success', 'Full delivered')
    };

    return factory;
}]);