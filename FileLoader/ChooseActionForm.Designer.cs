namespace FileLoader
{
    partial class ChooseActionForm
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
            Telerik.WinControls.UI.RadPrintWatermark radPrintWatermark1 = new Telerik.WinControls.UI.RadPrintWatermark();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseActionForm));
            this.radPrintDocument1 = new Telerik.WinControls.UI.RadPrintDocument();
            this.selectActionRadDropDownList = new Telerik.WinControls.UI.RadDropDownList();
            this.selectionActionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.selectActionRadDropDownList)).BeginInit();
            this.SuspendLayout();
            // 
            // radPrintDocument1
            // 
            this.radPrintDocument1.FooterFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPrintDocument1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPrintDocument1.Watermark = radPrintWatermark1;
            // 
            // selectActionRadDropDownList
            // 
            this.selectActionRadDropDownList.Location = new System.Drawing.Point(101, 93);
            this.selectActionRadDropDownList.Name = "selectActionRadDropDownList";
            this.selectActionRadDropDownList.Size = new System.Drawing.Size(301, 27);
            this.selectActionRadDropDownList.TabIndex = 1;
            // 
            // selectionActionButton
            // 
            this.selectionActionButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.selectionActionButton.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectionActionButton.Location = new System.Drawing.Point(424, 80);
            this.selectionActionButton.Name = "selectionActionButton";
            this.selectionActionButton.Size = new System.Drawing.Size(200, 50);
            this.selectionActionButton.TabIndex = 2;
            this.selectionActionButton.Text = "Go To Action";
            this.selectionActionButton.UseVisualStyleBackColor = false;
            this.selectionActionButton.Click += new System.EventHandler(this.selectionActionButton_Click);
            // 
            // ChooseActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 406);
            this.Controls.Add(this.selectionActionButton);
            this.Controls.Add(this.selectActionRadDropDownList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChooseActionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "O365 Choose Action";
            ((System.ComponentModel.ISupportInitialize)(this.selectActionRadDropDownList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadPrintDocument radPrintDocument1;
        private Telerik.WinControls.UI.RadDropDownList selectActionRadDropDownList;
        private System.Windows.Forms.Button selectionActionButton;
    }
}