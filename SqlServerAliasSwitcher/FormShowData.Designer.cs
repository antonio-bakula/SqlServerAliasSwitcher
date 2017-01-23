namespace SqlServerAliasSwitcher
{
  partial class FormShowData
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
      this.gridViewCmdData = new System.Windows.Forms.DataGridView();
      this.panelFooter = new System.Windows.Forms.Panel();
      this.buttonClose = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.gridViewCmdData)).BeginInit();
      this.panelFooter.SuspendLayout();
      this.SuspendLayout();
      // 
      // gridViewCmdData
      // 
      this.gridViewCmdData.AllowUserToAddRows = false;
      this.gridViewCmdData.AllowUserToDeleteRows = false;
      this.gridViewCmdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gridViewCmdData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.gridViewCmdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gridViewCmdData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gridViewCmdData.Location = new System.Drawing.Point(0, 0);
      this.gridViewCmdData.Name = "gridViewCmdData";
      this.gridViewCmdData.ReadOnly = true;
      this.gridViewCmdData.Size = new System.Drawing.Size(726, 481);
      this.gridViewCmdData.TabIndex = 0;
      // 
      // panelFooter
      // 
      this.panelFooter.Controls.Add(this.buttonClose);
      this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelFooter.Location = new System.Drawing.Point(0, 487);
      this.panelFooter.Name = "panelFooter";
      this.panelFooter.Size = new System.Drawing.Size(726, 55);
      this.panelFooter.TabIndex = 1;
      // 
      // buttonClose
      // 
      this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonClose.Location = new System.Drawing.Point(639, 20);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(75, 23);
      this.buttonClose.TabIndex = 0;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      // 
      // FormShowData
      // 
      this.AcceptButton = this.buttonClose;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(726, 542);
      this.Controls.Add(this.panelFooter);
      this.Controls.Add(this.gridViewCmdData);
      this.Name = "FormShowData";
      this.Text = "FormShowData";
      ((System.ComponentModel.ISupportInitialize)(this.gridViewCmdData)).EndInit();
      this.panelFooter.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView gridViewCmdData;
    private System.Windows.Forms.Panel panelFooter;
    private System.Windows.Forms.Button buttonClose;
  }
}