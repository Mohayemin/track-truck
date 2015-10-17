namespace Ssi.TrackTruck.Bussiness.Helpers
{
    // Keep sync with crudStatus.js
    public enum CrudStatus
    {
        Unchanged = 1,
        Added = 2,
        Edited = 4,
        DeletedUnchanged = 8,
        DeletedAdded = 16,
        DeletedEdited = 32
    }
}
