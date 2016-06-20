namespace FileLoader
{
    public partial class FileUploaderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileUploaderForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateWeeklyMatrixBySiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateWeeklyMatrixByCompanyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateWeeklyTotalMetricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblGridHeader = new System.Windows.Forms.Label();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblAnalyst = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.radGrid = new Telerik.WinControls.UI.RadGridView();
            this.dateUploaded = new Telerik.WinControls.UI.RadDateTimePicker();
            this.btnUploadFile = new System.Windows.Forms.Button();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.txtFilePath = new Telerik.WinControls.UI.RadTextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.cbx_Site = new Telerik.WinControls.UI.RadDropDownList();
            this.cbx_Analyst = new Telerik.WinControls.UI.RadDropDownList();
            this.progressIndicatorBrowse = new System.Windows.Forms.PictureBox();
            this.progressIndicator = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGrid.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateUploaded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_Site)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_Analyst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressIndicatorBrowse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressIndicator)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1192, 37);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadFileToolStripMenuItem,
            this.generateWeeklyMatrixBySiteToolStripMenuItem,
            this.generateWeeklyMatrixByCompanyToolStripMenuItem,
            this.generateWeeklyTotalMetricsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Calibri", 12F);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(60, 33);
            this.toolStripMenuItem1.Text = "File";
            // 
            // uploadFileToolStripMenuItem
            // 
            this.uploadFileToolStripMenuItem.Name = "uploadFileToolStripMenuItem";
            this.uploadFileToolStripMenuItem.Size = new System.Drawing.Size(475, 34);
            this.uploadFileToolStripMenuItem.Text = "Upload File";
            this.uploadFileToolStripMenuItem.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // generateWeeklyMatrixBySiteToolStripMenuItem
            // 
            this.generateWeeklyMatrixBySiteToolStripMenuItem.Name = "generateWeeklyMatrixBySiteToolStripMenuItem";
            this.generateWeeklyMatrixBySiteToolStripMenuItem.Size = new System.Drawing.Size(475, 34);
            this.generateWeeklyMatrixBySiteToolStripMenuItem.Text = "Generate Weekly Metrics by Site";
            this.generateWeeklyMatrixBySiteToolStripMenuItem.Click += new System.EventHandler(this.generateWeeklyMatrixBySiteToolStripMenuItem_Click);
            // 
            // generateWeeklyMatrixByCompanyToolStripMenuItem
            // 
            this.generateWeeklyMatrixByCompanyToolStripMenuItem.Name = "generateWeeklyMatrixByCompanyToolStripMenuItem";
            this.generateWeeklyMatrixByCompanyToolStripMenuItem.Size = new System.Drawing.Size(475, 34);
            this.generateWeeklyMatrixByCompanyToolStripMenuItem.Text = "Generate Weekly Metrics by Company";
            this.generateWeeklyMatrixByCompanyToolStripMenuItem.Click += new System.EventHandler(this.generateWeeklyMatrixByCompanyToolStripMenuItem_Click);
            // 
            // generateWeeklyTotalMetricsToolStripMenuItem
            // 
            this.generateWeeklyTotalMetricsToolStripMenuItem.Name = "generateWeeklyTotalMetricsToolStripMenuItem";
            this.generateWeeklyTotalMetricsToolStripMenuItem.Size = new System.Drawing.Size(475, 34);
            this.generateWeeklyTotalMetricsToolStripMenuItem.Text = "Generate Weekly Total Metrics";
            this.generateWeeklyTotalMetricsToolStripMenuItem.Click += new System.EventHandler(this.generateWeeklyTotalMetricsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(475, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lblGridHeader
            // 
            this.lblGridHeader.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridHeader.Location = new System.Drawing.Point(330, 126);
            this.lblGridHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGridHeader.Name = "lblGridHeader";
            this.lblGridHeader.Size = new System.Drawing.Size(183, 23);
            this.lblGridHeader.TabIndex = 5;
            this.lblGridHeader.Text = "Review Uploaded Data";
            // 
            // lblSite
            // 
            this.lblSite.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSite.Location = new System.Drawing.Point(70, 380);
            this.lblSite.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(100, 30);
            this.lblSite.TabIndex = 6;
            this.lblSite.Text = "IV and V Site";
            // 
            // lblAnalyst
            // 
            this.lblAnalyst.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnalyst.Location = new System.Drawing.Point(70, 440);
            this.lblAnalyst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAnalyst.Name = "lblAnalyst";
            this.lblAnalyst.Size = new System.Drawing.Size(100, 30);
            this.lblAnalyst.TabIndex = 8;
            this.lblAnalyst.Text = "Analyst Name";
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(70, 500);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(100, 30);
            this.lblDate.TabIndex = 10;
            this.lblDate.Text = "Date Uploaded";
            // 
            // radGrid
            // 
            this.radGrid.Font = new System.Drawing.Font("Calibri", 10F);
            this.radGrid.Location = new System.Drawing.Point(70, 180);
            this.radGrid.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            this.radGrid.MasterTemplate.AllowAddNewRow = false;
            this.radGrid.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGrid.Name = "radGrid";
            this.radGrid.ReadOnly = true;
            this.radGrid.Size = new System.Drawing.Size(1000, 150);
            this.radGrid.TabIndex = 14;
            this.radGrid.Text = "Data Grid";
            // 
            // dateUploaded
            // 
            this.dateUploaded.AutoSize = false;
            this.dateUploaded.Font = new System.Drawing.Font("Calibri", 10F);
            this.dateUploaded.Location = new System.Drawing.Point(255, 490);
            this.dateUploaded.Margin = new System.Windows.Forms.Padding(4);
            this.dateUploaded.Name = "dateUploaded";
            this.dateUploaded.Size = new System.Drawing.Size(250, 30);
            this.dateUploaded.TabIndex = 17;
            this.dateUploaded.TabStop = false;
            this.dateUploaded.Text = "Thursday, July 16, 2015";
            this.dateUploaded.Value = new System.DateTime(2015, 7, 16, 17, 8, 1, 824);
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnUploadFile.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadFile.Location = new System.Drawing.Point(70, 550);
            this.btnUploadFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new System.Drawing.Size(200, 50);
            this.btnUploadFile.TabIndex = 20;
            this.btnUploadFile.Text = "Upload File";
            this.btnUploadFile.UseVisualStyleBackColor = false;
            this.btnUploadFile.Click += new System.EventHandler(this.btnUploadFile_Click_1);
            // 
            // btn_Browse
            // 
            this.btn_Browse.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Browse.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Browse.Location = new System.Drawing.Point(630, 70);
            this.btn_Browse.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(200, 50);
            this.btn_Browse.TabIndex = 21;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = false;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click_1);
            // 
            // txtFilePath
            // 
            this.txtFilePath.AutoSize = false;
            this.txtFilePath.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtFilePath.Location = new System.Drawing.Point(70, 70);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(500, 30);
            this.txtFilePath.TabIndex = 22;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(70, 625);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 24);
            this.lblMessage.TabIndex = 25;
            // 
            // cbx_Site
            // 
            this.cbx_Site.AutoSize = false;
            this.cbx_Site.Font = new System.Drawing.Font("Calibri", 10F);
            this.cbx_Site.Location = new System.Drawing.Point(255, 370);
            this.cbx_Site.Name = "cbx_Site";
            this.cbx_Site.Size = new System.Drawing.Size(250, 30);
            this.cbx_Site.TabIndex = 26;
            this.cbx_Site.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cbx_Site_SelectedIndexChanged);
            // 
            // cbx_Analyst
            // 
            this.cbx_Analyst.AutoSize = false;
            this.cbx_Analyst.Font = new System.Drawing.Font("Calibri", 10F);
            this.cbx_Analyst.Location = new System.Drawing.Point(255, 430);
            this.cbx_Analyst.Name = "cbx_Analyst";
            this.cbx_Analyst.Size = new System.Drawing.Size(250, 30);
            this.cbx_Analyst.TabIndex = 27;
            // 
            // progressIndicatorBrowse
            // 
            this.progressIndicatorBrowse.Image = ((System.Drawing.Image)(resources.GetObject("progressIndicatorBrowse.Image")));
            this.progressIndicatorBrowse.Location = new System.Drawing.Point(112, 110);
            this.progressIndicatorBrowse.Name = "progressIndicatorBrowse";
            this.progressIndicatorBrowse.Size = new System.Drawing.Size(65, 65);
            this.progressIndicatorBrowse.TabIndex = 29;
            this.progressIndicatorBrowse.TabStop = false;
            // 
            // progressIndicator
            // 
            this.progressIndicator.Image = global::FileLoader.Properties.Resources.ProgressUpload;
            this.progressIndicator.Location = new System.Drawing.Point(334, 550);
            this.progressIndicator.Name = "progressIndicator";
            this.progressIndicator.Size = new System.Drawing.Size(70, 70);
            this.progressIndicator.TabIndex = 28;
            this.progressIndicator.TabStop = false;
            // 
            // FileUploaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1192, 773);
            this.Controls.Add(this.progressIndicatorBrowse);
            this.Controls.Add(this.progressIndicator);
            this.Controls.Add(this.cbx_Analyst);
            this.Controls.Add(this.cbx_Site);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.btnUploadFile);
            this.Controls.Add(this.dateUploaded);
            this.Controls.Add(this.radGrid);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblAnalyst);
            this.Controls.Add(this.lblSite);
            this.Controls.Add(this.lblGridHeader);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FileUploaderForm";
            this.Text = "O365 File Uploader";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGrid.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateUploaded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_Site)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_Analyst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressIndicatorBrowse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressIndicator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label lblGridHeader;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblAnalyst;
        private System.Windows.Forms.Label lblDate;
        private Telerik.WinControls.UI.RadGridView radGrid;
        private Telerik.WinControls.UI.RadDateTimePicker dateUploaded;
        private System.Windows.Forms.Button btnUploadFile;
        private System.Windows.Forms.Button btn_Browse;
        private Telerik.WinControls.UI.RadTextBox txtFilePath;
        private System.Windows.Forms.Label lblMessage;
        private Telerik.WinControls.UI.RadDropDownList cbx_Site;
        private Telerik.WinControls.UI.RadDropDownList cbx_Analyst;
        private System.Windows.Forms.PictureBox progressIndicator;
        private System.Windows.Forms.PictureBox progressIndicatorBrowse;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateWeeklyMatrixBySiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateWeeklyMatrixByCompanyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateWeeklyTotalMetricsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;



    }
}

