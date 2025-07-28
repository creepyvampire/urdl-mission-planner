using BitMiracle.LibTiff.Classic;
using DeviceProgramming;
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
        Int32 sysid = MainV2.comPort.sysidcurrent;
        Int32 compid = MainV2.comPort.compidcurrent;
        public ParamMenu()
        {

            InitializeComponent();

            foreach (var kvp in MainV2.comPort.MAV.param.ToKeyValuePairs())
            {
                Console.WriteLine($"Param name: {kvp.Key} Value: {kvp.Value}");
            }
            if (MainV2.comPort.MAV.param.Count() != 0 && MainV2.comPort.BaseStream.IsOpen)
            {
                refreshParams();
            }
        }

        private void refreshParams()
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
            MessageBox.Show("Params Refreshed");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshParams();
        }

        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
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
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "wx_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox2_KeyDown(object sender, KeyEventArgs e)
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

                Console.WriteLine($"Setting wx_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "wx_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox3.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting wx_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "wx_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox4.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting phi_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "phi_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox5.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting phi_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "phi_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox6.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting phi_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "phi_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox7.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting wy_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "wy_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox8.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting wy_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "wy_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox9.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting wy_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "wy_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox10.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting the_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "the_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox11.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting the_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "the_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox12.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting the_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "the_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox13.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting wz_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "wz_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox14.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting wz_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "wz_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox15.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting wz_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "wz_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox16_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox16.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting shi_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "shi_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox17_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox17.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting shi_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "shi_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox18_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox18.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting shi_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "shi_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox19.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vz_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vz_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox20_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox20.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vz_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vz_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox21_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox21.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vz_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vz_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox22_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox22.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting alt_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "alt_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox23_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox23.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting alt_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "alt_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox24_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox24.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting alt_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "alt_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox25_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox25.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vx_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vx_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox26_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox26.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vx_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vx_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox27_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox27.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vx_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vx_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox28_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox28.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vy_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vy_Kp", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox29_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox29.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vy_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vy_Ki", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void textBox30_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = textBox30.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vy_Kd to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vy_Kd", value);
                        label14.Text = "Parameter Successfully Updated";
                        await Task.Delay(5000);
                        label14.Text = "";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        continue;
                    }
                }
                MessageBox.Show("Parameter update operation failed. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
