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
    public class BlAgent : BaseClass
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
        public bool IsExist(string AgentName, Language language) => language==Language.Arabic? repoAgent.DbSet.Any(query => query.AgentNameAR.Trim() == AgentName.Trim()&&query.IsDeleted==false): repoAgent.DbSet.Any(query => query.AgentNameEN.ToLower().Trim() == AgentName.ToLower().Trim() && query.IsDeleted == false);

        /// <summary>
        /// Add New Agent
        /// </summary>
        public bool AddAgent(Agent agent) => repoAgent.Insert(agent);
        /// <summary>
        /// Delete Agent By ID
        /// </summary>
        /// <param name="agentID">Agent ID</param>
        /// <returns></returns>
        public bool DeleteAgent(Guid agentID) => repoAgent.Delete(agentID);
        /// <summary>
        /// Update Agent
        /// </summary>
        /// <param name="oldAgent"></param>
        /// <param name="newAgent"></param>
        /// <returns></returns>
        public bool UpdateAgent(Agent oldAgent,Agent newAgent) => repoAgent.Update(oldAgent,newAgent);



    }
}
