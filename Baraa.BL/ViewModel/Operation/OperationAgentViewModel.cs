using Baraa.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.BLL.ViewModel.Operation
{
    public class OperationAgentViewModel : BaseEntity
    {
        public int OperationAgentID { get; set; }
        public int AgentID { get; set; }
        public int OUserID { get; set; }

    }
}
