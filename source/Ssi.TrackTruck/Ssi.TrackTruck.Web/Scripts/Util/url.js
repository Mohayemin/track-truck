utilModule.provider('url', [
    function urlProvider() {
        function route() {
            var url = '#' + path.apply(this, arguments);
            return url;
        }

        function template(module, feature) {
            var root = '/Scripts/App/';
            return root + module + '/' + feature + '.html';
        }

        function path() {
            var paths = Array.prototype.slice.call(arguments);
            var url = paths.join('/');
            if (url[0] !== '/') {
                url = '/' + url;
            }
            return url;
        }

        function server(controller, action, params) {
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

        this.route = route;
        this.template = template;
        this.path = path;
        this.server = server;

        this.$get = [
            function() {
                return {
                    route: route,
                    template: template,
                    server: server
                };
            }
        ];
    }
]);
