using Microsoft.Office.Interop.Excel;
using Microsoft.SharePoint.Client;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml;
using Telerik.WinControls.UI;
using FileLoader.Helpers;

namespace FileLoader
{
    public partial class FileUploaderForm : System.Windows.Forms.Form
    {
        #region Constructor
        public FileUploaderForm(LoggedInUser user)
        {            
            InitializeComponent();
            this.OUser = user;
          
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FileUploaderForm";
            ShowInTaskbar = true;
            ControlBox = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Office 365 File Uploader";

            dateUploaded.Text = System.DateTime.Now.ToShortDateString();
            this.progressIndicator.Image = null;
            this.progressIndicatorBrowse.Image = null;

            //Initialize dynamic combo boxes           
            InitializeDynamicSiteComboBox();
            InitializeDynamicAnalystComboBox();

            //Initialize GridView         
            fieldMappings = ParseXML();
            InitializeDataGridView(fieldMappings);                    

        } 

        #endregion

        #region Properties       
        public LoggedInUser OUser { get; set; }
        public string FileName { get; set; }

        static List<Mapping> fieldMappings = new List<Mapping>();     
    
        #endregion

        #region Initialize Controls

        /// <summary>
        /// Initializes Site Combo box
        /// </summary>
        private void InitializeDynamicSiteComboBox()
        {

            using (var clientContext = new ClientContext(System.Configuration.ConfigurationManager.AppSettings["SiteURL"]))
            {
                SecureString securePassWd = new SecureString();
                foreach (var c in this.OUser.Password.ToCharArray())
                {
                    securePassWd.AppendChar(c);
                }

                clientContext.Credentials = new SharePointOnlineCredentials(this.OUser.UserName, securePassWd);

                Microsoft.SharePoint.Client.List siteList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings["SiteListName"]);

                clientContext.Load(siteList, list => list.DefaultViewUrl);

                CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                Microsoft.SharePoint.Client.ListItemCollection items = siteList.GetItems(query);
                clientContext.Load(items);
                clientContext.ExecuteQuery();
               
                foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                {
                    cbx_Site.Items.Add(listItem["Name"].ToString());
                }

                this.cbx_Site.SelectedIndex = -1;
                this.cbx_Site.Text = "Select Site";   
             
            }

        }

