namespace JS.Suite.CodeGeneration
{
    partial class JSTools
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
            this.CGT_TableCheckList = new System.Windows.Forms.CheckedListBox();
            this.CGT_CodeGenButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CGT_SuiteFolderTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CGT_DatabaseComboBox = new System.Windows.Forms.ComboBox();
            this.CGT_SelectAllTableCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCodeGenTool = new System.Windows.Forms.TabPage();
            this.tabBuildTool = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabCodeGenTool.SuspendLayout();
            this.tabBuildTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // CGT_TableCheckList
            // 
            this.CGT_TableCheckList.FormattingEnabled = true;
            this.CGT_TableCheckList.Location = new System.Drawing.Point(11, 30);
            this.CGT_TableCheckList.Name = "CGT_TableCheckList";
            this.CGT_TableCheckList.Size = new System.Drawing.Size(172, 229);
            this.CGT_TableCheckList.TabIndex = 0;
            // 
            // CGT_CodeGenButton
            // 
            this.CGT_CodeGenButton.Location = new System.Drawing.Point(197, 236);
            this.CGT_CodeGenButton.Name = "CGT_CodeGenButton";
            this.CGT_CodeGenButton.Size = new System.Drawing.Size(75, 23);
            this.CGT_CodeGenButton.TabIndex = 1;
            this.CGT_CodeGenButton.Text = "Code Gen";
            this.CGT_CodeGenButton.UseVisualStyleBackColor = true;
            this.CGT_CodeGenButton.Click += new System.EventHandler(this.CodeGenButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Suite Folder";
            // 
            // CGT_SuiteFolderTextBox
            // 
            this.CGT_SuiteFolderTextBox.Location = new System.Drawing.Point(194, 28);
            this.CGT_SuiteFolderTextBox.Name = "CGT_SuiteFolderTextBox";
            this.CGT_SuiteFolderTextBox.Size = new System.Drawing.Size(234, 20);
            this.CGT_SuiteFolderTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Database";
            // 
            // CGT_DatabaseComboBox
            // 
            this.CGT_DatabaseComboBox.FormattingEnabled = true;
            this.CGT_DatabaseComboBox.Location = new System.Drawing.Point(194, 72);
            this.CGT_DatabaseComboBox.Name = "CGT_DatabaseComboBox";
            this.CGT_DatabaseComboBox.Size = new System.Drawing.Size(234, 21);
            this.CGT_DatabaseComboBox.TabIndex = 8;
            this.CGT_DatabaseComboBox.SelectedIndexChanged += new System.EventHandler(this.DatabaseComboBox_SelectedIndexChanged);
            // 
            // CGT_SelectAllTableCheckBox
            // 
            this.CGT_SelectAllTableCheckBox.AutoSize = true;
            this.CGT_SelectAllTableCheckBox.Location = new System.Drawing.Point(11, 11);
            this.CGT_SelectAllTableCheckBox.Name = "CGT_SelectAllTableCheckBox";
            this.CGT_SelectAllTableCheckBox.Size = new System.Drawing.Size(70, 17);
            this.CGT_SelectAllTableCheckBox.TabIndex = 9;
            this.CGT_SelectAllTableCheckBox.Text = "Select All";
            this.CGT_SelectAllTableCheckBox.UseVisualStyleBackColor = true;
            this.CGT_SelectAllTableCheckBox.CheckedChanged += new System.EventHandler(this.SelectAllTableCheckBox_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCodeGenTool);
            this.tabControl1.Controls.Add(this.tabBuildTool);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(449, 310);
            this.tabControl1.TabIndex = 10;
            // 
            // tabCodeGenTool
            // 
            this.tabCodeGenTool.Controls.Add(this.CGT_TableCheckList);
            this.tabCodeGenTool.Controls.Add(this.CGT_SelectAllTableCheckBox);
            this.tabCodeGenTool.Controls.Add(this.CGT_CodeGenButton);
            this.tabCodeGenTool.Controls.Add(this.CGT_DatabaseComboBox);
            this.tabCodeGenTool.Controls.Add(this.label2);
            this.tabCodeGenTool.Controls.Add(this.label3);
            this.tabCodeGenTool.Controls.Add(this.CGT_SuiteFolderTextBox);
            this.tabCodeGenTool.Location = new System.Drawing.Point(4, 22);
            this.tabCodeGenTool.Name = "tabCodeGenTool";
            this.tabCodeGenTool.Padding = new System.Windows.Forms.Padding(3);
            this.tabCodeGenTool.Size = new System.Drawing.Size(441, 284);
            this.tabCodeGenTool.TabIndex = 0;
            this.tabCodeGenTool.Text = "Code Gen Tool";
            this.tabCodeGenTool.UseVisualStyleBackColor = true;
            // 
            // tabBuildTool
            // 
            this.tabBuildTool.Controls.Add(this.label1);
            this.tabBuildTool.Location = new System.Drawing.Point(4, 22);
            this.tabBuildTool.Name = "tabBuildTool";
            this.tabBuildTool.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuildTool.Size = new System.Drawing.Size(441, 284);
            this.tabBuildTool.TabIndex = 1;
            this.tabBuildTool.Text = "Build Tool";
            this.tabBuildTool.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(86, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "COMING SOON";
            // 
            // JSTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 336);
            this.Controls.Add(this.tabControl1);
            this.Name = "JSTools";
            this.Text = "JS Tools";
            this.tabControl1.ResumeLayout(false);
            this.tabCodeGenTool.ResumeLayout(false);
            this.tabCodeGenTool.PerformLayout();
            this.tabBuildTool.ResumeLayout(false);
            this.tabBuildTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CGT_TableCheckList;
        private System.Windows.Forms.Button CGT_CodeGenButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CGT_SuiteFolderTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CGT_DatabaseComboBox;
        private System.Windows.Forms.CheckBox CGT_SelectAllTableCheckBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCodeGenTool;
        private System.Windows.Forms.TabPage tabBuildTool;
        private System.Windows.Forms.Label label1;

    }
}

