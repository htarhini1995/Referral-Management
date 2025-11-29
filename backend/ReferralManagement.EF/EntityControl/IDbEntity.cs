namespace FleetManagement.EF.Models
{
    public interface IDbEntity<T>
    {
        T Insert();

        T? GetById(long id);

        void Save();

        bool Delete();

        bool DeleteById(long id);

    }
}
