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
    public class BlDriverTaxi :BaseEntity
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
        public bool Insert(DriverTaxi driverTaxi) => repoDriverTaxi.Insert(driverTaxi);
        /// <summary>
        /// Delete DriverTaxi By ID
        /// </summary>
        /// <param name="DriverTaxiID">DriverTaxi ID</param>
        /// <returns></returns>
        public bool Delete(Guid driverTaxiID) => repoDriverTaxi.Delete(driverTaxiID);
        /// <summary>
        /// Update DriverTaxi
        /// </summary>
        /// <param name="driverTaxi"></param>
        /// <returns></returns>
        public bool Update(DriverTaxi driverTaxi) => repoDriverTaxi.Update(driverTaxi);



    }
}
