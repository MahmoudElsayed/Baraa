using Baraa.DAL.Contract;
using Baraa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.BLL.General
{
    public class BLUser : BaseEntity
    {
        IRepository<User> repoUser;
        public BLUser(IRepository<User> repoUser)
        {
            this.repoUser = repoUser;
        }

        /// <summary>
        /// Get User Data By User Name
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public User GetUserInfo(string userName)
        {
            return repoUser.Find(e => e.UserName == userName || e.Email == userName).FirstOrDefault();
        }

        /// <summary>
        /// Get User Info By User Id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public User GetUserInfo(int userID)
        {
            return repoUser.DbSet.FirstOrDefault(e => e.Id==userID+"");
        }

        public User GetUserInfoByPasswordHash(string passwordHash)
        {
            return repoUser.DbSet.FirstOrDefault(e => e.PasswordHash == passwordHash);
        }
    }
}
