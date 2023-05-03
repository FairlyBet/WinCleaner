namespace WinCleanerGUI
{
    partial class ListForm
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
            this.dirsListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // dirsListBox
            // 
            this.dirsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dirsListBox.FormattingEnabled = true;
            this.dirsListBox.ItemHeight = 15;
            this.dirsListBox.Location = new System.Drawing.Point(12, 12);
            this.dirsListBox.Name = "dirsListBox";
            this.dirsListBox.Size = new System.Drawing.Size(440, 244);
            this.dirsListBox.TabIndex = 0;
            this.dirsListBox.TabStop = false;
            // 
            // ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.dirsListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ListForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowParent);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox dirsListBox;
    }
}