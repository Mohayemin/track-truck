trackTruck.factory('url', [
    urlFactory
]);

function urlFactory() {
    return {
        resolveTemplate: function(module, feature) {
            return '/Scripts/App/' + module + '/' + feature + '.html';
        },
        resolve: function (controller, action, params) {
            if (!controller) {
                return '/';
            }

            var urlString = '/' + controller + '/' + action;
            if (params) {
                urlString = urlString + '?';
                for (var key in params) {
                    urlString = urlString + key + '=' + params[key] + '&';
                }
            }

            return urlString;
        }
    };
}