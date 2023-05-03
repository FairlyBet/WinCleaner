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
            this._createTaskButton = new System.Windows.Forms.Button();
            this._clearTrashboxCheckBox = new System.Windows.Forms.CheckBox();
            this._showInclude = new System.Windows.Forms.Button();
            this._showExclude = new System.Windows.Forms.Button();
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
            // _createTaskButton
            // 
            this._createTaskButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this._createTaskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._createTaskButton.Location = new System.Drawing.Point(12, 12);
            this._createTaskButton.Name = "_createTaskButton";
            this._createTaskButton.Size = new System.Drawing.Size(210, 50);
            this._createTaskButton.TabIndex = 3;
            this._createTaskButton.Text = "Запланировать очистку";
            this._createTaskButton.UseVisualStyleBackColor = false;
            // 
            // _clearTrashboxCheckBox
            // 
            this._clearTrashboxCheckBox.AutoSize = true;
            this._clearTrashboxCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._clearTrashboxCheckBox.Location = new System.Drawing.Point(291, 38);
            this._clearTrashboxCheckBox.Name = "_clearTrashboxCheckBox";
            this._clearTrashboxCheckBox.Size = new System.Drawing.Size(161, 24);
            this._clearTrashboxCheckBox.TabIndex = 4;
            this._clearTrashboxCheckBox.Text = "Очищать корзину";
            this._clearTrashboxCheckBox.UseVisualStyleBackColor = true;
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
            // CleanerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this._showExclude);
            this.Controls.Add(this._showInclude);
            this.Controls.Add(this._clearTrashboxCheckBox);
            this.Controls.Add(this._createTaskButton);
            this.Controls.Add(this._excludeDirectoryButton);
            this.Controls.Add(this._addDirectoryButton);
            this.Controls.Add(this._clearButton);
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
        private System.Windows.Forms.Button _createTaskButton;
        private System.Windows.Forms.CheckBox _clearTrashboxCheckBox;
        private System.Windows.Forms.Button _showInclude;
        private System.Windows.Forms.Button _showExclude;
    }
}

