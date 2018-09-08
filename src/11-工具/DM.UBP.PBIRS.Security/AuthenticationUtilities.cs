#region
// Copyright (c) 2016 Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License (MIT)
/*============================================================================
   File:      AuthenticationStore.cs

  Summary:  Demonstrates how to create and maintain a user store for
            a security extension. 
--------------------------------------------------------------------
  This file is part of Microsoft SQL Server Code Samples.
    
 This source code is intended only as a supplement to Microsoft
 Development Tools and/or on-line documentation. See these other
 materials for detailed information regarding Microsoft code 
 samples.

 THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF 
 ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
 THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 PARTICULAR PURPOSE.
===========================================================================*/
#endregion

using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Management;
using System.Xml;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace DM.UBP.PBIRS.Security
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal sealed class AuthenticationUtilities
    {
        // The path of any item in the report server database 
        // has a maximum character length of 260
        private const int MaxItemPathLength = 260;
        private const string wmiNamespace = @"\root\Microsoft\SqlServer\ReportServer\{0}\v11";
        private const string rsAsmx = @"/ReportService2010.asmx";


        // Method that indicates whether 
        // the supplied username and password are valid
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
        internal static bool VerifyPassword(string userName,
           string password)
        {
            bool passwordMatch = false;

            string baseUrl = ConfigurationManager.AppSettings["CheckUser_WebApi_BaseUrl"];
            string apiPath = ConfigurationManager.AppSettings["CheckUser_WebApi_Path"];

            using (var client = new HttpClient() { BaseAddress = new Uri(baseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                //使用FormUrlEncodedContent做HttpContent
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                  {"tenancyName", ""},
                  {"usernameOrEmailAddress", userName},
                  {"password", password}
                 });

                //await异步等待回应
                var response = client.PostAsync(apiPath, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultJson = response.Content.ReadAsStringAsync();

                    //实例化JavaScriptSerializer类的新实例
                    JavaScriptSerializer jss = new JavaScriptSerializer();

                    Dictionary<string, object> dictResult = (Dictionary<string, object>)jss.DeserializeObject(resultJson.Result);
                    passwordMatch = (bool)dictResult["success"];
                }
            }

            return passwordMatch;
        }

        // Method to verify that the user name does not contain any
        // illegal characters. If My Reports is enabled, illegal characters
        // will invalidate the paths created in the \Users folder. Usernames
        // should not contain the characters captured by this method.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        internal static bool ValidateUserName(string input)
        {
            Regex r = new Regex(@"\A(\..*)?[^\. ](.*[^ ])?\z(?<=\A[^/;\?:@&=\+\$,\\\*<>\|""]{0," +
               MaxItemPathLength.ToString() + @"}\z)",
               RegexOptions.Compiled | RegexOptions.ExplicitCapture);
            bool isValid = r.IsMatch(input);
            return isValid;
        }

        //Method to get the report server url using WMI
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        internal static string GetReportServerUrl(string machineName, string instanceName)
        {
            string reportServerVirtualDirectory = String.Empty;
            string fullWmiNamespace = @"\\" + machineName + string.Format(wmiNamespace, instanceName);

            ManagementScope scope = null;

            ConnectionOptions connOptions = new ConnectionOptions();
            connOptions.Authentication = AuthenticationLevel.PacketPrivacy;

            //Get management scope
            try
            {
                scope = new ManagementScope(fullWmiNamespace, connOptions);
                scope.Connect();

                //Get management class
                ManagementPath path = new ManagementPath("MSReportServer_Instance");
                ObjectGetOptions options = new ObjectGetOptions();
                ManagementClass serverClass = new ManagementClass(scope, path, options);

                serverClass.Get();

                if (serverClass == null)
                    throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    Resource.WMIClassError));

                //Get instances
                ManagementObjectCollection instances = serverClass.GetInstances();

                foreach (ManagementObject instance in instances)
                {
                    instance.Get();
                    //We're doing this comparison just to make sure we're validating the right instance.
                    //This comparison is more reliable as we do the comparison on the instance name rather
                    //than on any other property.
                    if (instanceName.ToUpper().Equals("RS_" + instance.GetPropertyValue("InstanceName").ToString().ToUpper()))
                    {
                        ManagementBaseObject outParams = (ManagementBaseObject)instance.InvokeMethod("GetReportServerUrls",
                        null, null);

                        string[] appNames = (string[])outParams["ApplicationName"];
                        string[] urls = (string[])outParams["URLs"];

                        for (int i = 0; i < appNames.Length; i++)
                        {
                            if (appNames[i] == "ReportServerWebService")
                            {
                                reportServerVirtualDirectory = urls[i];
                                //Since we only look for ReportServer URL we can safely break here as it would save one more iteration.
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                Resource.RSUrlError + ex.Message), ex);
            }

            if (reportServerVirtualDirectory == string.Empty)
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                Resource.MissingUrlReservation));

            return reportServerVirtualDirectory + rsAsmx;

        }
    }
}
