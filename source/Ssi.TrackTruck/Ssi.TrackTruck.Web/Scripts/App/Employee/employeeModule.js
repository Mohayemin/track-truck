var employeeModule = angular.module('employee', [
    'utilModule'
]);

employeeModule.value('designation', {
    driver: 'driver',
    helper: 'helper'
});
