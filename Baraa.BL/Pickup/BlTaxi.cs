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
    public class BlTaxi :BaseEntity
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
        public bool Insert(Taxi taxi) => repoTaxi.Insert(taxi);
        /// <summary>
        /// Delete Taxi By ID
        /// </summary>
        /// <param name="taxiID">Taxi ID</param>
        /// <returns></returns>
        public bool Delete(Guid taxiID) => repoTaxi.Delete(taxiID);
        /// <summary>
        /// Update Taxi
        /// </summary>
        /// <param name="taxi"></param>
        /// <returns></returns>
        public bool Update(Taxi taxi) => repoTaxi.Update(taxi);



    }
}
