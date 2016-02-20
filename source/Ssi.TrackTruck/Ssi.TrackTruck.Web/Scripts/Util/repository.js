utilModule.factory('repository', [
    'url',
    '$http',
    '$window',
    '$q',
    function repository(url, $http, $window, $q) {
        function onSuccess(response) {
            return response.data;
        };

        function onError(response) {
            if (response.status == 401) {
                $window.location.reload();
            }
            return $q.reject(response);
        }

        return {
            get: function(controller, action, params) {
                return $http.get(url.server(controller, action, params)).then(onSuccess, onError);
            },
            post: function(controller, action, data) {
                return $http.post(url.server(controller, action), data).then(onSuccess, onError);
            }
        }
    }
]);
