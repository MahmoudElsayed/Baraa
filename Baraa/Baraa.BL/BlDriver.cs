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
    public class BlDriver : BaseClass
    {
        IRepository<Driver> repoDriver;
        public BlDriver(IRepository<Driver> repoDriver)
        {
            this.repoDriver = repoDriver;
        }

        /// <summary>
        /// Add New Driver
        /// </summary>
        public bool AddDriver(Driver Driver) => repoDriver.Insert(Driver);
        /// <summary>
        /// Delete Driver By ID
        /// </summary>
        /// <param name="DriverID">Driver ID</param>
        /// <returns></returns>
        public bool DeleteDriver(Guid DriverID) => repoDriver.Delete(DriverID);
        /// <summary>
        /// Update Driver
        /// </summary>
        /// <param name="oldDriver"></param>
        /// <param name="newDriver"></param>
        /// <returns></returns>
        public bool UpdateDriver(Driver oldDriver, Driver newDriver) => repoDriver.Update(oldDriver, newDriver);



    }
}
