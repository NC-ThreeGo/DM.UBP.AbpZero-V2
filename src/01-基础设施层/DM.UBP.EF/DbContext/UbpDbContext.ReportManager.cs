//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.Data.Entity;
using DM.UBP.Domain.Entity.ReportManager;

namespace DM.UBP.EF
{
    /// <summary>
    /// ReportManager的DbContext
    /// <summary>
    public partial class UbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...
        public virtual IDbSet<ReportCategory> Categories { get; set; }

        public virtual IDbSet<ReportTemplate> ReportTemplates { get; set; }

        public virtual IDbSet<ReportDataSource> ReportDataSources { get; set; }

        public virtual IDbSet<ReportParameter> ReportParameters { get; set; }

        //@@Insert Position




    }
}
