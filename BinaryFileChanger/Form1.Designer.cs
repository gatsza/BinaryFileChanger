namespace WindowsFormsApp1
{
    partial class Form1
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
            this.add1 = new System.Windows.Forms.Button();
            this.browserButton = new System.Windows.Forms.Button();
            this.browserText = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.fileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.excel = new System.Windows.Forms.Button();
            this.writeQR = new System.Windows.Forms.Button();
            this.readQR = new System.Windows.Forms.Button();
            this.readNextExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // add1
            // 
            this.add1.Location = new System.Drawing.Point(19, 96);
            this.add1.Name = "add1";
            this.add1.Size = new System.Drawing.Size(24, 23);
            this.add1.TabIndex = 6;
            this.add1.Text = "+";
            this.add1.UseVisualStyleBackColor = true;
            this.add1.Click += new System.EventHandler(this.addButton_Click);
            // 
            // browserButton
            // 
            this.browserButton.Location = new System.Drawing.Point(393, 29);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(75, 23);
            this.browserButton.TabIndex = 2;
            this.browserButton.Text = "Browser";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.browserButton_Click);
            // 
            // browserText
            // 
            this.browserText.AccessibleDescription = "";
            this.browserText.AccessibleName = "";
            this.browserText.Location = new System.Drawing.Point(15, 32);
            this.browserText.Name = "browserText";
            this.browserText.ReadOnly = true;
            this.browserText.Size = new System.Drawing.Size(364, 20);
            this.browserText.TabIndex = 3;
            this.browserText.Tag = "";
            this.browserText.Text = "C:\\";
            this.browserText.TextChanged += new System.EventHandler(this.browserText_TextChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(393, 182);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save File";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Panel1
            // 
            this.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Panel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.Panel1.ColumnCount = 6;
            this.Panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.Panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.Panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.Panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.Panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.Panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.Panel1.Location = new System.Drawing.Point(15, 125);
            this.Panel1.Name = "Panel1";
            this.Panel1.RowCount = 1;
            this.Panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.Panel1.Size = new System.Drawing.Size(471, 32);
            this.Panel1.TabIndex = 1;
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(164, 184);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(215, 20);
            this.fileName.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Cím";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Adat Hossz";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Adat";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Bemeneti fájl elérése:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(105, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Fájl neve:";
            // 
            // excel
            // 
            this.excel.Location = new System.Drawing.Point(15, 58);
            this.excel.Name = "excel";
            this.excel.Size = new System.Drawing.Size(75, 23);
            this.excel.TabIndex = 15;
            this.excel.Text = "Import Excel";
            this.excel.UseVisualStyleBackColor = true;
            this.excel.Click += new System.EventHandler(this.excel_Click);
            // 
            // writeQR
            // 
            this.writeQR.Enabled = false;
            this.writeQR.Location = new System.Drawing.Point(266, 58);
            this.writeQR.Name = "writeQR";
            this.writeQR.Size = new System.Drawing.Size(75, 23);
            this.writeQR.TabIndex = 16;
            this.writeQR.Text = "QR write";
            this.writeQR.UseVisualStyleBackColor = true;
            this.writeQR.Click += new System.EventHandler(this.writeQR_Click);
            // 
            // readQR
            // 
            this.readQR.Enabled = false;
            this.readQR.Location = new System.Drawing.Point(375, 58);
            this.readQR.Name = "readQR";
            this.readQR.Size = new System.Drawing.Size(93, 23);
            this.readQR.TabIndex = 17;
            this.readQR.Text = "QR read";
            this.readQR.UseVisualStyleBackColor = true;
            // 
            // readNextExcel
            // 
            this.readNextExcel.Location = new System.Drawing.Point(127, 58);
            this.readNextExcel.Name = "readNextExcel";
            this.readNextExcel.Size = new System.Drawing.Size(109, 23);
            this.readNextExcel.TabIndex = 18;
            this.readNextExcel.Text = "Next From Excel";
            this.readNextExcel.UseVisualStyleBackColor = true;
            this.readNextExcel.Click += new System.EventHandler(this.readNextExcel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 240);
            this.Controls.Add(this.readNextExcel);
            this.Controls.Add(this.readQR);
            this.Controls.Add(this.writeQR);
            this.Controls.Add(this.excel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.browserText);
            this.Controls.Add(this.browserButton);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.add1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button add1;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.TextBox browserText;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TableLayoutPanel Panel1;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button excel;
        private System.Windows.Forms.Button writeQR;
        private System.Windows.Forms.Button readQR;
        private System.Windows.Forms.Button readNextExcel;
    }
}

