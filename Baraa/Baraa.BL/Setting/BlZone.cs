using Baraa.BL.Setting;
using Baraa.DAL;
using Baraa.Model;
using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.BLL.Setting
{
    public class BlZone : BaseClass
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
        public bool IsExist(string ZoneName, Language language) => language == Language.Arabic ? repoZone.DbSet.Any(query => query.ZoneNameAr.Trim() == ZoneName.Trim() && query.IsDeleted == false) : repoZone.DbSet.Any(query => query.ZoneNameEn.ToLower().Trim() == ZoneName.ToLower().Trim() && query.IsDeleted == false);

        /// <summary>
        /// Add New Zone
        /// </summary>
        public bool AddZone(Zone Zone) => repoZone.Insert(Zone);
        /// <summary>
        /// Delete Zone By ID
        /// </summary>
        /// <param name="ZoneID">Zone ID</param>
        /// <returns></returns>
        public bool DeleteZone(Guid ZoneID) => repoZone.Delete(ZoneID);
        /// <summary>
        /// Update Zone
        /// </summary>
        /// <param name="oldZone"></param>
        /// <param name="newZone"></param>
        /// <returns></returns>
        public bool UpdateZone(Zone oldZone, Zone newZone) => repoZone.Update(oldZone, newZone);



    }
}
