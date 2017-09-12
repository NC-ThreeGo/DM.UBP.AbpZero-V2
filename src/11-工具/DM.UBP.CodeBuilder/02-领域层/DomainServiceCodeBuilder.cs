using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class DomainServiceCodeBuilder : DomainServiceInterfaceCodeBuilder
    {
        public DomainServiceInterfaceCodeBuilder DomainServiceInterfaceCodeBuilder { get; set; }

        public DomainServiceCodeBuilder(EntityCodeBuilder entityCodeBuilder, 
            DomainServiceInterfaceCodeBuilder domainServiceInterfaceCodeBuilder,
            string functionName)
            : base(entityCodeBuilder, functionName)
        {
            DomainServiceInterfaceCodeBuilder = domainServiceInterfaceCodeBuilder;
        }

        public override void InternalCreateCode()
        {
            this.WriteUsing();
            this.WriteClass();
        }

        private void WriteUsing()
        {
            CodeText.AppendLine("using System.Linq;");
            CodeText.AppendLine("using System.Data.Entity;");
            CodeText.AppendLine("using System.Threading.Tasks;");
            CodeText.AppendLine("using System.Collections.Generic;");
            CodeText.AppendLine("using Abp.Domain.Uow;");
            CodeText.AppendLine("using Abp.Domain.Services;");
            CodeText.AppendLine("using Abp.Domain.Repositories;");
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
            CodeText.AppendLine("public class " + ClassName + " : DomainService, " + DomainServiceInterfaceCodeBuilder.ClassName);
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
            CodeText.AppendLine("   IUnitOfWorkManager unitOfWorkManager, ");
            CodeText.AppendLine("   IRepository<" + EntityCodeBuilder.ClassName + ", "
                + EntityCodeBuilder.PkType.ToLower() + "> " + EntityCodeBuilder.ClassName.ToLower() + "Repository");
            CodeText.AppendLine("   )");
            CodeText.AppendLine("{");
            CodeText.AppendLine("_unitOfWorkManager = unitOfWorkManager;");
            CodeText.AppendLine("_" + EntityCodeBuilder.ClassName.ToLower() + "Repository = " + EntityCodeBuilder.ClassName.ToLower() + "Repository;");
            CodeText.AppendLine("}");
            CodeText.AppendLine("");
        }

        private void WriteProperty()
        {
            CodeText.AppendLine("private readonly IUnitOfWorkManager _unitOfWorkManager;");
            CodeText.AppendLine("");
            CodeText.AppendLine("private readonly IRepository<" + EntityCodeBuilder.ClassName + ", " 
                + EntityCodeBuilder.PkType.ToLower() + "> _" + EntityCodeBuilder.ClassName.ToLower() + "Repository; ");
            CodeText.AppendLine("");
        }

        private void WriteMethod()
        {
            if (DomainServiceInterfaceCodeBuilder.Has_Method_GetAllAsync)
            {
                CodeText.AppendLine("public async " + DomainServiceInterfaceCodeBuilder.Method_GetAllAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("var " + EntityCodeBuilder.ClassPluralName.ToLower() 
                    + " = _" + EntityCodeBuilder.ClassName.ToLower() + "Repository.GetAll().OrderBy(p => p.Id);");
                CodeText.AppendLine("return await " + EntityCodeBuilder.ClassPluralName.ToLower() + ".ToListAsync();");
                CodeText.AppendLine("}");
                CodeText.AppendLine("");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_GetByIdAsync)
            {
                CodeText.AppendLine("public async " + DomainServiceInterfaceCodeBuilder.Method_GetByIdAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("return await _" + EntityCodeBuilder.ClassName.ToLower() + "Repository.GetAsync(id);");
                CodeText.AppendLine("}");
                CodeText.AppendLine("");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_CreateAsync)
            {
                CodeText.AppendLine("public async " + DomainServiceInterfaceCodeBuilder.Method_CreateAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("var entity = await _" + EntityCodeBuilder.ClassName.ToLower() + "Repository.InsertAsync(" + EntityCodeBuilder.ClassName.ToLower() + ");");
                CodeText.AppendLine("return entity != null;");
                CodeText.AppendLine("}");
                CodeText.AppendLine("");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_UpdateAsync)
            {
                CodeText.AppendLine("public async " + DomainServiceInterfaceCodeBuilder.Method_UpdateAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("var entity = await _" + EntityCodeBuilder.ClassName.ToLower() + "Repository.UpdateAsync(" + EntityCodeBuilder.ClassName.ToLower() + ");");
                CodeText.AppendLine("return entity != null;");
                CodeText.AppendLine("}");
                CodeText.AppendLine("");
            }

            if (DomainServiceInterfaceCodeBuilder.Has_Method_DeleteAsync)
            {
                CodeText.AppendLine("public async " + DomainServiceInterfaceCodeBuilder.Method_DeleteAsync_FullName);
                CodeText.AppendLine("{");
                CodeText.AppendLine("await _" + EntityCodeBuilder.ClassName.ToLower() + "Repository.DeleteAsync(" + EntityCodeBuilder.ClassName.ToLower() + ");");
                CodeText.AppendLine("}");
                CodeText.AppendLine("");
            }

        }
    }
}
