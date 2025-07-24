using BitMiracle.LibTiff.Classic;
using Flurl.Util;
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
    public partial class ParamMenu : Form
    {
        public ParamMenu()
        {
            InitializeComponent();
            foreach (var kvp in MainV2.comPort.MAV.param.ToKeyValuePairs())
            {
                Console.WriteLine($"Param name: {kvp.Key} Value: {kvp.Value}");
            }
            if (MainV2.comPort.MAV.param.Count() != 0 && MainV2.comPort.BaseStream.IsOpen)
            {
                textBox1.Text = MainV2.comPort.MAV.param["wx_Kp"].ToString();
                textBox2.Text = MainV2.comPort.MAV.param["wx_Ki"].ToString();
                textBox3.Text = MainV2.comPort.MAV.param["wx_Kd"].ToString();
                textBox4.Text = MainV2.comPort.MAV.param["phi_Kp"].ToString();
                textBox5.Text = MainV2.comPort.MAV.param["phi_Ki"].ToString();
                textBox6.Text = MainV2.comPort.MAV.param["phi_Kd"].ToString();
                textBox7.Text = MainV2.comPort.MAV.param["wy_Kp"].ToString();
                textBox8.Text = MainV2.comPort.MAV.param["wy_Ki"].ToString();
                textBox9.Text = MainV2.comPort.MAV.param["wy_Kd"].ToString();
                textBox10.Text = MainV2.comPort.MAV.param["the_Kp"].ToString();
                textBox11.Text = MainV2.comPort.MAV.param["the_Ki"].ToString();
                textBox12.Text = MainV2.comPort.MAV.param["the_Kd"].ToString();
                textBox13.Text = MainV2.comPort.MAV.param["wz_Kp"].ToString();
                textBox14.Text = MainV2.comPort.MAV.param["wz_Ki"].ToString();
                textBox15.Text = MainV2.comPort.MAV.param["wz_Kd"].ToString();
                textBox16.Text = MainV2.comPort.MAV.param["shi_Kp"].ToString();
                textBox17.Text = MainV2.comPort.MAV.param["shi_Ki"].ToString();
                textBox18.Text = MainV2.comPort.MAV.param["shi_Kd"].ToString();
                textBox19.Text = MainV2.comPort.MAV.param["vz_Kp"].ToString();
                textBox20.Text = MainV2.comPort.MAV.param["vz_Ki"].ToString();
                textBox21.Text = MainV2.comPort.MAV.param["vz_Kd"].ToString();
                textBox22.Text = MainV2.comPort.MAV.param["alt_Kp"].ToString();
                textBox23.Text = MainV2.comPort.MAV.param["alt_Ki"].ToString();
                textBox24.Text = MainV2.comPort.MAV.param["alt_Kd"].ToString();
                textBox25.Text = MainV2.comPort.MAV.param["vx_Kp"].ToString();
                textBox26.Text = MainV2.comPort.MAV.param["vx_Ki"].ToString();
                textBox27.Text = MainV2.comPort.MAV.param["vx_Kd"].ToString();
                textBox28.Text = MainV2.comPort.MAV.param["vy_Kp"].ToString();
                textBox29.Text = MainV2.comPort.MAV.param["vy_Ki"].ToString();
                textBox30.Text = MainV2.comPort.MAV.param["vy_Kd"].ToString();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Parameters requested");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox1.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting wx_Kp to {value}");
                MainV2.comPort.setParam("wx_Kp", value);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox2.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting wx_Kp to {value}");
                MainV2.comPort.setParam("wx_Kp", value);
            }
        }

    }
}