        /// <summary>
        /// Initializes Analyst Combo box
        /// </summary>
        private void InitializeDynamicAnalystComboBox()
        {

            using (var clientContext = new ClientContext(System.Configuration.ConfigurationManager.AppSettings["SiteURL"]))
            {
                SecureString securePassWd = new SecureString();
                foreach (var c in this.OUser.Password.ToCharArray())
                {
                    securePassWd.AppendChar(c);
                }

                clientContext.Credentials = new SharePointOnlineCredentials(this.OUser.UserName, securePassWd);

                Microsoft.SharePoint.Client.List siteList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings["AnalystListName"]);

                clientContext.Load(siteList, list => list.DefaultViewUrl);

                CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                Microsoft.SharePoint.Client.ListItemCollection items = siteList.GetItems(query);
                clientContext.Load(items);
                clientContext.ExecuteQuery();
               
                foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                {
                    cbx_Analyst.Items.Add(listItem["Name"].ToString());
                }

                this.cbx_Analyst.SelectedIndex = -1;
                this.cbx_Analyst.Text = "Select Analyst";

            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_Site_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            using (var clientContext = new ClientContext(System.Configuration.ConfigurationManager.AppSettings["SiteURL"]))
            {
                SecureString securePassWd = new SecureString();
                foreach (var c in this.OUser.Password.ToCharArray())
                {
                    securePassWd.AppendChar(c);
                }

                clientContext.Credentials = new SharePointOnlineCredentials(this.OUser.UserName, securePassWd);

                Microsoft.SharePoint.Client.List siteList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings["AnalystListName"]);

                clientContext.Load(siteList, list => list.DefaultViewUrl);


                CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                Microsoft.SharePoint.Client.ListItemCollection items = siteList.GetItems(query);
                clientContext.Load(items);
                clientContext.ExecuteQuery();
                var site = cbx_Site.SelectedItem.ToString();

                cbx_Analyst.Items.Clear();

                foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                {
                    FieldLookupValue siteLookup = new FieldLookupValue();
                    siteLookup = (FieldLookupValue)listItem["IVnVSite"];
                    if(siteLookup.LookupValue==site)
                        cbx_Analyst.Items.Add(listItem["Name"].ToString());
                }

                this.cbx_Analyst.SelectedIndex = -1;
                this.cbx_Analyst.Text = "Select Analyst";

            }
        }

        /// <summary>
        /// Initializes Data GridView
        /// </summary>
        /// <param name="mappings"></param>
        private void InitializeDataGridView(List<Mapping> mappings)
        {
            radGrid.AutoGenerateColumns = false;      
            radGrid.ShowColumnHeaders = true;            
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Bold);
            //radGrid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            for (int i = 0; i < mappings.Count; i++)
            {

                GridViewTextBoxColumn textBoxColumn = new GridViewTextBoxColumn();
                textBoxColumn.Name = mappings[i].SharePointListField;
                textBoxColumn.HeaderText = mappings[i].ExcelColumnName;
                textBoxColumn.FieldName = mappings[i].RangeName;
                textBoxColumn.MaxLength = 50;
                textBoxColumn.TextAlignment = ContentAlignment.BottomRight;
                textBoxColumn.Width = 250;
                radGrid.MasterTemplate.Columns.Add(textBoxColumn);
                
            }
        }
        
        #endregion

        #region Methods

        OpenFileDialog openFile = new OpenFileDialog();
        private void btn_Browse_Click_1(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                btn_Browse.Enabled = false;
                btnUploadFile.Enabled = false;                    
                    
                openFile.DefaultExt = ".xlsx";
                openFile.Filter = "(.xlsx)|*.xlsx";
                if (openFile.ShowDialog() == DialogResult.OK)
                {

                    //progressBar.Visible = true;
                    //progressBar.Style = ProgressBarStyle.Marquee;
                    //System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(LoadData(excelSheet)));
                    //thread.Start();

                    this.progressIndicatorBrowse.Image = Properties.Resources.ProgressBrowse;

                    txtFilePath.Text = openFile.FileName;
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(txtFilePath.Text.ToString(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    //Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(Convert.ToInt32(ConfigurationManager.AppSettings["SpreadSheetIndex"])); ;
                    //LoadData(excelSheet);

                    List<Name> namedRanges = excelBook.GetNamedRanges();
                    LoadData(namedRanges);

                    //Dispose Excel Objects                  
                    excelBook.Close(0);
                    excelApp.Quit();

                    btn_Browse.Enabled = true;
                    btnUploadFile.Enabled = true;
                    this.progressIndicatorBrowse.Image = null;
                }
            }
            catch (Exception ex)
            {
                this.progressIndicatorBrowse.Image = null;
                lblMessage.Text = "The File you are trying to upload is not an Excel File, please try again." + System.Environment.NewLine + "Exception Details: " + System.Environment.NewLine + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
                btn_Browse.Enabled = true;
                btnUploadFile.Enabled = true;
            }
            

        }

        private void btnUploadFile_Click_1(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                btn_Browse.Enabled = false;
                btnUploadFile.Enabled = false;

                using (var clientContext = new ClientContext(System.Configuration.ConfigurationManager.AppSettings["SiteURL"]))
                {
                    this.progressIndicator.Image = Properties.Resources.ProgressUpload;

                    SecureString securePassWd = new SecureString();
                    foreach (var c in this.OUser.Password.ToCharArray())
                    {
                        securePassWd.AppendChar(c);
                    }
                    clientContext.Credentials = new SharePointOnlineCredentials(this.OUser.UserName, securePassWd); 
      
                    var summaryList = clientContext.Web.Lists.GetByTitle(System.Configuration.ConfigurationManager.AppSettings["SummaryListName"]);
                    var siteList = clientContext.Web.Lists.GetByTitle(System.Configuration.ConfigurationManager.AppSettings["SiteListName"]);
                    var analystList = clientContext.Web.Lists.GetByTitle(System.Configuration.ConfigurationManager.AppSettings["AnalystListName"]);
                    var docLibrary = clientContext.Web.Lists.GetByTitle(System.Configuration.ConfigurationManager.AppSettings["DocumentLibraryName"]);
                  

                    var docitemName = openFile.FileName;  
                    var site = cbx_Site.SelectedItem.ToString();
                    var analyst = cbx_Analyst.SelectedItem.ToString();
                    DateTime date = dateUploaded.Value;
                    StringBuilder temp = new StringBuilder();
                    temp.Append(date.Year.ToString()).Append(date.Month.ToString()).Append(date.Day.ToString()).Append("-").Append(site).Append("-").Append(analyst).Append(".").Append("xlsx");
                    this.FileName = temp.ToString();
                    var fileUrl = string.Empty;

                    using (var fs = new FileStream(docitemName, FileMode.Open))
                    {
                        var fi = new FileInfo(docitemName);

                        clientContext.Load(docLibrary.RootFolder);
                        clientContext.ExecuteQuery();
                        fileUrl = String.Format("{0}/{1}", docLibrary.RootFolder.ServerRelativeUrl, this.FileName);
                        Microsoft.SharePoint.Client.File.SaveBinaryDirect(clientContext, fileUrl, fs, true);
                        clientContext.ExecuteQuery();
                    }

                    var web = clientContext.Web;
                    var file = web.GetFileByServerRelativeUrl(fileUrl);

                    //Get Lookup IDs
                    //Call to Extension method not working, Fix it!
                    //var analystId = analystList.GetLookupFielId("Name", analyst, this.OUser);
                    //var siteId = siteList.GetLookupFielId("Name", site, this.OUser);

                    var analystId = 0;
                    clientContext.Load(analystList, oList => oList.DefaultViewUrl);
                    CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                    Microsoft.SharePoint.Client.ListItemCollection items = analystList.GetItems(query);
                    clientContext.Load(items);
                    clientContext.ExecuteQuery();
                    foreach (ListItem item in items)
                    {
                        if (item["Name"].ToString() == analyst)
                        { 
                            analystId = Convert.ToInt32(item["ID"]);
                            break;
                        }
                    }

                    var siteId = 0;
                    clientContext.Load(siteList, oList => oList.DefaultViewUrl);
                    CamlQuery query2 = CamlQuery.CreateAllItemsQuery(100);
                    Microsoft.SharePoint.Client.ListItemCollection siteItems = siteList.GetItems(query);
                    clientContext.Load(siteItems);
                    clientContext.ExecuteQuery();
                    foreach (ListItem item in siteItems)
                    {
                        if (item["Name"].ToString() == site)
                        { 
                            siteId = Convert.ToInt32(item["ID"]);
                            break;
                        }
                    }                 

                    var fileItemitem = file.ListItemAllFields;
                    fileItemitem["Analyst"] = analystId;
                    fileItemitem["IVnVSite"] = siteId;
                    //fileItemitem["IVVSite"] = siteId;
                    fileItemitem["DateUploaded"] = date;
                    fileItemitem.Update();

                    ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();
                    ListItem oListItem = summaryList.AddItem(listCreationInformation);
                    FieldCollection listFields = summaryList.Fields;
                    clientContext.Load(listFields, fields => fields.Include(field => field.InternalName, field=>field.Title));

                    clientContext.ExecuteQuery();

                    oListItem["Title"] = this.FileName;
                    oListItem[summaryList.GetListFieldInternalName("Site", this.OUser)]=siteId;
                    oListItem[summaryList.GetListFieldInternalName("Analyst", this.OUser)] = analystId;
                    oListItem[summaryList.GetListFieldInternalName("Date", this.OUser)] = date;

                    for (int i = 0; i < fieldMappings.Count; i++)                   
                    {               
                       var internalFieldName=summaryList.GetListFieldInternalName(fieldMappings[i].SharePointListField, this.OUser);
                       if (!string.IsNullOrEmpty(internalFieldName))
                           for (int j = 0; j < radGrid.Columns.Count;j++ )
                           {
                               if (radGrid.Columns[j].Name == fieldMappings[i].SharePointListField)
                                   oListItem[internalFieldName] = radGrid.Columns[j].DistinctValues[0];
                           }                               
                    }

                    oListItem.Update();
                    clientContext.ExecuteQuery();

                    var itemID = oListItem.Id;
                    fileItemitem["Summary"] = itemID;                    
                    fileItemitem.Update();
                    clientContext.ExecuteQuery();
                        
                    lblMessage.Text = "Your file has be uploaded to " + System.Configuration.ConfigurationManager.AppSettings["DocumentLibraryName"] + " Document Library successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);

                    btn_Browse.Enabled =true;
                    btnUploadFile.Enabled = true;
                    this.progressIndicator.Image = null;
                }

               
            }
            catch (Exception ex)
            {
                this.progressIndicator.Image = null;
                lblMessage.Text = "There was errors while uploading your file, please try again." + System.Environment.NewLine + "Exception Details: " + System.Environment.NewLine + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
                btn_Browse.Enabled = true;
                btnUploadFile.Enabled = true;
            }
        }
       
        /// <summary>
        /// Loads Excel Data from Excel Workbook Named Ranges
        /// </summary>
        /// <param name="namedRange"></param>
        public void LoadData(List<Name> namedRange)
        {
            var mappedField = string.Empty;
            string strCellData = "";           
            int colCnt = 0;
            System.Data.DataTable dt = new System.Data.DataTable();

            for (colCnt = 0; colCnt <= namedRange.Count - 1; colCnt++)
            {
                string strColumn = "";               
                strColumn = Convert.ToString(namedRange[colCnt].Name);
                dt.Columns.Add(strColumn, typeof(string));
            }

            string strData = "";
            for (colCnt = 0; colCnt <= namedRange.Count - 1; colCnt++)
            {
                try
                {
                    strCellData = Convert.ToString(namedRange[colCnt].RefersToRange.Value2);
                    strData += strCellData + "|";
                }
                catch (Exception ex)
                {
                    throw new FormatException("Invalid Excel Format Exception " + ex.Message);
                }
            }
            strData = strData.Remove(strData.Length - 1, 1);
            dt.Rows.Add(strData.Split('|'));

            radGrid.DataSource = dt.DefaultView;
        }

        /// <summary>
        /// Loads Excel Data from Excel Sheet - Not being used currently 
        /// </summary>
        /// <param name="sheet"></param>
        public void LoadData(Worksheet sheet)
        {
            Microsoft.Office.Interop.Excel.Range excelRange = sheet.UsedRange;

            string strCellData = "";            
            int rowCnt = 0;
            int colCnt = 0;
            System.Data.DataTable dt = new System.Data.DataTable();

            for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
            {
                string strColumn = "";
                strColumn = (string)(excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                dt.Columns.Add(strColumn, typeof(string));
            }


            for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
            {
                string strData = "";
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    try
                    {
                        strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                        strData += strCellData + "|";
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException("Invalid Excel Format Exception " + ex.Message);
                    }
                }
                strData = strData.Remove(strData.Length - 1, 1);
                dt.Rows.Add(strData.Split('|'));
            }

            radGrid.DataSource = dt.DefaultView;

        }

        /// <summary>
        /// Parses Field Mapping XML into List of Mapping
        /// </summary>
        /// <returns></returns>
        public static List<Mapping> ParseXML()
        {
            FieldSetMapping fieldsetMapping;
            List<FieldSet> fieldSets = new List<FieldSet>();
            List<Mapping> mappings = new List<Mapping>();

            var serializer = new XmlSerializer(typeof(FieldSetMapping));
            using (var stream = new StreamReader("FieldSets.xml"))
            using (var reader = XmlReader.Create(stream))
            {
                fieldsetMapping = (FieldSetMapping)serializer.Deserialize(reader);
            }

            fieldSets = fieldsetMapping.FieldSets;
            foreach (FieldSet f in fieldSets)
            {
                mappings = f.Mappings;
            }
            return mappings;
        }

        #endregion 
        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_Browse.PerformClick();
        }
        private void generateWeeklyMatrixBySiteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }        
        private void generateWeeklyMatrixByCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void generateWeeklyTotalMetricsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

      }

    /// <summary>
    /// Credentials for Authentication
    /// </summary>
    public class LoggedInUser
    {
        public LoggedInUser(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
