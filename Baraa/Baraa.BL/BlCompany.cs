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
    public class BlCompany : BaseClass
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
        public bool IsExist(string CompanyName) => repoCompany.DbSet.Any(query => query.CompanyName.Trim() == CompanyName.Trim() && query.IsDeleted == false);

        /// <summary>
        /// Add New Company
        /// </summary>
        public bool AddCompany(Company Company) => repoCompany.Insert(Company);
        /// <summary>
        /// Delete Company By ID
        /// </summary>
        /// <param name="CompanyID">Company ID</param>
        /// <returns></returns>
        public bool DeleteCompany(Guid CompanyID) => repoCompany.Delete(CompanyID);
        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="oldCompany"></param>
        /// <param name="newCompany"></param>
        /// <returns></returns>
        public bool UpdateCompany(Company oldCompany, Company newCompany) => repoCompany.Update(oldCompany, newCompany);



    }
}
