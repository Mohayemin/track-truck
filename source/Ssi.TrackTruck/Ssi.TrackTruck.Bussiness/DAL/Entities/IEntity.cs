using System;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public interface IEntity
    {
        string Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime CreationTime { get; set; }
    }
}
