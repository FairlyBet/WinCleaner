﻿#define DEV

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
        private const string _taskPath = "WinCleaner";
        private const string _taskAction = "WinCleaner.exe";
        private static readonly TaskService _ts = TaskService.Instance;
        private Task _task;


        public CleanerForm()
        {
            InitializeComponent();
            _clearRecycleBinCheckBox.Checked = ConfigurationManager.GetClearRecycleBin();

            _task = _ts.GetTask(_taskPath);

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
            var tigger = td.Triggers.FirstOrDefault();
            if (tigger is null || td.Triggers.Count != 1)
            {
                _neverRadioButton.Select();
            }
            else if (tigger is DailyTrigger)
            {
                _dailyRadioButton.Select();
            }
            else if (tigger is WeeklyTrigger)
            {
                _weeklyRadioButton.Select();
            }
            else if (tigger is MonthlyTrigger)
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
            td.Actions.Add(_taskAction);
            td.Triggers.Add(trigger);
            td.RegistrationInfo.Description = "WinCleaner task";

            _task = _ts.RootFolder.RegisterTaskDefinition(_taskPath, td);
        }
    }
}
