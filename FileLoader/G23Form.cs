using Microsoft.Office.Interop.Excel;
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
using System.Xml.Serialization;
using System.Xml;
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

                foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                {
                    usersRadDropDownList.Items.Add(listItem["Name"].ToString());
                }

                this.usersRadDropDownList.SelectedIndex = -1;
                this.usersRadDropDownList.Text = "Select User";

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
                btn_Browse.Enabled = false;
                btnUploadFile.Enabled = false;

                openFile.DefaultExt = ".xlsx";
                openFile.Filter = "(.xlsx)|*.xlsx";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    //FileInfo newFile = new FileInfo(openFile.FileName);
                    this.progressIndicatorBrowse.Image = Properties.Resources.ProgressBrowse;

                    txtFilePath.Text = openFile.FileName;
                    //begin eeplus test
                    ExcelPackage pck = new ExcelPackage(openFile.OpenFile());
                    
                    var activeWorkSheet = pck.Workbook.Worksheets["Summary"];


                    var ColumnNameRange = activeWorkSheet.Cells["B3:DC3"];
                    var TotalNameRange = activeWorkSheet.Cells["B4:DC4"];
                    //string firstoutput = activeWorkSheet.Cells["B2"].Value.ToString();
                    //MessageBox.Show(firstoutput);
                    System.Data.DataTable previewDT = new System.Data.DataTable();
                    foreach (var cell in ColumnNameRange)
                    {
                        previewDT.Columns.Add(cell.GetValue<string>(), typeof(string));
                    }

                    
                    DataRow previewDTRow = previewDT.NewRow();
                    int lastColumnIndex = activeWorkSheet.Dimension.End.Column;
                    for (int columnIndex = 2; columnIndex <= lastColumnIndex; columnIndex++)
                    {
                        string columnName = activeWorkSheet.Cells[3,columnIndex].GetValue<string>();
                        previewDTRow[columnName] = activeWorkSheet.Cells[4, columnIndex].GetValue<string>();
                    }
                    previewDT.Rows.Add(previewDTRow);
                    radGrid.DataSource = previewDT;
                    InitializeDataGridView();

                    btn_Browse.Enabled = true;
                    btnUploadFile.Enabled = true;

                    activeWorkSheet.Dispose();
                    pck.Dispose();
                    openFile.Dispose();
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

                var user = usersRadDropDownList.SelectedItem.ToString();
                DateTime date = dateUploaded.Value;


                //test insert section
                ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();
                ListItem oListItem = summaryList.AddItem(listCreationInformation);
                FieldCollection listFields = summaryList.Fields;
                clientContext.Load(listFields, fields => fields.Include(field => field.InternalName, field => field.Title));

                clientContext.ExecuteQuery();

                oListItem["Title"] = user;
                oListItem[summaryList.GetListFieldInternalName("Date", this.OUser)] = date;

                foreach (GridViewRowInfo rowInfo in radGrid.Rows)
                {
                    foreach(GridViewCellInfo cellInfo in rowInfo.Cells)
                    {
                        string columnNameData = cellInfo.ColumnInfo.Name;
                        string valueData = cellInfo.Value.ToString();
                        oListItem[columnNameData] = valueData;
                    }
                }

                /*for (int i = 0; i < fieldMappings.Count; i++)
                {
                    var internalFieldName = summaryList.GetListFieldInternalName(fieldMappings[i].SharePointListField, this.OUser);
                    if (!string.IsNullOrEmpty(internalFieldName))
                        for (int j = 0; j < radGrid.Columns.Count; j++)
                        {
                            if (radGrid.Columns[j].Name == fieldMappings[i].SharePointListField)
                                oListItem[internalFieldName] = radGrid.Columns[j].DistinctValues[0];
                        }
                }*/

                oListItem.Update();
                clientContext.ExecuteQuery();

                lblMessage.Text = "Your file has be uploaded to " + System.Configuration.ConfigurationManager.AppSettings["DocumentLibraryName"] + " Document Library successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);

                btn_Browse.Enabled = true;
                btnUploadFile.Enabled = true;
                this.progressIndicator.Image = null;
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
        /// Parses Field Mapping XML into List of Mapping
        /// </summary>
        /// <returns></returns>
        public static List<Mapping> ParseXML()
        {
            FieldSetMapping fieldsetMapping;
            List<FieldSet> fieldSets = new List<FieldSet>();
            List<Mapping> mappings = new List<Mapping>();

            var serializer = new XmlSerializer(typeof(FieldSetMapping));
            using (var stream = new StreamReader("FieldSetsG23.xml"))
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

        
    }
}
