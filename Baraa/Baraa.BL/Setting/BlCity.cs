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
    public class BlCity : BaseClass
    {
        IRepository<City> repoCity;
        public BlCity(IRepository<City> repoCity)
        {
            this.repoCity = repoCity;
        }
        /// <summary>
        /// Check If City Name Is Used Before Or Not
        /// </summary>
        /// <param name="CityName"></param>
        /// <param name="Language"></param>

        /// <returns></returns>
        public bool IsExist(string CityName, Language language) => language == Language.Arabic ? repoCity.DbSet.Any(query => query.CityNameAR.Trim() == CityName.Trim() && query.IsDeleted == false) : repoCity.DbSet.Any(query => query.CityNameEN.ToLower().Trim() == CityName.ToLower().Trim() && query.IsDeleted == false);

        /// <summary>
        /// Add New City
        /// </summary>
        public bool AddCity(City City) => repoCity.Insert(City);
        /// <summary>
        /// Delete City By ID
        /// </summary>
        /// <param name="CityID">City ID</param>
        /// <returns></returns>
        public bool DeleteCity(Guid CityID) => repoCity.Delete(CityID);
        /// <summary>
        /// Update City
        /// </summary>
        /// <param name="oldCity"></param>
        /// <param name="newCity"></param>
        /// <returns></returns>
        public bool UpdateCity(City oldCity, City newCity) => repoCity.Update(oldCity, newCity);



    }
}
