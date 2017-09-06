using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class EntityConfigurationCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        public string _RelativePath = @"02-领域层\DM.UBP.Domain.EntityConfiguration\";
        public string SubNameSpace = "Domain.EntityConfiguration";

        public EntityCodeBuilder EntityCodeBuilder { get; set; }

        public EntityConfigurationCodeBuilder(EntityCodeBuilder entityCodeBuilder)
        {
            EntityCodeBuilder = entityCodeBuilder;

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["EntityConfigurationCodePath"]))
            {
                _RelativePath = ConfigurationManager.AppSettings["EntityConfigurationCodePath"];
            }
            if (_RelativePath.Last() == '\\')
                _RelativePath = _RelativePath.Substring(0, _RelativePath.Length - 1);
            _RelativePath = _RelativePath + "." + EntityCodeBuilder.DbType.ToString() + "\\";

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["EntityConfigurationNameSpace"]))
            {
                SubNameSpace = ConfigurationManager.AppSettings["EntityConfigurationNameSpace"];
            }
            SubNameSpace = SubNameSpace + "." + EntityCodeBuilder.DbType.ToString();

            FileName = EntityCodeBuilder.FileName + "Configuration";
            ClassName = EntityCodeBuilder.ClassName + "Configuration";
            ModuleName = EntityCodeBuilder.ModuleName;
            SubModuleName = EntityCodeBuilder.SubModuleName;
        }

        public override void InternalCreateCode()
        {
            this.WriteUsing();
            this.WriteClass();
        }

        private void WriteUsing()
        {
            CodeText.AppendLine("using DM.UBP.EF;");
            CodeText.AppendLine("using " + EntityCodeBuilder.FullNameSpace + ";");
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
            CodeText.AppendLine("public class " + ClassName + " : EntityConfigurationBase<" + EntityCodeBuilder.ClassName + ", " + EntityCodeBuilder.PkType.ToLower() + ">");
            CodeText.AppendLine("{");

            this.WriteConstructor();

            CodeText.AppendLine("}");
            CodeText.AppendLine("}");
        }

        private void WriteConstructor()
        {
            CodeText.AppendLine("public " + ClassName + "()");
            CodeText.AppendLine("{");
            
            if (EntityCodeBuilder.TableName.ToLower() != EntityCodeBuilder.ClassName.ToLower())
            {
                CodeText.AppendLine("this.ToTable(\"" + EntityCodeBuilder.TableName.ToUpper() + "\");");
            }

            foreach (Field field in EntityCodeBuilder.Fields)
            {
                if (field.IsPk && (field.Name != "Id"))
                {
                    CodeText.AppendLine("this.Property(p => p.Id).HasColumnName(\"" + field.Name.ToUpper() + "\");");
                }

                if (!String.IsNullOrEmpty(field.Property) && (field.Property.ToLower() != field.Name.ToLower()))
                {
                    CodeText.AppendLine("this.Property(p => p." + field.Property + ").HasColumnName(\"" + field.Name.ToUpper() + "\");");
                }
            }

            CodeText.AppendLine("}");
        }
    }
}
