trackTruck
    .factory('globalMessage', [
        '$timeout',
        globalMessageService
    ])
    .directive('globalMessage', [
        globalMessageDirective
    ]);

function globalMessageService($timeout) {
    var lastTimeoutPromise = null;
    function setMessage(type, message) {
        lastTimeoutPromise && $timeout.cancel(lastTimeoutPromise);

        messageObject.message = message;
        messageObject.type = type;

        lastTimeoutPromise = $timeout(function() {
            factory.clear();
        }, 2000);
    }

    var messageObject = {};

    var factory = {
        error: function (message) {
            setMessage('danger', message);
        },
        success: function (message) {
            setMessage('success', message);
        },
        info: function(message) {
            setMessage('info', message);
        },
        clear: function () {
            setMessage(null, null);
        },
        getMessageObject: function () {
            return messageObject;
        }
    };
    return factory;
}

function globalMessageDirective() {
    return {
        template: '<div ng-style="style"> ' +
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