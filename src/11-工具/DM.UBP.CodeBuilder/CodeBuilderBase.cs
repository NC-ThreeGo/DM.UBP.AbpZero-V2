using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class CodeBuilderBase
    {
        public const string Version = "1.0";
        public StringBuilder CodeText = new StringBuilder();
        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 代码的根路径
        /// </summary>
        public string RootCodePath;

        /// <summary>
        /// 根命名空间，默认是：DM.UBP
        /// </summary>
        public string RootNameSpace = "DM.UBP";

        /// <summary>
        /// 当前代码分层的命名空间，例如：Domain.Entity
        ///                               Domain.Service
        /// </summary>
        public string SubNameSpace;

        public string ModuleName { get; set; }

        public string SubModuleName { get; set; }

        public string FileName { get; set; }

        public string ClassName { get; set; }
        public string ClassComments { get; set; }

        /// <summary>
        /// 相对路径：当前代码相对于代码根目录的相对路径
        /// </summary>
        public virtual string SubCodePath { get; set; }

        public CodeBuilderBase()
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CodeRootPath"]))
            {
                RootCodePath = ConfigurationManager.AppSettings["CodeRootPath"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["RootNameSpace"]))
            {
                RootNameSpace = ConfigurationManager.AppSettings["RootNameSpace"];
            }
        }

        protected void WriteCodeCopyright()
        {
            this.CodeText = new StringBuilder();
            this.CodeText.AppendLine("//------------------------------------------------------------");
            this.CodeText.AppendLine("// All Rights Reserved , Copyright (C)  ");
            this.CodeText.AppendLine("// 版本：" + Version);
            this.CodeText.AppendLine("/// <author>");
            this.CodeText.AppendLine("///		<name>" + this.Author + "</name>");
            this.CodeText.AppendLine("///		<date>" + this.DateCreated + "</date>");
            this.CodeText.AppendLine("/// </author>");
            this.CodeText.AppendLine("//------------------------------------------------------------");
            this.CodeText.AppendLine("");
        }

        public void CreateCode()
        {
            this.WriteCodeCopyright();
            this.InternalCreateCode();

            this.Save();
        }

        public virtual void InternalCreateCode()
        {

        }

        public void Save()
        {
            StreamWriter writer_CS = new StreamWriter(RootCodePath + SubCodePath 
                + ModuleName + @"\" + (String.IsNullOrEmpty(SubModuleName) ? "" : SubModuleName + @"\")  
                + FileName + ".cs", false, System.Text.Encoding.GetEncoding("UTF-8"));
            writer_CS.Write(this.CodeText);
            writer_CS.Close();
        }
    }
}
