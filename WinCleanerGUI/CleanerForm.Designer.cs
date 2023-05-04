namespace WinCleanerGUI
{
    partial class CleanerForm
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
            this._clearButton = new System.Windows.Forms.Button();
            this._addDirectoryButton = new System.Windows.Forms.Button();
            this._excludeDirectoryButton = new System.Windows.Forms.Button();
            this._clearRecycleBinCheckBox = new System.Windows.Forms.CheckBox();
            this._showInclude = new System.Windows.Forms.Button();
            this._showExclude = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this._neverRadioButton = new System.Windows.Forms.RadioButton();
            this._monthlyRadioButton = new System.Windows.Forms.RadioButton();
            this._weeklyRadioButton = new System.Windows.Forms.RadioButton();
            this._dailyRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _clearButton
            // 
            this._clearButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._clearButton.Location = new System.Drawing.Point(352, 219);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(100, 50);
            this._clearButton.TabIndex = 0;
            this._clearButton.Text = "Очистить";
            this._clearButton.UseVisualStyleBackColor = false;
            this._clearButton.Click += new System.EventHandler(this.ClearButtonClick);
            // 
            // _addDirectoryButton
            // 
            this._addDirectoryButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this._addDirectoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._addDirectoryButton.Location = new System.Drawing.Point(12, 163);
            this._addDirectoryButton.Name = "_addDirectoryButton";
            this._addDirectoryButton.Size = new System.Drawing.Size(210, 50);
            this._addDirectoryButton.TabIndex = 1;
            this._addDirectoryButton.Text = "Добавить директорию";
            this._addDirectoryButton.UseVisualStyleBackColor = false;
            this._addDirectoryButton.Click += new System.EventHandler(this.AddDirectoryButtonClick);
            // 
            // _excludeDirectoryButton
            // 
            this._excludeDirectoryButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this._excludeDirectoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._excludeDirectoryButton.Location = new System.Drawing.Point(12, 219);
            this._excludeDirectoryButton.Name = "_excludeDirectoryButton";
            this._excludeDirectoryButton.Size = new System.Drawing.Size(210, 50);
            this._excludeDirectoryButton.TabIndex = 2;
            this._excludeDirectoryButton.Text = "Исключить директорию";
            this._excludeDirectoryButton.UseVisualStyleBackColor = false;
            this._excludeDirectoryButton.Click += new System.EventHandler(this.ExcludeDirectoryButtonClick);
            // 
            // _clearRecycleBinCheckBox
            // 
            this._clearRecycleBinCheckBox.AutoSize = true;
            this._clearRecycleBinCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._clearRecycleBinCheckBox.Location = new System.Drawing.Point(15, 133);
            this._clearRecycleBinCheckBox.Name = "_clearRecycleBinCheckBox";
            this._clearRecycleBinCheckBox.Size = new System.Drawing.Size(161, 24);
            this._clearRecycleBinCheckBox.TabIndex = 4;
            this._clearRecycleBinCheckBox.Text = "Очищать корзину";
            this._clearRecycleBinCheckBox.UseVisualStyleBackColor = true;
            this._clearRecycleBinCheckBox.CheckedChanged += new System.EventHandler(this.СlearRecycleBinCheckedChanged);
            // 
            // _showInclude
            // 
            this._showInclude.BackColor = System.Drawing.SystemColors.ButtonFace;
            this._showInclude.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._showInclude.Location = new System.Drawing.Point(228, 183);
            this._showInclude.Name = "_showInclude";
            this._showInclude.Size = new System.Drawing.Size(40, 30);
            this._showInclude.TabIndex = 5;
            this._showInclude.Text = "...";
            this._showInclude.UseVisualStyleBackColor = false;
            this._showInclude.Click += new System.EventHandler(this.ShowIncludeClick);
            // 
            // _showExclude
            // 
            this._showExclude.BackColor = System.Drawing.SystemColors.ButtonFace;
            this._showExclude.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._showExclude.Location = new System.Drawing.Point(228, 239);
            this._showExclude.Name = "_showExclude";
            this._showExclude.Size = new System.Drawing.Size(40, 30);
            this._showExclude.TabIndex = 6;
            this._showExclude.Text = "...";
            this._showExclude.UseVisualStyleBackColor = false;
            this._showExclude.Click += new System.EventHandler(this.ShowExcludeClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(352, 233);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // _neverRadioButton
            // 
            this._neverRadioButton.AutoSize = true;
            this._neverRadioButton.Location = new System.Drawing.Point(15, 30);
            this._neverRadioButton.Name = "_neverRadioButton";
            this._neverRadioButton.Size = new System.Drawing.Size(68, 17);
            this._neverRadioButton.TabIndex = 8;
            this._neverRadioButton.TabStop = true;
            this._neverRadioButton.Text = "Никогда";
            this._neverRadioButton.UseVisualStyleBackColor = true;
            // 
            // _monthlyRadioButton
            // 
            this._monthlyRadioButton.AutoSize = true;
            this._monthlyRadioButton.Location = new System.Drawing.Point(277, 30);
            this._monthlyRadioButton.Name = "_monthlyRadioButton";
            this._monthlyRadioButton.Size = new System.Drawing.Size(89, 17);
            this._monthlyRadioButton.TabIndex = 9;
            this._monthlyRadioButton.TabStop = true;
            this._monthlyRadioButton.Text = "Ежемесячно";
            this._monthlyRadioButton.UseVisualStyleBackColor = true;
            // 
            // _weeklyRadioButton
            // 
            this._weeklyRadioButton.AutoSize = true;
            this._weeklyRadioButton.Location = new System.Drawing.Point(177, 30);
            this._weeklyRadioButton.Name = "_weeklyRadioButton";
            this._weeklyRadioButton.Size = new System.Drawing.Size(94, 17);
            this._weeklyRadioButton.TabIndex = 10;
            this._weeklyRadioButton.TabStop = true;
            this._weeklyRadioButton.Text = "Еженедельно";
            this._weeklyRadioButton.UseVisualStyleBackColor = true;
            // 
            // _dailyRadioButton
            // 
            this._dailyRadioButton.AutoSize = true;
            this._dailyRadioButton.Location = new System.Drawing.Point(89, 30);
            this._dailyRadioButton.Name = "_dailyRadioButton";
            this._dailyRadioButton.Size = new System.Drawing.Size(82, 17);
            this._dailyRadioButton.TabIndex = 11;
            this._dailyRadioButton.TabStop = true;
            this._dailyRadioButton.Text = "Ежедневно";
            this._dailyRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Планировать очистку";
            // 
            // CleanerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._clearButton);
            this.Controls.Add(this._dailyRadioButton);
            this.Controls.Add(this._weeklyRadioButton);
            this.Controls.Add(this._monthlyRadioButton);
            this.Controls.Add(this._neverRadioButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this._showExclude);
            this.Controls.Add(this._showInclude);
            this.Controls.Add(this._clearRecycleBinCheckBox);
            this.Controls.Add(this._excludeDirectoryButton);
            this.Controls.Add(this._addDirectoryButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CleanerForm";
            this.Text = "WinCleaner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _clearButton;
        private System.Windows.Forms.Button _addDirectoryButton;
        private System.Windows.Forms.Button _excludeDirectoryButton;
        private System.Windows.Forms.CheckBox _clearRecycleBinCheckBox;
        private System.Windows.Forms.Button _showInclude;
        private System.Windows.Forms.Button _showExclude;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RadioButton _neverRadioButton;
        private System.Windows.Forms.RadioButton _dailyRadioButton;
        private System.Windows.Forms.RadioButton _weeklyRadioButton;
        private System.Windows.Forms.RadioButton _monthlyRadioButton;
        private System.Windows.Forms.Label label1;
    }
}

