using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileLoader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginDialogForm login = new LoginDialogForm();
            DialogResult Result = login.ShowDialog();
            switch (Result)
            {
                case DialogResult.OK:
                    Application.Run(new ChooseActionForm(new LoggedInUser(login.Username, login.Password)));
                    //Application.Run(new FileUploaderForm(new LoggedInUser(login.Username,login.Password)));
                    break;
                case DialogResult.Cancel:
                    Application.Exit();
                    break;
            }

        }
    }
}
