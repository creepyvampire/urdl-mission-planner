using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissionPlanner
{
    public partial class ButtonMenu : Form
    {
        bool _isFailsafeEnabled = false;
        bool _isArmed = false;
        public ButtonMenu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!_isArmed)
            {
                _isArmed=true;
                MainV2.comPort.doARM(true, false);
                button2.Text = "DISARM";
                button2.BackColor = Color.Red;
                button2.ForeColor = Color.White;
                MessageBox.Show("Vehicle ARMED", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _isArmed = false;
                MainV2.comPort.doARM(false,false);
                button2.Text = "ARM";
                button2.BackColor = Color.Green;
                button2.ForeColor = Color.Black;
                MessageBox.Show("Vehicle DISARMED", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            //MainV2.comPort.doARM(true, false);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vehicle set to IDLE", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double takeoffAlt;
            double.TryParse(textBox1.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out takeoffAlt);
            MessageBox.Show($"Takeoff to altitide: {takeoffAlt}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Landing Mode Initiated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Return to home initiated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!_isFailsafeEnabled)
            {
                _isFailsafeEnabled = true;
                button5.Text = "Disable Failsafe";
                button5.BackColor = Color.Red;
                button5.ForeColor = Color.White;
                MessageBox.Show("Failsafe Enabled", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _isFailsafeEnabled = false;
                button5.Text = "Enable Failsafe";
                button5.BackColor = Color.Green;
                button5.ForeColor = Color.Black;
                MessageBox.Show("Failsafe Disbaled", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void ButtonMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
