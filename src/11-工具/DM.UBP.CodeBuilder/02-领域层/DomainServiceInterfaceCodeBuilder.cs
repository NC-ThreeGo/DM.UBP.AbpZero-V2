using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class DomainServiceInterfaceCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        public string _RelativePath = @"02-领域层\DM.UBP.Domain.Service\";
        public string SubNameSpace = "Domain.Service";

        public EntityCodeBuilder EntityCodeBuilder { get; set; }

        //五个方法
        public bool Has_Method_GetAllAsync { get; set; }
        public bool Has_Method_GetByIdAsync { get; set; }
        public bool Has_Method_CreateAsync { get; set; }
        public bool Has_Method_UpdateAsync { get; set; }
        public bool Has_Method_DeleteAsync { get; set; }

        public string Method_GetAllAsync_FullName { get; set; }
        public string Method_GetAllAsync_Name { get; set; }
        public string Method_GetByIdAsync_FullName { get; set; }
        public string Method_GetByIdAsync_Name { get; set; }
        public string Method_CreateAsync_FullName { get; set; }
        public string Method_CreateAsync_Name { get; set; }
        public string Method_UpdateAsync_FullName { get; set; }
        public string Method_UpdateAsync_Name { get; set; }
        public string Method_DeleteAsync_FullName { get; set; }
        public string Method_DeleteAsync_Name { get; set; }

        public DomainServiceInterfaceCodeBuilder(EntityCodeBuilder entityCodeBuilder, string functionName)
        {
            EntityCodeBuilder = entityCodeBuilder;

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["DomainServiceCodePath"]))
            {
                _RelativePath = ConfigurationManager.AppSettings["DomainServiceCodePath"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["DomainServiceNameSpace"]))
            {
                SubNameSpace = ConfigurationManager.AppSettings["DomainServiceNameSpace"];
            }

            ModuleName = EntityCodeBuilder.ModuleName;
            SubModuleName = EntityCodeBuilder.SubModuleName;
            FunctionName = functionName;
        }

        public override void InternalCreateCode()
        {
            this.WriteUsing();
            this.WriteClass();
        }

        private void WriteUsing()
        {
            CodeText.AppendLine("using System.Threading.Tasks;");
            CodeText.AppendLine("using System.Collections.Generic;");
            CodeText.AppendLine("using Abp.Domain.Services;");
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
            CodeText.AppendLine("public interface " + ClassName + " : IDomainService");
            CodeText.AppendLine("{");

            this.WriteMethod();

            CodeText.AppendLine("}");
            CodeText.AppendLine("}");
        }

        private void WriteMethod()
        {
            if (Has_Method_GetAllAsync)
            {
                Method_GetAllAsync_Name = "GetAll" + EntityCodeBuilder.ClassPluralName + "Async";
                Method_GetAllAsync_FullName = "Task<List<" + EntityCodeBuilder.ClassName + ">> " + Method_GetAllAsync_Name + "()";
                CodeText.AppendLine(Method_GetAllAsync_FullName + ";");
                CodeText.AppendLine("");
            }

            if (Has_Method_GetByIdAsync)
            {
                Method_GetByIdAsync_Name = "Get" + EntityCodeBuilder.ClassName + "ByIdAsync";
                Method_GetByIdAsync_FullName = "Task<" + EntityCodeBuilder.ClassName + "> " + Method_GetByIdAsync_Name
                    + "(" + EntityCodeBuilder.PkType.ToLower() + " id)";
                CodeText.AppendLine(Method_GetByIdAsync_FullName + ";");
                CodeText.AppendLine("");
            }

            if (Has_Method_CreateAsync)
            {
                Method_CreateAsync_Name = "Create" + EntityCodeBuilder.ClassName + "Async";
                Method_CreateAsync_FullName = "Task<bool> " + Method_CreateAsync_Name 
                    + "(" + EntityCodeBuilder.ClassName + " " + EntityCodeBuilder.ClassName.ToLower() + ")";
                CodeText.AppendLine(Method_CreateAsync_FullName + ";");
                CodeText.AppendLine("");
            }

            if (Has_Method_UpdateAsync)
            {
                Method_UpdateAsync_Name = "Update" + EntityCodeBuilder.ClassName + "Async";
                Method_UpdateAsync_FullName = "Task<bool> " + Method_UpdateAsync_Name
                    + "(" + EntityCodeBuilder.ClassName + " " + EntityCodeBuilder.ClassName.ToLower() + ")";
                CodeText.AppendLine(Method_UpdateAsync_FullName + ";");
                CodeText.AppendLine("");
            }

            if (Has_Method_DeleteAsync)
            {
                Method_DeleteAsync_Name = "Delete" + EntityCodeBuilder.ClassName + "Async";
                Method_DeleteAsync_FullName = "Task " + Method_DeleteAsync_Name 
                    + "(" + EntityCodeBuilder.ClassName + " " + EntityCodeBuilder.ClassName.ToLower() + ")";
                CodeText.AppendLine(Method_DeleteAsync_FullName + ";");
                CodeText.AppendLine("");
            }
        }
    }
}
