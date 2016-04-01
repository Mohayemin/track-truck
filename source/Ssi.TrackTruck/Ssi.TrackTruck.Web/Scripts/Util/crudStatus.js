// Keep sync with CrudStatus.cs
// Algorithm: http://mohayemin.bitbucket.org/modification-state-transitions/
utilModule.factory('crudStatus', [
    function () {
        var unchanged = 'Unchanged';
        var added = 'Added';
        var edited = 'Edited';
        var deletedUnchanged = 'DeletedUnchanged';
        var deletedAdded = 'DeletedAdded';
        var deletedEdited = 'DeletedEdited';

        function invalidTransitionMessage(action, status) {
            return 'performing ' + action + ' in ' + status + ' status is invalid';
        }

        var service = {
            unchanged: unchanged,
            added: added,
            edited: edited,
            deletedUnchanged: deletedUnchanged,
            deletedAdded: deletedAdded,
            deletedEdited: deletedEdited,
            allDeleted: [deletedUnchanged, deletedAdded, deletedEdited],

            getStatusOnEdit: function (currentStatus) {
                switch (currentStatus) {
                    case unchanged:
                        return edited;
                    case added:
                        return added;
                    case edited:
                        return edited;
                    case deletedUnchanged:
                        return deletedEdited;
                    case deletedAdded:
                        return deletedAdded;
                    case deletedEdited:
                        return deletedEdited;
                    default:
                        throw invalidTransitionMessage('edit', currentStatus);
                }
            },
            getStatusOnDelete: function (currentStatus) {
                switch (currentStatus) {
                    case unchanged:
                        return deletedUnchanged;
                    case added:
                        return deletedAdded;
                    case edited:
                        return deletedEdited;
                    case deletedUnchanged:
                    case deletedAdded:
                    case deletedEdited:
                    default:
                        throw invalidTransitionMessage('delete', currentStatus);
                }
            },
            getStatusOnUndelete: function (currentStatus) {
                switch (currentStatus) {
                    case deletedUnchanged:
                        return unchanged;
                    case deletedAdded:
                        return added;
                    case deletedEdited:
                        return edited;
                    case unchanged:
                    case added:
                    case edited:
                    default:
                        throw invalidTransitionMessage('undelete', currentStatus);
                }
            }
        };

        return service;
    }
]);