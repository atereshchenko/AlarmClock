using System;
using System.Windows.Forms;
using System.Media;
using System.Drawing;
using System.Threading;

namespace AlarmClock
{
    public partial class Form1 : Form
    {
		private DateTime alarm;
		SoundPlayer sp = new SoundPlayer();
		Monitor monitor = new Monitor();
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
			
			//MessageBox.Show(monitor.Resolution());
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
				timer2.Interval = 50000;
				timer2.Enabled = true;
			}
			else
			{
				numericUpDown1.Enabled = true;
				numericUpDown2.Enabled = true;
				notifyIcon1.Text = "Будильник не установлен";
				timer2.Enabled = false;
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

        private void timer2_Tick(object sender, EventArgs e)
        {
			new Thread(() => TransitionMouseTo(50, 50, 400)).Start();			
		}

		public static void TransitionMouseTo(int x, int y, int durationSecs)
		{
			int frames = 200;
			Monitor monitor = new Monitor();
			PointF vector = new PointF();

			Cursor.Position = new Point(monitor.Width / 2, monitor.Height / 2);

			vector.X = (x - Cursor.Position.X) / frames;
			vector.Y = (y - Cursor.Position.Y) / frames;

			for (int i = 0; i < frames; i++)
			{
				Point pos = Cursor.Position;
				pos.X += Convert.ToInt32(vector.X);
				pos.Y += Convert.ToInt32(vector.Y);
				Cursor.Position = pos;
				Thread.Sleep(durationSecs / frames * 50);
			}
		}
	}
}
