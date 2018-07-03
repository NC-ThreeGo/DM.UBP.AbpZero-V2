using DM.UBP.Domain.Entity.BackgroundJobManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.EF
{
    public partial class UbpDbContext
    {
        public virtual IDbSet<Job_RPTEmail> Job_RPTEmails { get; set; }
        public virtual IDbSet<JobGroup> JobGroups { get; set; }
        public virtual IDbSet<Scheduler> Schedulers { get; set; }
        public virtual IDbSet<Trigger> Triggers { get; set; }

        public virtual IDbSet<Job_Sql> Job_Sql { get; set; }

//@@Insert Position


    }
}
