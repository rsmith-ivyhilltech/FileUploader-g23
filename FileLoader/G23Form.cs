using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileLoader.Helpers;
using System.Configuration;


namespace FileLoader
{
    public partial class G23Form : System.Windows.Forms.Form
    {
        #region Properties       
        public LoggedInUser OUser { get; set; }
        public string FileName { get; set; }

        static List<Mapping> fieldMappings = new List<Mapping>();

        #endregion


        public G23Form(LoggedInUser user)
        {
            InitializeComponent();
            this.OUser = user;

            InitializeDynamicUserComboBox();
        }


        #region Initialize Controls
        /// <summary>
        /// Initializes Analyst Combo box
        /// </summary>
        private void InitializeDynamicUserComboBox()
        {

            using (var clientContext = new ClientContext(System.Configuration.ConfigurationManager.AppSettings["G23SiteURL"]))
            {
                SecureString securePassWd = new SecureString();
                foreach (var c in this.OUser.Password.ToCharArray())
                {
                    securePassWd.AppendChar(c);
                }

                clientContext.Credentials = new SharePointOnlineCredentials(this.OUser.UserName, securePassWd);

                Microsoft.SharePoint.Client.List siteList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings["G23UserListName"]);

                clientContext.Load(siteList, list => list.DefaultViewUrl);

                CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                Microsoft.SharePoint.Client.ListItemCollection items = siteList.GetItems(query);
                clientContext.Load(items);
                clientContext.ExecuteQuery();

                foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                {
                    usersRadDropDownList.Items.Add(listItem["Name"].ToString());
                }

                this.usersRadDropDownList.SelectedIndex = -1;
                this.usersRadDropDownList.Text = "Select User";

            }

        }
        #endregion
    }
}
