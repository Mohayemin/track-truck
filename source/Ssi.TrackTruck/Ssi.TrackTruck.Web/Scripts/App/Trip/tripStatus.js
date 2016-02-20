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
        'New': obj('New', 1001, 'danger', 'New'),
        'InProgress': obj('InProgress', 1002, 'warning', 'In progress'),
        'Delivered': obj('Done', 1005, 'info', 'Delivered'),
        'Archived': obj('Archived', 1006, 'success', 'Archived')
    };

    return factory;
}]);