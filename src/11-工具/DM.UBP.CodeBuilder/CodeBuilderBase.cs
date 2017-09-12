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
        /// <summary>
        /// 修改文件时，插入代码的位置。
        /// </summary>
        public const string INSERT_POSITION = "//@@Insert Position";

        /// <summary>
        /// 指定文件的后缀名
        /// </summary>
        public virtual string Suffix
        {
            get
            {
                return ".cs";
            }
        }

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

        public string FunctionName { get; set; }

        public string FileName { get; set; }

        public string ClassName { get; set; }
        public string ClassComments { get; set; }

        public string FullNameSpace { get; set; }

        /// <summary>
        /// 当文件存在时，是否覆盖。
        ///     默认覆盖，但UbpDbContext.XXX.cs和AppPermissions_XXX.cs文件需要追加。
        /// </summary>
        public virtual bool IsOverrideWrite
        {
            get
            {
                return true;
            }
        }

        public virtual bool AutoMakeDir
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 相对路径：当前代码相对于代码根目录的相对路径
        /// </summary>
        public virtual string SubCodePath { get; set; }
        //public virtual string ModulePath { get; set; }
        //public virtual string SubModulePath { get; set; }
        //public virtual string FunctionPath { get; set; }

        public bool FileExists
        {
            get
            {
                return File.Exists(RootCodePath + SubCodePath
                    + ModuleName + @"\" + (String.IsNullOrEmpty(SubModuleName) ? "" : SubModuleName + @"\")
                    + (String.IsNullOrEmpty(FunctionName) ? "" : FunctionName + @"\")
                    + FileName + Suffix);
            }
        }

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
            StringBuilder copyright = new StringBuilder();
            copyright.AppendLine("//------------------------------------------------------------");
            copyright.AppendLine("// All Rights Reserved , Copyright (C)  ");
            copyright.AppendLine("// 版本：" + Version);
            copyright.AppendLine("/// <author>");
            copyright.AppendLine("///		<name>" + this.Author + "</name>");
            copyright.AppendLine("///		<date>" + this.DateCreated + "</date>");
            copyright.AppendLine("/// </author>");
            copyright.AppendLine("//------------------------------------------------------------");
            copyright.AppendLine("");

            this.CodeText.Insert(0, copyright);
        }

        public void CreateCode()
        {
            this.InternalCreateCode();

            this.Save();
        }

        public virtual void InternalCreateCode()
        {

        }

        public void Save()
        {
            if (AutoMakeDir)
            {
                if (!Directory.Exists(RootCodePath + SubCodePath + ModuleName))
                {
                    Directory.CreateDirectory(RootCodePath + SubCodePath + ModuleName);
                }
                if (!String.IsNullOrEmpty(SubModuleName) && (!Directory.Exists(RootCodePath + SubCodePath
                    + ModuleName + @"\" + SubModuleName)))
                {
                    Directory.CreateDirectory(RootCodePath + SubCodePath + ModuleName + @"\" + SubModuleName);
                }
                if (!String.IsNullOrEmpty(FunctionName) && (!Directory.Exists(RootCodePath + SubCodePath
                    + ModuleName + @"\" + SubModuleName + @"\" + FunctionName)))
                {
                    Directory.CreateDirectory(RootCodePath + SubCodePath + ModuleName + @"\" + SubModuleName + @"\" + FunctionName);
                }
            }

            //当为覆盖模式时，且文件已存在，则先备份老文件。
            if (IsOverrideWrite)
            {
                if (FileExists)
                    BackupOldFile();

                SaveForNewFile();
            }
            else
            {
                //当为追加模式时，且文件不存在，则新建文件。
                if (!FileExists)
                {
                    SaveForNewFile();
                }
                else
                    AddForOldFile();
            }

        }

        private void BackupOldFile()
        {
            File.Move(RootCodePath + SubCodePath + ModuleName + @"\"
                + (String.IsNullOrEmpty(SubModuleName) ? "" : SubModuleName + @"\")
                + (String.IsNullOrEmpty(FunctionName) ? "" : FunctionName + @"\")
                + FileName + Suffix,
                RootCodePath + SubCodePath + ModuleName + @"\"
                + (String.IsNullOrEmpty(SubModuleName) ? "" : SubModuleName + @"\")
                + (String.IsNullOrEmpty(FunctionName) ? "" : FunctionName + @"\")
                + FileName + Suffix + "01");
        }

        private void SaveForNewFile()
        {
            this.WriteCodeCopyright();

            using (StreamWriter writer_CS = new StreamWriter(RootCodePath + SubCodePath
                                + ModuleName + @"\"
                                + (String.IsNullOrEmpty(SubModuleName) ? "" : SubModuleName + @"\")
                                + (String.IsNullOrEmpty(FunctionName) ? "" : FunctionName + @"\")
                                + FileName + Suffix, false, System.Text.Encoding.GetEncoding("UTF-8")))
            {
                writer_CS.Write(this.CodeText);
                writer_CS.Close();
            }
        }

        private void AddForOldFile()
        {
            StringBuilder output = new StringBuilder();
            using (StreamReader read_CS = new StreamReader(RootCodePath + SubCodePath
                                + ModuleName + @"\"
                                + (String.IsNullOrEmpty(SubModuleName) ? "" : SubModuleName + @"\")
                                + (String.IsNullOrEmpty(FunctionName) ? "" : FunctionName + @"\")
                                + FileName + Suffix, System.Text.Encoding.GetEncoding("UTF-8")))
            {

                /**////设置当前流的起始位置为文件流的起始点
                read_CS.BaseStream.Seek(0, SeekOrigin.Begin);

                /**////读取文件
                while (read_CS.Peek() > -1)
                {
                    /**////取文件的一行内容并换行
                    output.AppendLine(read_CS.ReadLine());
                }

                read_CS.Close();
            }

            using (StreamWriter writer_CS = new StreamWriter(RootCodePath + SubCodePath
                                + ModuleName + @"\"
                                + (String.IsNullOrEmpty(SubModuleName) ? "" : SubModuleName + @"\")
                                + (String.IsNullOrEmpty(FunctionName) ? "" : FunctionName + @"\")
                                + FileName + Suffix, false, System.Text.Encoding.GetEncoding("UTF-8")))
            {
                output.Replace(INSERT_POSITION, this.CodeText.ToString());
                writer_CS.Write(output);
                writer_CS.Close();
            }
        }
    }
}
