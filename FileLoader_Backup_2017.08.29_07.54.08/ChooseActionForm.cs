using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileLoader
{
    public partial class ChooseActionForm : Form
    {
        protected string chooseActionPassword { get; set; }
        protected string chooseActionUserName { get; set; }

        public ChooseActionForm(LoggedInUser user)
        {
            InitializeComponent();
            SetupDropdowns();
            chooseActionUserName = user.UserName;
            chooseActionPassword = user.Password;
        }

        


        protected void SetupDropdowns()
        {
            selectActionRadDropDownList.Items.Add("Select An Action");
            //Set default item
            selectActionRadDropDownList.SelectedIndex = 0;
            selectActionRadDropDownList.Items.Add("IV&V");
            selectActionRadDropDownList.Items.Add("IPO G-23");
            

        }

        private void selectionActionButton_Click(object sender, EventArgs e)
        {
            string selectedStringValue = selectActionRadDropDownList.SelectedItem.Text;
            switch (selectedStringValue)
            {
                case "Select An Action":
                    MessageBox.Show("Please Select An Action");
                    break;
                case "IV&V":
                    FileUploaderForm fileUploadForm = new FileUploaderForm(new LoggedInUser(chooseActionUserName, chooseActionPassword));
                    fileUploadForm.ShowDialog();
                    break;
                case "IPO G-23":
                    G23Form ipoForm = new G23Form(new LoggedInUser(chooseActionUserName, chooseActionPassword));
                    ipoForm.ShowDialog();
                    break;
                default:
                    MessageBox.Show("Error has occured");
                    break;

            }
        }
    }
}
