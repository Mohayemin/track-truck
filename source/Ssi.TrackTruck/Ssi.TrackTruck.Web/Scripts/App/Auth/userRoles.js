/**
* Must synch with enum Role in serverside
*/
authModule.factory('userRoles', [
    function () {
        var roleList = [
            { label: 'Admin', value: 1 },
            { label: 'Branch Custodian', value: 2 },
            { label: 'Encoder', value: 4 }
        ];

        var map = roleList.reduce(function (mapObject, role) {
            mapObject[role.value] = role.label;
            return mapObject;
        }, {});

        return {
            roleList: roleList,
            map: map,
            admin: 1,
            branchCustodian: 2,
            encoder: 4
        };
    }
]);