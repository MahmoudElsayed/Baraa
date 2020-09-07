using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Baraa.Model.Operator
{
    [Table("Operator.OperatorAgent")]
    public partial class OperatorAgent : BaseEntity
    {
        public OperatorAgent()
        {
            OperatorAgentGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperatorAgentID { get; set; }

        public Guid OperatorAgentGuid { get; set; }

        public int AgentID { get; set; }
        public int OperatorID { get; set; }
        public virtual User User { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual OperatorUser OperatorUser { get; set; }

    }
}
