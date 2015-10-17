// Keep sync with CrudStatus.cs
utilModule.factory('CrudStatus', [
    function () {
        var service = {
            unchanged: 'Unchanged',
            added: 'Added',
            edited: 'Edited',
            deleted: 'Deleted',

            getStatusOnEdit: function (originalStatus) {
                if (originalStatus == service.unchanged) {
                    return service.edited;
                }
                if (originalStatus == service.added) {
                    return service.added;
                }
                if (originalStatus == service.edited) {
                    return service.edited;
                }
                if (originalStatus == service.deleted) {
                    return service.deleted;
                }
                throw 'invalid original status: ' + originalStatus;
            },
            getStatusOnDelete: function(originalStatus) {
                if (originalStatus == service.unchanged) {
                    return service.deleted;
                }
                if (originalStatus == service.added) {
                    return service.unchanged;
                }
                if (originalStatus == service.edited) {
                    return service.deleted;
                }
                if (originalStatus == service.deleted) {
                    return service.deleted;
                }
                throw 'invalid original status: ' + originalStatus;
            }
        };

        return service;
    }
]);