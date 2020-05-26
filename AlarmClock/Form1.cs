using System;
using System.Windows.Forms;
using System.Media;

namespace AlarmClock
{
    public partial class Form1 : Form
    {
		private DateTime alarm;
		SoundPlayer sp = new SoundPlayer();	
		public Form1()
        {
            InitializeComponent();
			
			numericUpDown1.Maximum = 23;
			numericUpDown1.Minimum = 0;

			numericUpDown2.Maximum = 59;
			numericUpDown2.Minimum = 0;

			numericUpDown1.Value = DateTime.Now.Hour;
			numericUpDown2.Value = DateTime.Now.Minute;
					
			notifyIcon1.Text = "Будильник не установлен";
			notifyIcon1.Visible = true;
			
			timer1.Interval = 1000;
			timer1.Enabled = true;

			label2.Text = DateTime.Now.ToLongTimeString();
		}

        private void toolStripMenuExit_Click(object sender, EventArgs e)
        {
			this.Close();
        }

        private void toolStripMenuHideShow_Click(object sender, EventArgs e)
        {
			if (this.Visible) this.Hide();
			else this.Show();
		}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
			sp.Stop();
			if (checkBox1.Checked)
			{
				numericUpDown1.Enabled = false;
				numericUpDown2.Enabled = false;
				alarm = new DateTime(
					DateTime.Now.Year,
					DateTime.Now.Month,
					DateTime.Now.Day,
					Convert.ToInt16(numericUpDown1.Value),
					Convert.ToInt16(numericUpDown2.Value),
					0, 0);
				if (DateTime.Compare(DateTime.Now, alarm) > 0)
					alarm = alarm.AddDays(1);

				notifyIcon1.Text = "Будильник: " + alarm.ToShortTimeString();
			}
			else
			{
				numericUpDown1.Enabled = true;
				numericUpDown2.Enabled = true;
				notifyIcon1.Text = "Будильник не установлен";
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
			this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
			label2.Text = DateTime.Now.ToLongTimeString();
			if (checkBox1.Checked)
			{
				// будильник установлен
				if (DateTime.Compare(DateTime.Now, alarm) > 0)
				{
					checkBox1.Checked = false;							
					sp.Stream = Properties.Resources.ring;
					sp.Play();
					this.Show();
				}
			}
		}

        private void toolStripMenuAbout_Click(object sender, EventArgs e)
        {
			MessageBox.Show("Программа AlarmClock.\n\n" +
				"Простой будильник.\n\n" +
				"Сделано в Microsoft Visual Studio C#\n" +
				"for the Microsoft® .NET Framework\n",
				"AlarmClock " + GetType().Assembly.GetName().Version.ToString(),
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
		}
    }
}
