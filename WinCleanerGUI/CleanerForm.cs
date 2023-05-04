#define DEV

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WinCleaner;
using Microsoft.Win32.TaskScheduler;


namespace WinCleanerGUI
{
    public partial class CleanerForm : Form
    {
        private const string _taskPath = "\\WinCleaner";


        public CleanerForm()
        {
            InitializeComponent();
            _clearRecycleBinCheckBox.Checked = ConfigurationManager.GetClearRecycleBin();
            var task = TaskService.Instance.GetTask(_taskPath);
            if (task is null)
            {
                _neverRadioButton.Select();
            }
            else
            {
                //var definition = task.Definition;
                //var triggers = definition.Triggers;
                //var dailyTrigger = new DailyTrigger();
                //var weeklyTrigger = new WeeklyTrigger();
                //var monthlyTrigger = new MonthlyTrigger();
                //triggers.Contains(dailyTrigger);
            }
            //TaskDefinition td = TaskService.Instance.NewTask();
            //td.Triggers.AddNew()
            //TimeTrigger timeTrigger = new TimeTrigger();
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
#if DEV
                    WindowStyle = ProcessWindowStyle.Normal,
#else
                    WindowStyle = ProcessWindowStyle.Hidden,
#endif
                    FileName = "WinCleaner.exe",
                },
                EnableRaisingEvents = true
            };
            process.Exited += (sn, ev) => _clearButton.Enabled = true;

            _clearButton.Enabled = false;

            process.Start();
        }

        private void AddDirectoryButtonClick(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ConfigurationManager.AddInclude(fbd.SelectedPath);
                }
            }
        }

        private void ExcludeDirectoryButtonClick(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ConfigurationManager.AddExclude(fbd.SelectedPath);
                }
            }
        }

        private void ShowIncludeClick(object sender, EventArgs e)
        {
            var dirs = ConfigurationManager.ReadInclude().ToArray();
            var listForm = new ListForm(dirs, "Добавленные директории", Location);
            Hide();
            listForm.Show(this);
        }

        private void ShowExcludeClick(object sender, EventArgs e)
        {
            var dirs = ConfigurationManager.ReadExclude().ToArray();
            var listForm = new ListForm(dirs, "Исключенные директории", Location);
            Hide();
            listForm.Show(this);
        }

        private void СlearRecycleBinCheckedChanged(object sender, EventArgs e)
        {
            ConfigurationManager.SetClearRecycleBin(_clearRecycleBinCheckBox.Checked);
        }
    }


    internal enum TaskRepetitionTrigger
    {
        Never,
        Daily,
        Weekly,
        Monthly,
    }
}
