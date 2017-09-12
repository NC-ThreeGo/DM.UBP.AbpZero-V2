using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class AppServiceInterfaceCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        public string _RelativePath = @"02-领域层\DM.UBP.Application.Service\";
        public string SubNameSpace = "Application.Service";

        public EntityCodeBuilder EntityCodeBuilder { get; set; }

        public InputDtoCodeBuilder InputDtoCodeBuilder { get; set; }
        public OutputDtoCodeBuilder OutputDtoCodeBuilder { get; set; }

        public DomainServiceInterfaceCodeBuilder DomainServiceInterfaceCodeBuilder { get; set; }

        // 5个方法名
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

        public AppServiceInterfaceCodeBuilder(EntityCodeBuilder entityCodeBuilder, 
            InputDtoCodeBuilder inputDtoCodeBuilder,
            OutputDtoCodeBuilder outputDtoCodeBuilder,
            DomainServiceInterfaceCodeBuilder domainServiceInterfaceCodeBuilder,
            string className, string functionName)
        {
            EntityCodeBuilder = entityCodeBuilder;
            InputDtoCodeBuilder = inputDtoCodeBuilder;
            OutputDtoCodeBuilder = outputDtoCodeBuilder;
            DomainServiceInterfaceCodeBuilder = domainServiceInterfaceCodeBuilder;

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ApplicationServiceCodePath"]))
            {
                _RelativePath = ConfigurationManager.AppSettings["ApplicationServiceCodePath"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ApplicationServiceNameSpace"]))
            {
                SubNameSpace = ConfigurationManager.AppSettings["ApplicationServiceNameSpace"];
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
            CodeText.AppendLine("using System.Threading.Tasks;");
            CodeText.AppendLine("using System.Collections.Generic;");
            CodeText.AppendLine("using Abp.Application.Services;");
            CodeText.AppendLine("using Abp.Application.Services.Dto;");
            CodeText.AppendLine("using " + InputDtoCodeBuilder.FullNameSpace + ";");
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
            CodeText.AppendLine("public interface " + ClassName + " : IApplicationService");
            CodeText.AppendLine("{");

            this.WriteMethod();

            CodeText.AppendLine("}");
            CodeText.AppendLine("}");
        }

        private void WriteMethod()
        {
            if (DomainServiceInterfaceCodeBuilder.Has_Method_GetAllAsync)
            {
                Method_GetAllAsync_Name = "Get" + EntityCodeBuilder.ClassPluralName;
                Method_GetAllAsync_FullName = "Task<PagedResultDto<" + OutputDtoCodeBuilder.ClassName + ">> " + Method_GetAllAsync_Name + "()";
                CodeText.AppendLine(Method_GetAllAsync_FullName + ";");
                CodeText.AppendLine("");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_GetByIdAsync)
            {
                Method_GetByIdAsync_Name = "Get" + EntityCodeBuilder.ClassName + "ById";
                Method_GetByIdAsync_FullName = "Task<" + OutputDtoCodeBuilder.ClassName + "> " + Method_GetByIdAsync_Name
                    + "(" + EntityCodeBuilder.PkType.ToLower() + " id)";
                CodeText.AppendLine(Method_GetByIdAsync_FullName + ";");
                CodeText.AppendLine("");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_CreateAsync)
            {
                Method_CreateAsync_Name = "Create" + EntityCodeBuilder.ClassName ;
                Method_CreateAsync_FullName = "Task<bool> " + Method_CreateAsync_Name
                    + "(" + InputDtoCodeBuilder.ClassName + " input)";
                CodeText.AppendLine(Method_CreateAsync_FullName + ";");
                CodeText.AppendLine("");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_UpdateAsync)
            {
                Method_UpdateAsync_Name = "Update" + EntityCodeBuilder.ClassName;
                Method_UpdateAsync_FullName = "Task<bool> " + Method_UpdateAsync_Name 
                    + "(" + InputDtoCodeBuilder.ClassName + " input)";
                CodeText.AppendLine(Method_UpdateAsync_FullName + ";");
                CodeText.AppendLine("");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_DeleteAsync)
            {
                Method_DeleteAsync_Name = "Delete" + EntityCodeBuilder.ClassName;
                Method_DeleteAsync_FullName = "Task " + Method_DeleteAsync_Name
                    + "(" + InputDtoCodeBuilder.ClassName + " input)";
                CodeText.AppendLine(Method_DeleteAsync_FullName + ";");
                CodeText.AppendLine("");
            }
        }
    }
}
