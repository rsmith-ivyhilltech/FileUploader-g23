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
            //this.progressIndicator.Image = null;
            this.progressIndicatorBrowse.Image = null;

            InitializeDynamicUserComboBox();

            //Initialize GridView         
            fieldMappings = ParseXML();
            InitializeDataGridView(fieldMappings);
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
