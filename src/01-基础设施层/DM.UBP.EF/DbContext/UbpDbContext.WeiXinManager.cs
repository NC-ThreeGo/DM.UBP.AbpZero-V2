//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.Data.Entity;
using DM.UBP.Authorization.Users;
using DM.UBP.Domain.Entity.WeiXinManager;
using DM.UBP.Organizations;

namespace DM.UBP.EF
{
    /// <summary>
    /// WeiXinManager的DbContext
    /// <summary>
    public partial class UbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...
        public virtual IDbSet<WeiXinConfig> WeiXinConfigs { get; set; }

        public virtual IDbSet<WeiXinApp> WeiXinApps { get; set; }

        public virtual IDbSet<WX_User> WXUser { get; set; }

        public virtual IDbSet<WX_OrganizationUnit> WXOrganizationUnit { get; set; }

        //@@Insert Position
    }
}
