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
    public class BlDriver :BaseEntity
    {
        IRepository<Driver> repoDriver;
        public BlDriver(IRepository<Driver> repoDriver)
        {
            this.repoDriver = repoDriver;
        }

        /// <summary>
        /// Add New Driver
        /// </summary>
        public bool Insert(Driver driver) => repoDriver.Insert(driver);
        /// <summary>
        /// Delete Driver By ID
        /// </summary>
        /// <param name="DriverID">Driver ID</param>
        /// <returns></returns>
        public bool Delete(Guid driverID) => repoDriver.Delete(driverID);
        /// <summary>
        /// Update Driver
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public bool Update(Driver driver) => repoDriver.Update(driver);



    }
}
