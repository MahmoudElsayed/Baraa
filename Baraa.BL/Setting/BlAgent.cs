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
    public class BlAgent : BaseEntity
    {
        IRepository<Agent> repoAgent;
        public BlAgent(IRepository<Agent> repoAgent)
        {
            this.repoAgent = repoAgent;
        }
        /// <summary>
        /// Check If Agent Name Is Used Before Or Not
        /// </summary>
        /// <param name="AgentName"></param>
        /// <param name="Language"></param>

        /// <returns></returns>
        public bool IsExist(string agentName, Language language) => language==Language.Arabic? repoAgent.DbSet.Any(query => query.NameAR.Trim() == agentName.Trim()&&!query.IsDeleted): repoAgent.DbSet.Any(query => query.NameEn.ToLower().Trim() == agentName.ToLower().Trim() && !query.IsDeleted);

        /// <summary>
        /// Add New Agent
        /// </summary>
        public bool Insert(Agent agent) => repoAgent.Insert(agent);
        /// <summary>
        /// Delete Agent By ID
        /// </summary>
        /// <param name="agentID">Agent ID</param>
        /// <returns></returns>
        public bool Delete(Guid agentID) => repoAgent.Delete(agentID);
        /// <summary>
        /// Update Agent
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public bool Update(Agent agent) => repoAgent.Update(agent);



    }
}
