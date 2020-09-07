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
    public class BlCity : BaseEntity
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
        public bool IsExist(string cityName, Language language) => language == Language.Arabic ? repoCity.DbSet.Any(query => query.NameAR.Trim() == cityName.Trim() && query.IsDeleted == false) : repoCity.DbSet.Any(query => query.NameEN.ToLower().Trim() ==cityName.ToLower().Trim() && !query.IsDeleted);
       
        /// <summary>
        /// Add New City
        /// </summary>
        public bool Insert(City city) => repoCity.Insert(city);
        /// <summary>
        /// Delete City By ID
        /// </summary>
        /// <param name="CityID">City ID</param>
        /// <returns></returns>
        public bool Delete(Guid cityID) => repoCity.Delete(cityID);
        /// <summary>
        /// Update City
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public bool Update(City city) => repoCity.Update(city);



    }
}
