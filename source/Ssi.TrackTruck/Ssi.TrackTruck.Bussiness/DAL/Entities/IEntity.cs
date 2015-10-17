using System;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public interface IEntity : IDeletable
    {
        string Id { get; set; }
        DateTime CreationTime { get; set; }
        string CreatorId { get; set; }
    }
}
