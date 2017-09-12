using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class MyDbContext : DbContext
    {

        #region Constructors

        public MyDbContext()
          : base()
        {
        }

        public MyDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public MyDbContext(DbConnection connection)
          : base(connection, true)
        {
        }

        #endregion
    }

    public class Field
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Int16 Length { get; set; }

        public Byte Scale { get; set; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comments { get; set; }

        public bool IsPk { get; set; }

        /// <summary>
        /// 实体类中的属性名
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 当前字段是否需要包含在InputDto中
        /// </summary>
        public bool HasInputDto { get; set; }

        /// <summary>
        /// 当前字段是否需要包含在OutputDto中
        /// </summary>
        public bool HasOutputDto { get; set; }

        /// <summary>
        /// jtable中列的宽度（百分比）
        /// </summary>
        public int TableColWidth { get; set; }

        /// <summary>
        /// 当前属性是否可编辑
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
