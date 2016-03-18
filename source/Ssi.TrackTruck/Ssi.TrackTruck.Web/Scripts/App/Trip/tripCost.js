﻿tripModule.factory('TripCost', [
    function() {
        function TripCost(dbCost) {
            angular.extend(this, dbCost);
        }

        TripCost.prototype.getAdjustment = function() {
            return this.AdjustmentInPeso = this.ExpectedCostInPeso - this.ActualCostInPeso;
        };

        return TripCost;
    }
]);