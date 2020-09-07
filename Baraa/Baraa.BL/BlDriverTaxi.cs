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
    public class BlDriverTaxi : BaseClass
    {
        IRepository<DriverTaxi> repoDriverTaxi;
        public BlDriverTaxi(IRepository<DriverTaxi> repoDriverTaxi)
        {
            this.repoDriverTaxi = repoDriverTaxi;
        }
        /// <summary>
        /// Check If Driver for Taxi  Is Used Before Or Not
        /// </summary>
        /// <param name="DriverTaxiNumber"></param>

        /// <returns></returns>
        public bool DriverTaxiNumberIsExists(int taxiID,int driverID) => repoDriverTaxi.DbSet.Any(query => query.TaxiID == taxiID&&query.DriverID==driverID);

        /// <summary>
        /// Add New DriverTaxi
        /// </summary>
        public bool AddDriverTaxi(DriverTaxi DriverTaxi) => repoDriverTaxi.Insert(DriverTaxi);
        /// <summary>
        /// Delete DriverTaxi By ID
        /// </summary>
        /// <param name="DriverTaxiID">DriverTaxi ID</param>
        /// <returns></returns>
        public bool DeleteDriverTaxi(Guid DriverTaxiID) => repoDriverTaxi.Delete(DriverTaxiID);
        /// <summary>
        /// Update DriverTaxi
        /// </summary>
        /// <param name="oldDriverTaxi"></param>
        /// <param name="newDriverTaxi"></param>
        /// <returns></returns>
        public bool UpdateDriverTaxi(DriverTaxi oldDriverTaxi, DriverTaxi newDriverTaxi) => repoDriverTaxi.Update(oldDriverTaxi, newDriverTaxi);



    }
}
