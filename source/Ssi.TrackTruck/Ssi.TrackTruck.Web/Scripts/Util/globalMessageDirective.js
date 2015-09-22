utilModule.factory('globalMessage', [
    '$timeout',
    function globalMessageService($timeout) {
        var lastTimeoutPromise = null;

        function setMessage(type, message, durationInSecond) {
            lastTimeoutPromise && $timeout.cancel(lastTimeoutPromise);

            messageObject.message = message;
            messageObject.type = type;

            if (isNaN(durationInSecond)) {
                durationInSecond = 5;
            } else if (durationInSecond == 0) {
                durationInSecond = 99999999; // Almost INFINITY
            }

            lastTimeoutPromise = $timeout(function () {
                factory.clear();
            }, durationInSecond * 1000);
        }

        var messageObject = {};

        var factory = {
            error: function (message, durationInSecond) {
                setMessage('danger', message, durationInSecond);
            },
            success: function (message, durationInSecond) {
                setMessage('success', message, durationInSecond);
            },
            info: function (message, durationInSecond) {
                setMessage('info', message, durationInSecond);
            },
            clear: function () {
                messageObject.message = null;
                messageObject.type = null;
                lastTimeoutPromise && $timeout.cancel(lastTimeoutPromise);
            },
            getMessageObject: function () {
                return messageObject;
            }
        };
        return factory;
    }
]);

utilModule.directive('globalMessage', [
    function globalMessageDirective() {
        return {
            template: '<div class="global-message"> ' +
                '<alert ng-show="messageObject.message" close="close()" type="{{messageObject.type}}">{{messageObject.message}}</alert>' +
                '</div>',
            scope: {},
            controller: [
                '$scope',
                'globalMessage',
                function ($scope, globalMessage) {
                    $scope.messageObject = globalMessage.getMessageObject();

                    $scope.close = function () {
                        globalMessage.clear();
                    };
                }
            ]
        }
    }
]);
