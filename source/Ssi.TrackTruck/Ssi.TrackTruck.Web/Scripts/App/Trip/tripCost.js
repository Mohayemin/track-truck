tripModule.factory('TripCost', [
    'costType',
    function (costType) {
        function TripCost(cost) {
            this.Id = cost.Id;
            this.ExpectedCostInPeso = cost.ExpectedCostInPeso || 0;
            this.ActualCostInPeso = cost.ActualCostInPeso || this.ExpectedCostInPeso;
            this.Comment = cost.Comment;
            this.CostType = cost.CostType || costType.Discrepancy;
            this.IsDiscrepancy = this.CostType === costType.Discrepancy;
            this.AssignedEmployeeId = cost.AssignedEmployeeId;
        }

        TripCost.prototype.getAdjustment = function() {
            return this.AdjustmentInPeso = this.ExpectedCostInPeso - this.ActualCostInPeso;
        };

        return TripCost;
    }
]);