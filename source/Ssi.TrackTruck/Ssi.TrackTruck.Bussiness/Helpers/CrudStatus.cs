namespace Ssi.TrackTruck.Bussiness.Helpers
{
    // Keep sync with crudStatus.js
    public enum CrudStatus
    {
        Unchanged = 1,
        Added = 2,
        Edited = 4,
        Deleted = 8,
        DeletedUnchanged = Deleted | Unchanged,
        DeletedAdded = Deleted | Added,
        DeletedEdited = Deleted | Edited
    }
}
