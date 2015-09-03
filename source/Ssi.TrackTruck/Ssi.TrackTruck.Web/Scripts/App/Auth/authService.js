trackTruck.factory('authService', [
    '$http',
    'url',
    authService
]);

function authService($http, url) {
    function signIn(model) {
        return $http.post(url('Auth', 'SignIn'), model).then(function(response) {
            return response.data;
        });
    }

    return {
        signIn: signIn
    };
}