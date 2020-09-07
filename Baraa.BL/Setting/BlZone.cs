using Baraa.DAL;
using Baraa.DAL.Contract;
using Baraa.Model;
using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.BLL.Setting
{
    public class BlZone : BaseEntity
    {
        IRepository<Zone> repoZone;
        public BlZone(IRepository<Zone> repoZone)
        {
            this.repoZone = repoZone;
        }
        /// <summary>
        /// Check If Zone Name Is Used Before Or Not
        /// </summary>
        /// <param name="ZoneName"></param>
        /// <param name="Language"></param>

        /// <returns></returns>
        public bool IsExist(string zoneName, Language language) => language == Language.Arabic ? repoZone.DbSet.Any(query => query.NameAr.Trim() == zoneName.Trim() && !query.IsDeleted ) : repoZone.DbSet.Any(query => query.NameEn.ToLower().Trim() == zoneName.ToLower().Trim() && !query.IsDeleted);

        /// <summary>
        /// Add New Zone
        /// </summary>
        public bool Insert(Zone zone) => repoZone.Insert(zone);
        /// <summary>
        /// Delete Zone By ID
        /// </summary>
        /// <param name="ZoneID">Zone ID</param>
        /// <returns></returns>
        public bool Delete(Guid zoneID) => repoZone.Delete(zoneID);
        /// <summary>
        /// Update Zone
        /// </summary>
        /// <param name="zone"></param>
        /// <returns></returns>
        public bool Update(Zone zone) => repoZone.Update(zone);



    }
}
