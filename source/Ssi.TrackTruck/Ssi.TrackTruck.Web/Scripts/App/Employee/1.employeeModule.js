var employeeModule = angular.module('employee', [
    'utilModule'
]);

employeeModule.value('designation', {
    supervisor: 'supervisor',
    driver: 'driver',
    helper: 'helper'
});
