using System.Drawing;
using System.Windows.Forms;


namespace WinCleanerGUI
{
    public partial class ListForm : Form
    {
        public ListForm(string[] lines, string title, Point location)
        {
            InitializeComponent();
            Text = title;
            if (lines.Length == 0)
            {
                dirsListBox.Items.Add("Список пуст...");
            }
            dirsListBox.Items.AddRange(lines);
            Location = location;
        }

        private void ShowParent(object sender, FormClosingEventArgs e)
        {
            Owner.Location = Location;
            Owner.Show();
        }
    }
}
