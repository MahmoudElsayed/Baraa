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
    public class BlCompany :BaseEntity
    {
        IRepository<Company> repoCompany;
        public BlCompany(IRepository<Company> repoCompany)
        {
            this.repoCompany = repoCompany;
        }
        /// <summary>
        /// Check If Company Name Is Used Before Or Not
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="Language"></param>

        /// <returns></returns>
        public bool IsExist(string companyName) => repoCompany.DbSet.Any(query => query.CompanyName.Trim() == companyName.Trim() && query.IsDeleted);

        /// <summary>
        /// Add New Company
        /// </summary>
        public bool Insert(Company company) => repoCompany.Insert(company);
        /// <summary>
        /// Delete Company By ID
        /// </summary>
        /// <param name="CompanyID">Company ID</param>
        /// <returns></returns>
        public bool Delete(Guid companyID) => repoCompany.Delete(companyID);
        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public bool Update(Company company) => repoCompany.Update(company);



    }
}
