using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class AppServiceCodeBuilder : AppServiceInterfaceCodeBuilder
    {
        public AppServiceInterfaceCodeBuilder AppServiceInterfaceCodeBuilder { get; set; }

        public PermissionCodeBuilder PermissionCodeBuilder { get; set; }

        public AppServiceCodeBuilder(EntityCodeBuilder entityCodeBuilder, 
            InputDtoCodeBuilder inputDtoCodeBuilder,
            OutputDtoCodeBuilder outputDtoCodeBuilder,
            DomainServiceInterfaceCodeBuilder domainServiceInterfaceCodeBuilder,
            AppServiceInterfaceCodeBuilder appServiceInterfaceCodeBuilder,
            PermissionCodeBuilder permissionCodeBuilder,
            string className, string functionName)
            : base(entityCodeBuilder, inputDtoCodeBuilder, outputDtoCodeBuilder, 
                  domainServiceInterfaceCodeBuilder, className, functionName)
        {
            AppServiceInterfaceCodeBuilder = appServiceInterfaceCodeBuilder;
            PermissionCodeBuilder = permissionCodeBuilder;
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
            CodeText.AppendLine("using Abp.Auditing;");
            CodeText.AppendLine("using Abp.AutoMapper;");
            CodeText.AppendLine("using Abp.Authorization;");
            CodeText.AppendLine("using Abp.Application.Services.Dto;");
            CodeText.AppendLine("using " + EntityCodeBuilder.FullNameSpace + ";");
            CodeText.AppendLine("using " + DomainServiceInterfaceCodeBuilder.FullNameSpace + ";");
            CodeText.AppendLine("using " + PermissionCodeBuilder.FullNameSpace + ";");
            CodeText.AppendLine("using " + InputDtoCodeBuilder.FullNameSpace + ";");
            CodeText.AppendLine("using System.Linq;");
            CodeText.AppendLine("using System.Linq.Dynamic;");
            CodeText.AppendLine("using DM.UBP.Dto;");
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

            //当前服务所需要的菜单权限
            CodeText.AppendLine("[AbpAuthorize(" + PermissionCodeBuilder.ClassName + "." + PermissionCodeBuilder.MenuPermKey + ")]");

            CodeText.AppendLine("public class " + ClassName + " : " + AppServiceInterfaceCodeBuilder.ClassName);
            CodeText.AppendLine("{");

            this.WriteProperty();
            this.WriteConstructor();
            this.WriteMethod();

            CodeText.AppendLine("}");
            CodeText.AppendLine("}");
        }

        private void WriteConstructor()
        {
            CodeText.AppendLine("public " + ClassName + "(");
            CodeText.AppendLine("   " + DomainServiceInterfaceCodeBuilder.ClassName + " " + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1).ToLower());
            CodeText.AppendLine("   )");
            CodeText.AppendLine("{");
            CodeText.AppendLine("_" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + " = " + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1).ToLower() + "; ");
            CodeText.AppendLine("}");
            CodeText.AppendLine("");
        }

        private void WriteProperty()
        {
            CodeText.AppendLine("private readonly " + DomainServiceInterfaceCodeBuilder.ClassName + " _" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + "; ");
        }

        private void WriteMethod()
        {
            if (DomainServiceInterfaceCodeBuilder.Has_Method_GetAllAsync)
            {
                CodeText.AppendLine("public async " + AppServiceInterfaceCodeBuilder.Method_GetAllAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("var entities = await _" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + "." + DomainServiceInterfaceCodeBuilder.Method_GetAllAsync_Name + "();");
                CodeText.AppendLine("var listDto = entities.MapTo<List<" + OutputDtoCodeBuilder.ClassName + ">>();");
                CodeText.AppendLine("");
                CodeText.AppendLine("return new PagedResultDto<" + OutputDtoCodeBuilder.ClassName + "> (");
                CodeText.AppendLine("listDto.Count,");
                CodeText.AppendLine("listDto");
                CodeText.AppendLine("); ");
                CodeText.AppendLine("}");

                CodeText.AppendLine("public async " + AppServiceInterfaceCodeBuilder.Method_GetPageAllAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("var entities = await _" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + "." + DomainServiceInterfaceCodeBuilder.Method_GetAllAsync_Name + "();");
                CodeText.AppendLine("if (string.IsNullOrEmpty(input.Sorting))");
                CodeText.AppendLine("input.Sorting = \"Id\";");
                CodeText.AppendLine("var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));");
                CodeText.AppendLine("var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));");
                CodeText.AppendLine("var listDto = pageEntities.MapTo<List<" + OutputDtoCodeBuilder.ClassName + ">>();");
                CodeText.AppendLine("");
                CodeText.AppendLine("return new PagedResultDto<" + OutputDtoCodeBuilder.ClassName + "> (");
                CodeText.AppendLine("entities.Count,");
                CodeText.AppendLine("listDto");
                CodeText.AppendLine("); ");
                CodeText.AppendLine("}");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_GetByIdAsync)
            {
                CodeText.AppendLine("public async " + AppServiceInterfaceCodeBuilder.Method_GetByIdAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("var entity = await _" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + "." + DomainServiceInterfaceCodeBuilder.Method_GetByIdAsync_Name + "(id);");
                CodeText.AppendLine("return entity.MapTo<" + OutputDtoCodeBuilder.ClassName + ">();");
                CodeText.AppendLine("}");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_CreateAsync)
            {
                //当前方法所需要的菜单权限
                CodeText.AppendLine("[AbpAuthorize(" + PermissionCodeBuilder.ClassName + "." + PermissionCodeBuilder.PermCreateKey + ")]");
                CodeText.AppendLine("public async " + AppServiceInterfaceCodeBuilder.Method_CreateAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("var entity = input.MapTo<" + EntityCodeBuilder.ClassName + ">();");
                CodeText.AppendLine("return await _" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + "." + DomainServiceInterfaceCodeBuilder.Method_CreateAsync_Name + "(entity);");
                CodeText.AppendLine("}");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_UpdateAsync)
            {
                //当前方法所需要的菜单权限
                CodeText.AppendLine("[AbpAuthorize(" + PermissionCodeBuilder.ClassName + "." + PermissionCodeBuilder.PermEditKey + ")]");
                CodeText.AppendLine("public async " + AppServiceInterfaceCodeBuilder.Method_UpdateAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("var entity = await _" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + "." + DomainServiceInterfaceCodeBuilder.Method_GetByIdAsync_Name + "(input.Id);");
                CodeText.AppendLine("input.MapTo(entity);");
                CodeText.AppendLine("return await _" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + "." + DomainServiceInterfaceCodeBuilder.Method_UpdateAsync_Name + "(entity);");
                CodeText.AppendLine("}");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_DeleteAsync)
            {
                //当前方法所需要的菜单权限
                CodeText.AppendLine("[AbpAuthorize(" + PermissionCodeBuilder.ClassName + "." + PermissionCodeBuilder.PermDeleteKey + ")]");
                CodeText.AppendLine("public async " + AppServiceInterfaceCodeBuilder.Method_DeleteAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("var entity = await _" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + "." + DomainServiceInterfaceCodeBuilder.Method_GetByIdAsync_Name + "(input.Id);");
                CodeText.AppendLine("await _" + DomainServiceInterfaceCodeBuilder.ClassName.Substring(1) + "." + DomainServiceInterfaceCodeBuilder.Method_DeleteAsync_Name + "(entity);");
                CodeText.AppendLine("}");
            }
        }
    }
}
