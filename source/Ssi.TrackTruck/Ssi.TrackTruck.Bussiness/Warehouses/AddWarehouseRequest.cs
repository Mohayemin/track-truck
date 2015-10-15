using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Warehouses
{
    public class AddWarehouseRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public Response Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return Response.ValidationError("Name cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(Address))
            {
                return Response.ValidationError("Address cannot be empty");
            }

            return Response.Success();
        }
    }
}