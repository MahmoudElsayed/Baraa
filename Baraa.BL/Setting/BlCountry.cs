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
    public class BlCountry : BaseEntity
    {
        IRepository<Country> repoCountry;
        public BlCountry(IRepository<Country> repoCountry)
        {
            this.repoCountry = repoCountry;
        }
        /// <summary>
        /// Check If Country Name Is Used Before Or Not
        /// </summary>
        /// <param name="CountryName"></param>
        /// <param name="Language"></param>

        /// <returns></returns>
        public bool IsExist(string countryName, Language language) => language == Language.Arabic ? repoCountry.DbSet.Any(query => query.NameAR.Trim() == countryName.Trim() && !query.IsDeleted) : repoCountry.DbSet.Any(query => query.NameEN.ToLower().Trim() == countryName.ToLower().Trim() && !query.IsDeleted);

        /// <summary>
        /// Add New Country
        /// </summary>
        public bool Insert(Country country) => repoCountry.Insert(country);
        /// <summary>
        /// Delete Country By ID
        /// </summary>
        /// <param name="CountryID">Country ID</param>
        /// <returns></returns>
        public bool Delete(Guid countryID) => repoCountry.Delete(countryID);
        /// <summary>
        /// Update Country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public bool Update(Country country, Country newCountry) => repoCountry.Update(country);



    }
}
