using System;
//using System.IO.Packaging;
using System.Reflection;
using System.Windows.Forms;

namespace MissionPlanner
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            label1.Text = $"BUILD: {ProductVersion}";

            
        }
    }
}