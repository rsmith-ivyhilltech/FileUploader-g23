
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Security;
using System.Text;
using System.Windows.Forms;
using FileLoader.Helpers;
using System.Configuration;
using System.IO;
using Telerik.WinControls.UI;
using OfficeOpenXml;


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

            dateUploaded.Text = System.DateTime.Now.ToShortDateString();
            this.progressIndicator.Image = null;
            this.progressIndicatorBrowse.Image = null;

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

                usersRadDropDownList.Items.Add("Select User");
                foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                {
                    //Yes = True, No = False
                    if (listItem["Active"].ToString() == "True")
                    {
                        usersRadDropDownList.Items.Add(listItem["Name"].ToString());
                    }
                }
                //Set default item
                usersRadDropDownList.SelectedIndex = 0;

            }

        }

        /// <summary>
        /// Initializes Data GridView
        /// </summary>
        /// <param name="mappings"></param>
        private void InitializeDataGridView()
        {
            radGrid.AutoGenerateColumns = false;
            radGrid.ShowColumnHeaders = true;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Bold);
            radGrid.MasterTemplate.BestFitColumns();
        }


        #endregion

        OpenFileDialog openFile = new OpenFileDialog();
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                //old logic
                //btn_Browse.Enabled = false;
                //btnUploadFile.Enabled = false;

                openFile.DefaultExt = ".xlsx";
                openFile.Filter = "(.xlsx)|*.xlsx";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    this.progressIndicatorBrowse.Image = Properties.Resources.ProgressBrowse;

                    txtFilePath.Text = openFile.FileName;
                    var fileTest = new FileInfo(openFile.FileName);
                    //begin eeplus read
                    using (var pck = new ExcelPackage(fileTest))
                    {
                        var activeWorkSheet = pck.Workbook.Worksheets["Summary"];

                        string startColumn = ConfigurationManager.AppSettings["RangeStart"];
                        string endColumn = ConfigurationManager.AppSettings["RangeEnd"];
                        string createColumnNameRange = startColumn + "3:" + endColumn + "3";
                        string createTotalNameRange = startColumn + "4:" + endColumn + "4";
                        var ColumnNameRange = activeWorkSheet.Cells[createColumnNameRange];
                        var TotalNameRange = activeWorkSheet.Cells[createTotalNameRange];
                        //old non dynamic range
                        //var ColumnNameRange = activeWorkSheet.Cells["B3:DC3"];
                        //var TotalNameRange = activeWorkSheet.Cells["B4:DC4"];

                        DataTable previewDT = new DataTable();
                        foreach (var cell in ColumnNameRange)
                        {
                            previewDT.Columns.Add(cell.GetValue<string>(), typeof(string));
                        }


                        DataRow previewDTRow = previewDT.NewRow();
                        int lastColumnIndex = activeWorkSheet.Dimension.End.Column;
                        for (int columnIndex = 2; columnIndex <= lastColumnIndex; columnIndex++)
                        {
                            string columnName = activeWorkSheet.Cells[3, columnIndex].GetValue<string>();
                            previewDTRow[columnName] = activeWorkSheet.Cells[4, columnIndex].GetValue<string>();
                        }
                        previewDT.Rows.Add(previewDTRow);
                        radGrid.DataSource = previewDT;
                        InitializeDataGridView();

                        btn_Browse.Enabled = true;
                        btnUploadFile.Enabled = true;
                    }

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


        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text == string.Empty)
            {
                MessageBox.Show("No filed selected to load.");
            }
            else if(usersRadDropDownList.SelectedItem.Text == "Select User")
            {
                MessageBox.Show("Select A User.");
            }
            else if (dateUploaded.Text == string.Empty)
            {
                MessageBox.Show("Select A Date.");
            }
            else
            {
                lblMessage.Text = "";
                btn_Browse.Enabled = false;
                btnUploadFile.Enabled = false;

                using (var clientContext = new ClientContext(System.Configuration.ConfigurationManager.AppSettings["G23SiteURL"]))
                {
                    this.progressIndicator.Image = Properties.Resources.ProgressUpload;

                    SecureString securePassWd = new SecureString();
                    foreach (var c in this.OUser.Password.ToCharArray())
                    {
                        securePassWd.AppendChar(c);
                    }
                    clientContext.Credentials = new SharePointOnlineCredentials(this.OUser.UserName, securePassWd);

                    var summaryList = clientContext.Web.Lists.GetByTitle(System.Configuration.ConfigurationManager.AppSettings["G23SummaryListName"]);
                    var docLibrary = clientContext.Web.Lists.GetByTitle(System.Configuration.ConfigurationManager.AppSettings["G23DocumentLibraryName"]);

                    var docitemName = openFile.FileName;
                    var user = usersRadDropDownList.SelectedItem.ToString();
                    DateTime date = dateUploaded.Value;

                    //Insert DocumentLibrary
                    StringBuilder temp = new StringBuilder();
                    temp.Append(date.Year.ToString()).Append(date.Month.ToString()).Append(date.Day.ToString()).Append("-").Append(user).Append(".").Append("xlsx");
                    this.FileName = temp.ToString();
                    string fileWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(this.FileName);
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

                    var fileItemitem = file.ListItemAllFields;
                    fileItemitem["Title"] = fileWithoutExtension;
                    fileItemitem.Update();

                    //gets lookup ID of the Document that was just uploaded
                    //This lookup ID is passed into the Summary Table
                    var DocumentTitleId = 0;
                    clientContext.Load(docLibrary, oList => oList.DefaultViewUrl);
                    CamlQuery query = CamlQuery.CreateAllItemsQuery();
                    Microsoft.SharePoint.Client.ListItemCollection items = docLibrary.GetItems(query);
                    clientContext.Load(items);
                    clientContext.ExecuteQuery();
                    foreach (ListItem item in items)
                    {
                        if (item["Title"].ToString() == fileWithoutExtension)
                        {
                            DocumentTitleId = Convert.ToInt32(item["ID"]);
                            break;
                        }
                    }


                    //Insert Summary
                    ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();
                    ListItem oListItem = summaryList.AddItem(listCreationInformation);
                    FieldCollection listFields = summaryList.Fields;
                    clientContext.Load(listFields, fields => fields.Include(field => field.InternalName, field => field.Title));

                    clientContext.ExecuteQuery();

                    oListItem["Title"] = user;
                    oListItem[summaryList.GetListFieldInternalName("Date", this.OUser)] = date;
                    oListItem["SourceFile"] = DocumentTitleId;

                    foreach (GridViewRowInfo rowInfo in radGrid.Rows)
                    {
                        foreach (GridViewCellInfo cellInfo in rowInfo.Cells)
                        {
                            string columnNameData = cellInfo.ColumnInfo.Name;
                            string valueData = cellInfo.Value.ToString();
                            oListItem[columnNameData] = valueData;
                        }
                    }

                    oListItem.Update();
                    clientContext.ExecuteQuery();

                    lblMessage.Text = "Your file has be uploaded to the Source File Document Library successfully.";
                    //lblMessage.Text = "Your file has be uploaded to " + System.Configuration.ConfigurationManager.AppSettings["G23DocumentLibraryName"] + " Document Library successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);

                    btn_Browse.Enabled = true;
                    btnUploadFile.Enabled = true;
                    this.progressIndicator.Image = null;
                }
            }
            
        }
        
        
                
    }
}
