using DM.UBP.Domain.Entity.ReportManage;
using System.Data.Entity;

namespace DM.UBP.EF
{
    public partial class UbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...
        public virtual IDbSet<RptCategorys> RptCategorys { get; set; }
    }
}
