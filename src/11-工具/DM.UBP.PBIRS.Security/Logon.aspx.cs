#region
// Copyright (c) 2016 Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License (MIT)
/*============================================================================
  File:     Logon.aspx.cs
  Summary:  The code-behind for a logon page that supports Forms
            Authentication in a custom security extension    
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

using DM.Common.Security;
using System;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Security;

namespace DM.UBP.PBIRS.Security
{
    public class Logon : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TextBox txtPwd;
        protected System.Web.UI.WebControls.TextBox txtUserName;
        protected System.Web.UI.WebControls.Button btnLogin;
        protected System.Web.UI.WebControls.Label lblMessage;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    //ReturnUrl=/PBIReportServer/localredirect?url=/PBIReportS/powerbi/SysLog?rs:embed=true&token=asdfdasfasdfadsfasdf
                    //  需要先解析出url参数，再从url参数中解析出token
                    Uri returnUri = new Uri("http://localhost" + returnUrl);
                    string url = HttpUtility.ParseQueryString(returnUri.Query).Get("url");
                    Uri tokenUri = new Uri("http://localhost" + url);
                    string token = HttpUtility.ParseQueryString(tokenUri.Query).Get("token");

                    if (!String.IsNullOrEmpty(token))
                    {
                        string username = CheckJwtToken(token);
                        if (!String.IsNullOrEmpty(username))
                        {
                            FormsAuthentication.RedirectFromLoginPage(
                              username, false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 验证URL中的Jwt Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>返回Jwt Token中所携带的UserName</returns>
        private string CheckJwtToken(string token)
        {
            string username = null;

            JwtTokenUtils jwtTokenUtils = new JwtTokenUtils();
            username = jwtTokenUtils.ValidateJwtToken(token, ConfigurationManager.AppSettings["Jwt_Audience_PBIRS"] ?? "pbirs");

            return username;
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool passwordVerified = false;
            try
            {
                passwordVerified =
                   AuthenticationUtilities.VerifyPassword(txtUserName.Text, txtPwd.Text);
                if (passwordVerified)
                {
                    FormsAuthentication.RedirectFromLoginPage(
                       txtUserName.Text, false);
                }
                else
                {
                    //Response.Redirect("logon.aspx");
                    lblMessage.Text = string.Format(CultureInfo.InvariantCulture, Resource.InvalidUsernamePassword);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = string.Format(CultureInfo.InvariantCulture, ex.Message);
                return;
            }
        }
    }
}
