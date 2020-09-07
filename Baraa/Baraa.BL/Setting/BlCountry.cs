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
    public class BlCountry : BaseClass
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
        public bool IsExist(string CountryName, Language language) => language == Language.Arabic ? repoCountry.DbSet.Any(query => query.CountryNameAR.Trim() == CountryName.Trim() && query.IsDeleted == false) : repoCountry.DbSet.Any(query => query.CountryNameEN.ToLower().Trim() == CountryName.ToLower().Trim() && query.IsDeleted == false);

        /// <summary>
        /// Add New Country
        /// </summary>
        public bool AddCountry(Country Country) => repoCountry.Insert(Country);
        /// <summary>
        /// Delete Country By ID
        /// </summary>
        /// <param name="CountryID">Country ID</param>
        /// <returns></returns>
        public bool DeleteCountry(Guid CountryID) => repoCountry.Delete(CountryID);
        /// <summary>
        /// Update Country
        /// </summary>
        /// <param name="oldCountry"></param>
        /// <param name="newCountry"></param>
        /// <returns></returns>
        public bool UpdateCountry(Country oldCountry, Country newCountry) => repoCountry.Update(oldCountry, newCountry);



    }
}
