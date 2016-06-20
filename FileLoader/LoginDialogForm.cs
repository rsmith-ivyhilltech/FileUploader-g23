using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Security;
using Microsoft.SharePoint.Client;
using System.Net.Mail;

namespace FileLoader
{
    public partial class LoginDialogForm : System.Windows.Forms.Form
    {

        #region Constructor
        public LoginDialogForm()
        {
            InitializeComponent();

            AcceptButton = this.btnLogin;
            CancelButton = this.btnCancel;
            ControlBox = true;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "LoginForm";
            ShowInTaskbar = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Sign in to Office 365 ";
            LogoPictureBox.TabStop = false;
            LogoPictureBox.Width = 160;
            LogoPictureBox.Height = 40;

            this.txtUserName.NullText = "someone@example.com";
            this.txtPassword.NullText = "Password";

            DialogResult = System.Windows.Forms.DialogResult.OK;
            btnLogin.Click += new System.EventHandler(this.btnLogin_Click_1);
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);

        } 
        #endregion

        #region Properties
       

        public string Username
        {
            get { return txtUserName.Text; }
        }
        public string Password
        {
            get { return txtPassword.Text; }
        }
        
        #endregion

        #region Methods
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (!ValidatePassword())
            {
                txtUserName.Clear();
                txtPassword.Clear();
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            txtUserName.Clear();
            txtPassword.Clear();
            this.Close();
        }
        private bool ValidateUsername()
        {
            if (String.IsNullOrEmpty(txtUserName.Text))
                return false;
            else if (!IsValidEmail(txtUserName.Text))
                return false;
            else
                return true;
        }
        private bool IsValidEmail(string userName)
        {
            try
            {
                MailAddress test = new MailAddress(userName);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private bool ValidatePassword()
        {
            if (!ValidateUsername())
            {
                errorMessage.Text = "Wrong User ID or Password, Please enter your Office 365 User ID and Password";
                errorMessage.ForeColor = System.Drawing.Color.Red;
                errorMessage.Font = new Font("Microsoft Sans Serif", 10);
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    errorMessage.Text = "Wrong User ID or Password, Please enter your Office 365 User ID and Password";
                    errorMessage.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
                else
                {
                    try
                    {
                        System.Uri Office365URL = new Uri(ConfigurationManager.AppSettings["SiteURL"]);
                        SecureString pass = new SecureString();
                        foreach (char c in txtPassword.Text) pass.AppendChar(c);

                        SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(txtUserName.Text, pass);
                        if (!string.IsNullOrEmpty(credentials.GetAuthenticationCookie(Office365URL)))
                        {
                            return true;
                        }
                        else
                        {
                            errorMessage.Text = "Wrong User ID or Password, Please enter your Office 365 User ID and Password";
                            errorMessage.ForeColor = System.Drawing.Color.Red;
                            return false;
                        }
                    }
                    catch (FormatException)
                    {
                        errorMessage.Text = "Wrong User ID or Password, Please enter your Office 365 User ID and Password";
                        errorMessage.ForeColor = System.Drawing.Color.Red;
                        return false;

                    }
                }
            }
        } 
        #endregion

     
    }
}
