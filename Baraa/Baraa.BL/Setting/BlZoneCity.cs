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
    public class BlZoneCity : BaseClass
    {
        IRepository<ZoneCity> repoZoneCity;
        public BlZoneCity(IRepository<ZoneCity> repoZoneCity)
        {
            this.repoZoneCity = repoZoneCity;
        }
        /// <summary>
        /// Check If ZoneCity Name Is Used Before Or Not
        /// </summary>
        /// <param name="ZoneCityName"></param>
        /// <param name="Language"></param>

        /// <returns></returns>
        public bool IsExist(int cityID, int zoneID) => repoZoneCity.DbSet.Any(query => query.CityID == cityID && query.ZoneID == zoneID);

        /// <summary>
        /// Add New ZoneCity
        /// </summary>
        public bool AddZoneCity(ZoneCity ZoneCity) => repoZoneCity.Insert(ZoneCity);
        /// <summary>
        /// Delete ZoneCity By ID
        /// </summary>
        /// <param name="ZoneCityID">ZoneCity ID</param>
        /// <returns></returns>
        public bool DeleteZoneCity(Guid ZoneCityID) => repoZoneCity.Delete(ZoneCityID);
        /// <summary>
        /// Update ZoneCity
        /// </summary>
        /// <param name="oldZoneCity"></param>
        /// <param name="newZoneCity"></param>
        /// <returns></returns>
        public bool UpdateZoneCity(ZoneCity oldZoneCity, ZoneCity newZoneCity) => repoZoneCity.Update(oldZoneCity, newZoneCity);



    }
}
