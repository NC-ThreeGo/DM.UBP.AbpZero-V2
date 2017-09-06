using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class EntityCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        public string _RelativePath = @"02-领域层\DM.UBP.Domain.Entity\";
        public string SubNameSpace = "Domain.Entity";

        public DbType DbType { get; set; }

        public string BaseClass { get; set; }

        public string PkType { get; set; }

        public string TableName { get; set; }

        /// <summary>
        /// 类的复数名
        /// </summary>
        public string ClassPluralName { get; set; }

        /// <summary>
        /// 实现多租户的接口：None， IMayHaveTenant、IMustHaveTenant
        /// </summary>
        public string TenantInterface { get; set; }

        public List<Field> Fields { get; set; }

        public EntityCodeBuilder()
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["EntityCodePath"]))
            {
                _RelativePath = ConfigurationManager.AppSettings["EntityCodePath"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["EntityNameSpace"]))
            {
                SubNameSpace = ConfigurationManager.AppSettings["EntityNameSpace"];
            }
        }

        public override void InternalCreateCode()
        {
            this.WriteUsing();
            this.WriteClass();
        }

        private void WriteUsing()
        {
            CodeText.AppendLine("using System;");
            CodeText.AppendLine("using System.ComponentModel.DataAnnotations; ");
            CodeText.AppendLine("using Abp.Domain.Entities; ");
            CodeText.AppendLine("using Abp.Domain.Entities.Auditing;");
            CodeText.AppendLine("using Abp.MultiTenancy; ");
            CodeText.AppendLine("");
        }

        private void WriteClass()
        {
            FullNameSpace = RootNameSpace + "." + SubNameSpace + "." + ModuleName + (String.IsNullOrEmpty(SubModuleName) ? "" : "." + SubModuleName);
            CodeText.AppendLine("namespace " + FullNameSpace);
            CodeText.AppendLine("{");
            CodeText.AppendLine("/// <summary>");
            CodeText.AppendLine("/// " + ClassComments);
            CodeText.AppendLine("/// <summary>");
            CodeText.AppendLine("public class " + ClassName + " : " + BaseClass + (PkType.ToLower() == "int" ? "" : "<" + PkType + ">") 
                + (String.IsNullOrEmpty(TenantInterface) ? "" : ", " + TenantInterface));
            CodeText.AppendLine("{");

            this.WriteProperty();

            CodeText.AppendLine("}");
            CodeText.AppendLine("}");
        }

        private void WriteProperty()
        {
            foreach(Field field in Fields)
            {
                //审计字段已经在基类里实现了，所以这里就不用再定义了。
                if (BaseClass.ToLower() == "fullauditedentity")
                {
                    if ((field.Name.ToLower() == "isdeleted") ||
                        (field.Name.ToLower() == "deleteruserid") ||
                        (field.Name.ToLower() == "deletiontime") ||
                        (field.Name.ToLower() == "lastmodificationtime") ||
                        (field.Name.ToLower() == "lastmodifieruserid") ||
                        (field.Name.ToLower() == "creationtime") ||
                        (field.Name.ToLower() == "creatoruserid"))
                        continue;
                }

                if (BaseClass.ToLower() == "auditedentity")
                {
                    if ((field.Name.ToLower() == "lastmodificationtime") ||
                        (field.Name.ToLower() == "lastmodifieruserid") ||
                        (field.Name.ToLower() == "creationtime") ||
                        (field.Name.ToLower() == "creatoruserid"))
                        continue;
                }

                if (BaseClass.ToLower() == "creationauditedentity")
                {
                    if ((field.Name.ToLower() == "creationtime") ||
                        (field.Name.ToLower() == "creatoruserid"))
                        continue;
                }

                //由于ABP的主键必须是Id，所以当前字段如果是主键则不能再被定义。如果表中的主键字段名<>Id，则需要定义映射类。
                if (field.IsPk)
                {
                    continue;
                }

                if (!String.IsNullOrEmpty(field.Comments))
                {
                    CodeText.AppendLine("/// <summary>");
                    CodeText.AppendLine("/// " + field.Comments);
                    CodeText.AppendLine("/// <summary>");
                    CodeText.AppendLine("[Display(Name = \"" + field.Comments + "\")]");
                }

                string type = Utils.GetCSType(DbType, field);
                if (type == "string")
                {
                    CodeText.AppendLine("[StringLength(StringMaxLengthConst.MaxStringLength" + field.Length + ")]");
                }

                CodeText.AppendLine("public " + type + (field.Nullable && type != "string" ? "? " : " ") + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(field.Name.ToLower()) + " { get; set; } ");

                CodeText.AppendLine("");
            }
        }
    }
}
