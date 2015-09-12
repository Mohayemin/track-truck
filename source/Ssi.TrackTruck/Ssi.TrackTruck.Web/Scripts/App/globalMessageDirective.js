trackTruck
    .factory('globalMessage', [
        globalMessageService
    ])
    .directive('globalMessage', [
        globalMessageDirective
    ]);

function globalMessageService() {
    function setMessage(type, message) {
        messageObject.message = message;
        messageObject.type = type;
    }

    var messageObject = {};

    var factory = {
        error: function (message) {
            setMessage('error', message);
        },
        success: function (message) {
            setMessage('success', message);
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
            '<alert ng-show="messageObject.message" close="close()" type="{{messageObject.type}}" dismiss-on-timeout="5000">{{messageObject.message}}</alert>' +
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