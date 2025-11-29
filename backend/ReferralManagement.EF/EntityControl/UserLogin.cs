using Microsoft.EntityFrameworkCore;

namespace FleetManagement.EF.Models
{
    public partial class UserLogin : IDbEntity<UserLogin>
    {
        #region Public Methods

        public UserLogin Insert()
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            db.UserLogin.Add(this);
            db.SaveChanges();
            return this;
        }

        public UserLogin? GetById(long id)
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            return db.UserLogin.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public UserLogin? GetByUserName(string userName)
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            return db.UserLogin.AsNoTracking().FirstOrDefault(x => x.Username == userName);
        }

        public UserLogin? GetByUserEmail(string email)
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            return db.UserLogin.AsNoTracking().FirstOrDefault(x => x.Email == email);
        }

        public void Save()
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            if (Id == 0) db.UserLogin.Add(this);
            else db.UserLogin.Update(this);
            db.SaveChanges();
        }

        public bool Delete()
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            db.UserLogin.Remove(this);
            return db.SaveChanges() > 0;
        }

        public bool DeleteById(long id)
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();
            var e = new UserLogin { Id = id };
            db.Entry(e).State = EntityState.Deleted;
            try { return db.SaveChanges() > 0; }
            catch (DbUpdateConcurrencyException) { return false; }
        }

        public  UserLogin? FindByUsernameOrEmail(string usernameOrEmail)
        {
            var user = this.GetByUserName(usernameOrEmail) ?? this.GetByUserEmail(usernameOrEmail);
            return user;
        }

        #endregion
    }
}
