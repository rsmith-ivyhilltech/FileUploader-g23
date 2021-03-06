﻿namespace FileLoader
{
    partial class G23Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(G23Form));
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.txtFilePath = new Telerik.WinControls.UI.RadTextBoxControl();
            this.label1 = new System.Windows.Forms.Label();
            this.usersRadDropDownList = new Telerik.WinControls.UI.RadDropDownList();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.btnUploadFile = new System.Windows.Forms.Button();
            this.progressIndicatorBrowse = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dateUploaded = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.radGrid = new Telerik.WinControls.UI.RadGridView();
            this.progressIndicator = new System.Windows.Forms.PictureBox();
            this.lblGridHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersRadDropDownList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressIndicatorBrowse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateUploaded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGrid.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressIndicator)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.IsReadOnly = true;
            this.txtFilePath.Location = new System.Drawing.Point(47, 45);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(333, 19);
            this.txtFilePath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 247);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Users";
            // 
            // usersRadDropDownList
            // 
            this.usersRadDropDownList.AutoSize = false;
            this.usersRadDropDownList.DropDownHeight = 71;
            this.usersRadDropDownList.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.usersRadDropDownList.Location = new System.Drawing.Point(170, 240);
            this.usersRadDropDownList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.usersRadDropDownList.Name = "usersRadDropDownList";
            this.usersRadDropDownList.Size = new System.Drawing.Size(167, 20);
            this.usersRadDropDownList.TabIndex = 2;
            // 
            // btn_Browse
            // 
            this.btn_Browse.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Browse.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Browse.Location = new System.Drawing.Point(420, 45);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(133, 32);
            this.btn_Browse.TabIndex = 22;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = false;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnUploadFile.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadFile.Location = new System.Drawing.Point(47, 357);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new System.Drawing.Size(133, 32);
            this.btnUploadFile.TabIndex = 23;
            this.btnUploadFile.Text = "Upload File";
            this.btnUploadFile.UseVisualStyleBackColor = false;
            this.btnUploadFile.Click += new System.EventHandler(this.btnUploadFile_Click);
            // 
            // progressIndicatorBrowse
            // 
            this.progressIndicatorBrowse.Image = ((System.Drawing.Image)(resources.GetObject("progressIndicatorBrowse.Image")));
            this.progressIndicatorBrowse.Location = new System.Drawing.Point(75, 71);
            this.progressIndicatorBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressIndicatorBrowse.Name = "progressIndicatorBrowse";
            this.progressIndicatorBrowse.Size = new System.Drawing.Size(43, 42);
            this.progressIndicatorBrowse.TabIndex = 30;
            this.progressIndicatorBrowse.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(47, 406);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 25;
            // 
            // dateUploaded
            // 
            this.dateUploaded.AutoSize = false;
            this.dateUploaded.Font = new System.Drawing.Font("Calibri", 10F);
            this.dateUploaded.Location = new System.Drawing.Point(170, 279);
            this.dateUploaded.Name = "dateUploaded";
            this.dateUploaded.Size = new System.Drawing.Size(167, 19);
            this.dateUploaded.TabIndex = 31;
            this.dateUploaded.TabStop = false;
            this.dateUploaded.Text = "Thursday, July 16, 2015";
            this.dateUploaded.Value = new System.DateTime(2015, 7, 16, 17, 8, 1, 824);
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(47, 286);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(67, 19);
            this.lblDate.TabIndex = 32;
            this.lblDate.Text = "Date Uploaded";
            // 
            // radGrid
            // 
            this.radGrid.Font = new System.Drawing.Font("Calibri", 10F);
            this.radGrid.Location = new System.Drawing.Point(47, 117);
            // 
            // 
            // 
            this.radGrid.MasterTemplate.AllowAddNewRow = false;
            this.radGrid.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGrid.Name = "radGrid";
            this.radGrid.ReadOnly = true;
            this.radGrid.Size = new System.Drawing.Size(667, 97);
            this.radGrid.TabIndex = 33;
            this.radGrid.Text = "Data Grid";
            // 
            // progressIndicator
            // 
            this.progressIndicator.Image = global::FileLoader.Properties.Resources.ProgressUpload;
            this.progressIndicator.Location = new System.Drawing.Point(223, 357);
            this.progressIndicator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressIndicator.Name = "progressIndicator";
            this.progressIndicator.Size = new System.Drawing.Size(47, 45);
            this.progressIndicator.TabIndex = 34;
            this.progressIndicator.TabStop = false;
            // 
            // lblGridHeader
            // 
            this.lblGridHeader.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridHeader.Location = new System.Drawing.Point(170, 82);
            this.lblGridHeader.Name = "lblGridHeader";
            this.lblGridHeader.Size = new System.Drawing.Size(210, 25);
            this.lblGridHeader.TabIndex = 35;
            this.lblGridHeader.Text = "Review Uploaded Data";
            // 
            // G23Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 476);
            this.Controls.Add(this.lblGridHeader);
            this.Controls.Add(this.progressIndicator);
            this.Controls.Add(this.radGrid);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dateUploaded);
            this.Controls.Add(this.progressIndicatorBrowse);
            this.Controls.Add(this.btnUploadFile);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.usersRadDropDownList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "G23Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Office 365 IPO G-23 Uploader";
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersRadDropDownList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressIndicatorBrowse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateUploaded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGrid.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressIndicator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBoxControl txtFilePath;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadDropDownList usersRadDropDownList;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Button btnUploadFile;
        private System.Windows.Forms.PictureBox progressIndicatorBrowse;
        private System.Windows.Forms.Label lblMessage;
        private Telerik.WinControls.UI.RadDateTimePicker dateUploaded;
        private System.Windows.Forms.Label lblDate;
        private Telerik.WinControls.UI.RadGridView radGrid;
        private System.Windows.Forms.PictureBox progressIndicator;
        private System.Windows.Forms.Label lblGridHeader;
    }
}