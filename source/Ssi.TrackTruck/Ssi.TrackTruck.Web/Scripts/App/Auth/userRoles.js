/**
* Must synch with enum Role in serverside
*/
authModule.factory('userRoles', [
    function() {
        return {
            roleList: [
                { label: 'Admin', value: 1 },
                { label: 'Branch Custodian', value: 2 },
                { label: 'Encoder', value: 4 }
            ],
            admin: 1,
            branchCustodian: 2,
            Encoder: 5
        };
    }
]);