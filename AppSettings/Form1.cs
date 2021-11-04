using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppSettings
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AppSettings.Instance().Load((MethodInvoker)delegate
            {
                OnSettingsLoaded();
            });
        }

        void OnSettingsLoaded()
        {
            if (AppSettings.Instance().Settings.UpdateUIFromSetting)
            {
                this.Width = AppSettings.Instance().Settings.ScreenSizeWidth;
                this.Height = AppSettings.Instance().Settings.ScreenSizeHeight;
                this.Text = AppSettings.Instance().Settings.Title;

                OnUpdateUISetting();
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            this.textBox1.Text = AppSettings.Instance().Settings.Title;
            this.textBox2.Text = this.Width.ToString();
            this.textBox3.Text = this.Height.ToString();
        }

        void OnUpdateUISetting()
        {
            this.textBox1.Text = AppSettings.Instance().Settings.Title;
            this.textBox2.Text = this.Width.ToString();
            this.textBox3.Text = this.Height.ToString();
            this.checkBox1.Checked = AppSettings.Instance().Settings.UpdateUIFromSetting;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnSaveSettings(null, null);
        }

        private void OnSaveSettings(object sender, EventArgs e)
        {
            AppSettings.Instance().Settings.ScreenSizeWidth = this.Width;
            AppSettings.Instance().Settings.ScreenSizeHeight = this.Height;
            AppSettings.Instance().Settings.Title = this.textBox1.Text;
            AppSettings.Instance().Settings.UpdateUIFromSetting = this.checkBox1.Checked;
            AppSettings.Instance().Save();
        }
    }
}
