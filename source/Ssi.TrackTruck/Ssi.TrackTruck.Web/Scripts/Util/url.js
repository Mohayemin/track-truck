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
            var url = '/' + paths.join('/');
            return url;
        }

        this.route = route;
        this.template = template;
        this.path = path;

        this.$get = [
            function() {
                return {
                    route: route,
                    template: template,
                    server: function (controller, action, params) {
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
        ];
    }
]);
