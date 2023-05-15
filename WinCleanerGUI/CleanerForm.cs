//#define DUMMY

using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using WinCleaner;


namespace WinCleanerGUI
{
    public partial class CleanerForm : Form
    {
        private const string TaskPath = "WinCleaner";
        private const string TaskAction = "WinCleaner.exe";
        private static readonly TaskService _ts = TaskService.Instance;
        private Microsoft.Win32.TaskScheduler.Task _task;


        public CleanerForm()
        {
            InitializeComponent();

            _clearRecycleBinCheckBox.Checked = ConfigurationManager.GetClearRecycleBin();
            _task = _ts.GetTask(TaskPath);

            SelectRadioButton();
        }

        private void SelectRadioButton()
        {
            if (_task is null)
            {
                _neverRadioButton.Select();
                return;
            }

            var td = _task.Definition;
            var trigger = td.Triggers.FirstOrDefault();
            if (trigger is null || td.Triggers.Count != 1)
            {
                _neverRadioButton.Select();
            }
            else if (trigger is DailyTrigger)
            {
                _dailyRadioButton.Select();
            }
            else if (trigger is WeeklyTrigger)
            {
                _weeklyRadioButton.Select();
            }
            else if (trigger is MonthlyTrigger)
            {
                _monthlyRadioButton.Select();
            }
            else
            {
                _neverRadioButton.Select();
            }
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            _ = PerformCleaningAsync();
        }

        private async System.Threading.Tasks.Task PerformCleaningAsync()
        {
            _clearButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
#if !DUMMY
            void Cleaning()
            {
                Logger.CommitBeforeCleaning();
                WinCleaner.Cleaner.PerformCleaning();
                Logger.CommitAfterCleaning();
                Logger.Publish();
            }
            await System.Threading.Tasks.Task.Run(Cleaning);
#endif
            _clearButton.Enabled = true;
            Cursor = Cursors.Default;
            MessageBox.Show($"Очитска завершена\nВсего очищено {Logger.TotalFreeAfter - Logger.TotalFreeBefore} байт");
        }

        private void IncludeDirectoryButtonClick(object sender, EventArgs e)
        {
            try
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
            catch { }
        }

        private void ExcludeDirectoryButtonClick(object sender, EventArgs e)
        {
            try
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
            catch { }
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
            try
            {
                ConfigurationManager.SetClearRecycleBin(_clearRecycleBinCheckBox.Checked);
            }
            catch { }
        }

        private void NeverRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_neverRadioButton.Checked)
            {
                if (_task != null)
                {
                    _ts.RootFolder.DeleteTask(_task.Name);
                    _task = null;
                }
            }
        }

        private void DailyRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            if (_dailyRadioButton.Checked)
            {
                SetTrigger(new DailyTrigger());
            }
        }

        private void WeeklyRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            if (_weeklyRadioButton.Checked)
            {
                SetTrigger(new WeeklyTrigger());
            }
        }

        private void MonthlyRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            if (_monthlyRadioButton.Checked)
            {
                SetTrigger(new MonthlyTrigger());
            }
        }

        private void SetTrigger(Trigger trigger)
        {
            if (_task != null)
            {
                _ts.RootFolder.DeleteTask(_task.Name);
            }
            var td = _ts.NewTask();
            td.Actions.Add(TaskAction);
            td.Triggers.Add(trigger);
            td.RegistrationInfo.Description = "WinCleaner task";

            _task = _ts.RootFolder.RegisterTaskDefinition(TaskPath, td);
        }
    }
}
