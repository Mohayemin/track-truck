utilModule.factory('globalMessage', [
    '$timeout',
    function globalMessageService($timeout) {
        var lastTimeoutPromise = null;

        function setMessage(type, message, durationInSecond) {
            lastTimeoutPromise && $timeout.cancel(lastTimeoutPromise);

            messageObject.message = message;
            messageObject.type = type;

            durationInSecond = durationInSecond || 600;
            lastTimeoutPromise = $timeout(function() {
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
            clear: function() {
                messageObject.message = null;
                messageObject.type = null;
                lastTimeoutPromise && $timeout.cancel(lastTimeoutPromise);
            },
            getMessageObject: function() {
                return messageObject;
            }
        };
        return factory;
    }
]);

utilModule.directive('globalMessage', [
    function globalMessageDirective() {
        return {
            template: '<div ng-style="style"> ' +
                '<alert ng-show="messageObject.message" close="close()" type="{{messageObject.type}}">{{messageObject.message}}</alert>' +
                '</div>',
            scope: {},
            controller: [
                '$scope',
                'globalMessage',
                function($scope, globalMessage) {
                    $scope.messageObject = globalMessage.getMessageObject();

                    $scope.close = function() {
                        globalMessage.clear();
                    };

                    $scope.style = {
                        position: 'fixed',
                        top: '40px',
                        left: 0,
                        right: 0,
                        'text-align': 'center'
                    };
                }
            ]
        }
    }
]);
