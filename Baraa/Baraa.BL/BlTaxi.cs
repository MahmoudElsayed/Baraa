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
    public class BlTaxi : BaseClass
    {
        IRepository<Taxi> repoTaxi;
        public BlTaxi(IRepository<Taxi> repoTaxi)
        {
            this.repoTaxi = repoTaxi;
        }
        /// <summary>
        /// Check If Taxi Number Is Used Before Or Not
        /// </summary>
        /// <param name="taxiNumber"></param>

        /// <returns></returns>
        public bool TaxiNumberIsExists(string taxiNumber) => repoTaxi.DbSet.Any(query => query.TaxiNumber == taxiNumber);

        /// <summary>
        /// Add New Taxi
        /// </summary>
        public bool AddTaxi(Taxi Taxi) => repoTaxi.Insert(Taxi);
        /// <summary>
        /// Delete Taxi By ID
        /// </summary>
        /// <param name="TaxiID">Taxi ID</param>
        /// <returns></returns>
        public bool DeleteTaxi(Guid TaxiID) => repoTaxi.Delete(TaxiID);
        /// <summary>
        /// Update Taxi
        /// </summary>
        /// <param name="oldTaxi"></param>
        /// <param name="newTaxi"></param>
        /// <returns></returns>
        public bool UpdateTaxi(Taxi oldTaxi, Taxi newTaxi) => repoTaxi.Update(oldTaxi, newTaxi);



    }
}
