utilModule.factory('url', [
    function url() {
        return {
            template: function(module, feature) {
                var root = '/Scripts/App/';
                return root + module + '/' + feature + '.html';
            },
            route: function() {
                var paths = Array.prototype.slice.call(arguments);
                return '#/' + paths.join('/');
            },
            server: function(controller, action, params) {
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
]);
