trackTruck.factory('repository', [
    'url',
    '$http',
    repository
]);

function repository(url, $http) {
    function onSuccess(response) {
        return response.data;
    };

    return {
        get: function(controller, action, params) {
            return $http.get(url.server(controller, action, params)).then(onSuccess);
        },
        post: function(controller, action, data) {
            return $http.post(url.server(controller, action), data).then(onSuccess);
        }
    }
}