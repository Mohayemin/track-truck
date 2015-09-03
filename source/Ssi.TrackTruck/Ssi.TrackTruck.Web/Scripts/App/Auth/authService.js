trackTruck.factory('authService', [
    '$http',
    'url',
    authService
]);

function authService($http, url) {
    function signIn(request) {
        return $http.post(url('Auth', 'SignIn'), request).then(function(response) {
            return response.data;
        });
    }

    return {
        signIn: signIn
    };
}