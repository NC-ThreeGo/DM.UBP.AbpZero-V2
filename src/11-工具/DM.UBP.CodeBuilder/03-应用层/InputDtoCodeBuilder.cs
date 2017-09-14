using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class InputDtoCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        public string _RelativePath = @"02-领域层\DM.UBP.Application.Dto\";
        public string SubNameSpace = "Application.Dto";

        public EntityCodeBuilder EntityCodeBuilder { get; set; }

        public string BaseClass { get; set; }

        public InputDtoCodeBuilder(EntityCodeBuilder entityCodeBuilder, string className, string functionName)
        {
            EntityCodeBuilder = entityCodeBuilder;

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["DtoCodePath"]))
            {
                _RelativePath = ConfigurationManager.AppSettings["DtoCodePath"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["DtoNameSpace"]))
            {
                SubNameSpace = ConfigurationManager.AppSettings["DtoNameSpace"];
            }

            ModuleName = EntityCodeBuilder.ModuleName;
            SubModuleName = EntityCodeBuilder.SubModuleName;
            ClassName = className;
            FileName = className;
            FunctionName = functionName;
        }

        public override void InternalCreateCode()
        {
            this.WriteUsing();
            this.WriteClass();
        }

        private void WriteUsing()
        {
            CodeText.AppendLine("using System.ComponentModel.DataAnnotations;");
            CodeText.AppendLine("using Abp.AutoMapper;");
            CodeText.AppendLine("using Abp.Application.Services.Dto;");
            CodeText.AppendLine("using DM.UBP.Domain.Entity;");
            CodeText.AppendLine("using " + EntityCodeBuilder.FullNameSpace + ";");
            CodeText.AppendLine("");
        }

        private void WriteClass()
        {
            FullNameSpace = RootNameSpace + "." + SubNameSpace + "."
                + ModuleName + (String.IsNullOrEmpty(SubModuleName) ? "" : "." + SubModuleName)
                + "." + FunctionName;
            CodeText.AppendLine("namespace " + FullNameSpace);
            CodeText.AppendLine("{");
            CodeText.AppendLine("/// <summary>");
            CodeText.AppendLine("/// " + ClassComments);
            CodeText.AppendLine("/// <summary>");
            CodeText.AppendLine("[AutoMapTo(typeof(" + EntityCodeBuilder.ClassName + "))]");

            //if (EntityCodeBuilder.BaseClass.ToLower() == "fullauditedentity")
            //{
            //    BaseClass = "FullAuditedEntityDto";
            //}

            //if (EntityCodeBuilder.BaseClass.ToLower() == "auditedentity")
            //{
            //    BaseClass = "AuditedEntityDto";
            //}

            //if (EntityCodeBuilder.BaseClass.ToLower() == "creationauditedentity")
            //{
            //    BaseClass = "CreationAuditedEntityDto";
            //}

            //传入参数不需要create等相关信息
            BaseClass = "EntityDto";

            CodeText.AppendLine("public class " + ClassName + " : " + BaseClass 
                + (EntityCodeBuilder.PkType.ToLower() == "int" ? "" : "<" + EntityCodeBuilder.PkType + ">"));
            CodeText.AppendLine("{");

            this.WriteProperty();

            CodeText.AppendLine("}");
            CodeText.AppendLine("}");
        }

        private void WriteProperty()
        {
            foreach (Field field in EntityCodeBuilder.Fields)
            {
                if (field.HasInputDto)
                {
                    if (!String.IsNullOrEmpty(field.Comments))
                    {
                        CodeText.AppendLine("[Display(Name = \"" + field.Comments + "\")]");
                    }

                    string type = Utils.GetCSType(EntityCodeBuilder.DbType, field);
                    if (type == "string")
                    {
                        CodeText.AppendLine("[StringLength(StringMaxLengthConst.MaxStringLength" + field.Length + ")]");
                    }

                    if (!field.Nullable)
                    {
                        CodeText.AppendLine("[Required]");
                    }

                    //CodeText.AppendLine("public " + type + (field.Nullable && type != "string" ? "? " : " ") + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(field.Name.ToLower()) + " { get; set; } ");
                    CodeText.AppendLine("public " + type + (field.Nullable && type != "string" ? "? " : " ") + field.Property + " { get; set; } ");
                    CodeText.AppendLine("");
                }
            }
        }
    }
}
