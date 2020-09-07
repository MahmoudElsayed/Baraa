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
    public class BlZoneCity : BaseEntity
    {
        IRepository<ZoneCity> repoZoneCity;
        public BlZoneCity(IRepository<ZoneCity> repoZoneCity)
        {
            this.repoZoneCity = repoZoneCity;
        }
        /// <summary>
        /// Check If zone id and city id  are Used Before Or Not
        /// </summary>
        /// <param name="cityID"></param>
        /// <param name="zoneID"></param>

        /// <returns></returns>
        public bool IsExist(int cityID, int zoneID) => repoZoneCity.DbSet.Any(query => query.CityID == cityID && query.ZoneID == zoneID);

        /// <summary>
        /// Add New ZoneCity
        /// </summary>
        public bool Insert(ZoneCity zoneCity) => repoZoneCity.Insert(zoneCity);
        /// <summary>
        /// Delete ZoneCity By ID
        /// </summary>
        /// <param name="ZoneCityID">ZoneCity ID</param>
        /// <returns></returns>
        public bool Delete(Guid zoneCityID) => repoZoneCity.Delete(zoneCityID);
        /// <summary>
        /// Update ZoneCity
        /// </summary>
        /// <param name="zoneCity"></param>
        /// <returns></returns>
        public bool Update(ZoneCity zoneCity) => repoZoneCity.Update(zoneCity);



    }
}
