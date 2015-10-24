utilModule.provider('pageTitle', [
    function () {
        var _prefix = '', _suffix = '';

        function setPrefix(prefix) {
            _prefix = prefix || '';
            return this;
        }

        function setSuffix(suffix) {
            _suffix = suffix || '';
            return this;
        };

        this.setPrefix = setPrefix;
        this.setSuffix = setSuffix;

        this.$get = [
            '$document',
            function ($document) {
                var _title = null;
                var service = {
                    setPrefix: setPrefix,
                    setSuffix: setSuffix,
                    setTitle: function (title) {
                        _title = title;
                        $document[0].title = service.getFullTitle();
                    },
                    getTitle: function () {
                        return _title;
                    },
                    getFullTitle: function () {
                        return _prefix + _title + _suffix;
                    }
                };

                return service;
            }
        ];

    }
]);