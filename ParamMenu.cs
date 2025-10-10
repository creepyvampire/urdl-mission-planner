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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MissionPlanner
{
    public partial class ParamMenu : Form
    {
        private Thread attitudeThread;
        Int32 sysid = MainV2.comPort.sysidcurrent;
        Int32 compid = MainV2.comPort.compidcurrent;

        bool isBiasSet=false;
        double biasRoll, biasPitch, biasYaw;
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
            attitudeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        var packet = MainV2.comPort.MAV.getPacket((uint)MAVLink.MAVLINK_MSG_ID.ATTITUDE);
                        if (packet != null)
                        {
                            var attitude = packet.ToStructure<MAVLink.mavlink_attitude_t>();

                            // Marshal the update to the UI thread
                            this.BeginInvoke((MethodInvoker)(() =>
                            {
                                textBox31.Text = (attitude.roll*57.3).ToString("F4");
                                textBox32.Text = (attitude.pitch*57.3).ToString("F4");
                                textBox33.Text = (attitude.yaw * 57.3).ToString("F4");
                                if (!isBiasSet)
                                {
                                    textBox34.Text = (attitude.roll * 57.3).ToString("F4");
                                    textBox35.Text = (attitude.pitch * 57.3).ToString("F4");
                                    textBox36.Text = (attitude.yaw * 57.3).ToString("F4");
                                }
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        
                        // Optional: Log or ignore
                    }

                    Thread.Sleep(100); // Limit UI update rate to ~10 Hz
                }
            });

            attitudeThread.IsBackground = true;
            attitudeThread.Start();
        }

        private void refreshParams()
        {
            try
            {
                //Gains
                tb_wx_Kp.Text = MainV2.comPort.MAV.param["wx_Kp"].ToString();
                tb_wx_Ki.Text = MainV2.comPort.MAV.param["wx_Ki"].ToString();
                tb_wx_Kd.Text = MainV2.comPort.MAV.param["wx_Kd"].ToString();
                tb_phi_Kp.Text = MainV2.comPort.MAV.param["phi_Kp"].ToString();
                tb_phi_Ki.Text = MainV2.comPort.MAV.param["phi_Ki"].ToString();
                tb_phi_Kd.Text = MainV2.comPort.MAV.param["phi_Kd"].ToString();
                tb_wy_Kp.Text = MainV2.comPort.MAV.param["wy_Kp"].ToString();
                tb_wy_Ki.Text = MainV2.comPort.MAV.param["wy_Ki"].ToString();
                tb_wy_Kd.Text = MainV2.comPort.MAV.param["wy_Kd"].ToString();
                tb_the_Kp.Text = MainV2.comPort.MAV.param["the_Kp"].ToString();
                tb_the_Ki.Text = MainV2.comPort.MAV.param["the_Ki"].ToString();
                tb_the_Kd.Text = MainV2.comPort.MAV.param["the_Kd"].ToString();
                tb_wz_Kp.Text = MainV2.comPort.MAV.param["wz_Kp"].ToString();
                tb_wz_Ki.Text = MainV2.comPort.MAV.param["wz_Ki"].ToString();
                tb_wz_Kd.Text = MainV2.comPort.MAV.param["wz_Kd"].ToString();
                tb_shi_Kp.Text = MainV2.comPort.MAV.param["shi_Kp"].ToString();
                tb_shi_Ki.Text = MainV2.comPort.MAV.param["shi_Ki"].ToString();
                tb_shi_Kd.Text = MainV2.comPort.MAV.param["shi_Kd"].ToString();
                tb_vz_Kp.Text = MainV2.comPort.MAV.param["vz_Kp"].ToString();
                tb_vz_Ki.Text = MainV2.comPort.MAV.param["vz_Ki"].ToString();
                tb_vz_Kd.Text = MainV2.comPort.MAV.param["vz_Kd"].ToString();
                tb_alt_Kp.Text = MainV2.comPort.MAV.param["alt_Kp"].ToString();
                tb_alt_Ki.Text = MainV2.comPort.MAV.param["alt_Ki"].ToString();
                tb_alt_Kd.Text = MainV2.comPort.MAV.param["alt_Kd"].ToString();
                tb_vx_Kp.Text = MainV2.comPort.MAV.param["vx_Kp"].ToString();
                tb_vx_Ki.Text = MainV2.comPort.MAV.param["vx_Ki"].ToString();
                tb_vx_Kd.Text = MainV2.comPort.MAV.param["vx_Kd"].ToString();
                tb_vy_Kp.Text = MainV2.comPort.MAV.param["vy_Kp"].ToString();
                tb_vy_Ki.Text = MainV2.comPort.MAV.param["vy_Ki"].ToString();
                tb_vy_Kd.Text = MainV2.comPort.MAV.param["vy_Kd"].ToString();
                tb_sxkf.Text = MainV2.comPort.MAV.param["sxkf"].ToString();
                tb_sykf.Text = MainV2.comPort.MAV.param["sykf"].ToString();
                tb_sx_Kp.Text = MainV2.comPort.MAV.param["sx_Kp"].ToString();
                tb_sx_Ki.Text = MainV2.comPort.MAV.param["sx_Ki"].ToString();
                tb_sy_Kp.Text = MainV2.comPort.MAV.param["sy_Kp"].ToString();
                tb_sy_Ki.Text = MainV2.comPort.MAV.param["sy_Ki"].ToString();

                //Limits
                tb_vh_thhv.Text = MainV2.comPort.MAV.param["vh_thhv"].ToString();
                tb_vh_thmax.Text = MainV2.comPort.MAV.param["vh_thmax"].ToString();
                tb_vh_thmin.Text = MainV2.comPort.MAV.param["vh_thmin"].ToString();
                th_vh_maxdTv.Text = MainV2.comPort.MAV.param["vh_maxdTv"].ToString();
                tb_vh_mindTv.Text = MainV2.comPort.MAV.param["vh_mindTv"].ToString();
                tb_vh_dTwx.Text = MainV2.comPort.MAV.param["vh_dTwx"].ToString();
                tb_vh_dTwy.Text = MainV2.comPort.MAV.param["vh_dTwy"].ToString();
                tb_vh_dTwz.Text = MainV2.comPort.MAV.param["vh_dTwz"].ToString();
                tb_vh_phistick.Text = MainV2.comPort.MAV.param["vh_phistick"].ToString();
                tb_vh_thestick.Text = MainV2.comPort.MAV.param["vh_thestick"].ToString();
                th_vh_wzstick.Text = MainV2.comPort.MAV.param["vh_wzstick"].ToString();
                tb_vh_maxalt.Text = MainV2.comPort.MAV.param["vh_maxalt"].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to load parameters, Try again.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshParams();
        }

        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_wx_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_wx_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_wx_Kd.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_phi_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_phi_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_phi_Kd.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_wy_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_wy_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_wy_Kd.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_the_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_the_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_the_Kd.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_wz_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_wz_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_wz_Kd.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_shi_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_shi_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_shi_Kd.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vz_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vz_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vz_Kd.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_alt_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_alt_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_alt_Kd.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vx_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vx_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vx_Kd.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vy_Kp.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vy_Ki.Text.Trim();

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
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vy_Kd.Text.Trim();

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

        private async void textBox37_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_thhv.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_thhv to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_thhv", value);
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

        private async void textBox38_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_thmax.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_thmax to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_thmax", value);
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

        private async void textBox39_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_thmin.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_thmin to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_thmin", value);
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

        private async void textBox40_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = th_vh_maxdTv.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_maxdTv to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_maxdTv", value);
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

        private async void textBox41_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_mindTv.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_mindTv to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_mindTv", value);
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

        private async void textBox42_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_dTwx.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_dTwxto {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_dTwx", value);
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

        private async void textBox43_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_dTwy.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_dTwy to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_dTwy", value);
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

        private async void textBox44_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_dTwz.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_dTwz to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_dTwz", value);
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

        private async void textBox45_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_phistick.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_phistick to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_phistick", value);
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

        private async void textBox46_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_thestick.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_thestick to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_thestick", value);
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

        private async void textBox47_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = th_vh_wzstick.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_wzstick to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_wzstick", value);
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

        private async void textBox48_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_vh_maxalt.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting vh_maxalt to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "vh_maxalt", value);
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


        private void ParamMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            attitudeThread.Abort();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (isBiasSet) { return; }
            if(!MainV2.comPort.BaseStream.IsOpen) { return; }

            isBiasSet = true;
            string roll = textBox34.Text.Trim();
            string pitch = textBox35.Text.Trim();
            string yaw = textBox36.Text.Trim();
            double.TryParse(roll, NumberStyles.Float, CultureInfo.InvariantCulture, out biasRoll);
            double.TryParse(pitch,NumberStyles.Float, CultureInfo.InvariantCulture,out biasPitch);
            double.TryParse(yaw, NumberStyles.Float, CultureInfo.InvariantCulture, out biasYaw);

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "phibias", biasRoll);
                    await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "thebias", biasPitch);
                    await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "shibias", biasYaw);
                    label14.Text = "Biases Set successfully";
                    await Task.Delay(5000);
                    label14.Text = "";
                    return;
                }
                catch { continue; }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isBiasSet = false;
            biasRoll = 0;
            biasPitch = 0;
            biasYaw = 0;

            MessageBox.Show("Biases reset", "Info", MessageBoxButtons.OK);
        }

        private async  void tb_sxkf_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_sxkf.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting sxkf to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "sxkf", value);
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

        private async void tb_sykf_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_sykf.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting sykf to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "sykf", value);
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

        private IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                yield return ctrl;
                foreach (var child in GetAllControls(ctrl))
                    yield return child;
            }
        }

        private async void tb_sx_Kp_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_sx_Kp.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting sx_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "sx_Kp", value);
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

        private async void tb_sx_Ki_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_sx_Ki.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting sx_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "sx_Ki", value);
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

        private async void tb_sy_Kp_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_sy_Kp.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting sy_Kp to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "sy_Kp", value);
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

        private async void tb_sy_Ki_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep

                string input = tb_sy_Ki.Text.Trim();

                if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    MessageBox.Show("Invalid number format. Please enter a valid double.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"Setting sy_Ki to {value}");
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, "sy_Ki", value);
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

        private async void btnUpdateAll_Click(object sender, EventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }
            label14.Text = "Updating parameters...";
            

            foreach (Control ctrl in GetAllControls(this))
            {
                if (ctrl is TextBox tb && tb.Tag != null && tb.Tag.ToString() == "gain")
                {
                    // param name comes from textbox name (strip "tb_")
                    string paramName = tb.Name.StartsWith("tb_") ? tb.Name.Substring(3) : tb.Name;
                    string input = tb.Text.Trim();

                    if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                    {
                        MessageBox.Show(
                            $"Invalid number format in {tb.Name}.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        continue;
                    }

                    Console.WriteLine($"Setting {paramName} to {value}");

                    bool success = false;
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, paramName, value);
                            success = true;
                            break;
                        }
                        catch (TimeoutException)
                        {
                            continue;
                        }
                    }

                    if (!success)
                    {
                        MessageBox.Show(
                            $"Failed to update {paramName} after retries.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }

            label14.Text = "All parameters updated.";
            await Task.Delay(5000);
            label14.Text = "";
        }

        private async void btnUpdateLimits_Click(object sender, EventArgs e)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                return;
            }

            label14.Text = "Updating limits...";

            foreach (Control ctrl in GetAllControls(this))
            {
                if (ctrl is TextBox tb && tb.Tag?.ToString() == "limit")
                {
                    // strip "tb_" if present
                    string paramName = tb.Name.StartsWith("tb_") ? tb.Name.Substring(3) : tb.Name;
                    string input = tb.Text.Trim();

                    if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                    {
                        MessageBox.Show(
                            $"Invalid number format in {tb.Name}.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        continue;
                    }

                    Console.WriteLine($"Setting {paramName} to {value}");

                    bool success = false;
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            await MainV2.comPort.setParamAsync((byte)sysid, (byte)compid, paramName, value);
                            success = true;
                            break;
                        }
                        catch (TimeoutException)
                        {
                            continue;
                        }
                    }

                    if (!success)
                    {
                        MessageBox.Show(
                            $"Failed to update {paramName} after retries.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }

                    // throttle requests so autopilot isn't spammed
                    await Task.Delay(200);
                }
            }

            label14.Text = "Limits updated.";
            await Task.Delay(5000);
            label14.Text = "";
        }

    }
}
