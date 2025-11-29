using Microsoft.EntityFrameworkCore;

namespace FleetManagement.EF.Models
{
    public partial class User : IDbEntity<User>
    {
        #region Public Methods

        public User Insert()
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            db.Users.Add(this);
            db.SaveChanges();
            return this;
        }

        public User? GetById(long id)
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            return db.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
  
        public void Save()
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            if (Id == 0) db.Users.Add(this);
            else db.Users.Update(this);
            db.SaveChanges();
        }

        public bool Delete()
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            db.Users.Remove(this);
            return db.SaveChanges() > 0;
        }

        public bool DeleteById(long id)
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            var e = new User { Id = id };
            db.Entry(e).State = EntityState.Deleted;
            try { return db.SaveChanges() > 0; }
            catch (DbUpdateConcurrencyException) { return false; }
        }

        #endregion
    }
}
