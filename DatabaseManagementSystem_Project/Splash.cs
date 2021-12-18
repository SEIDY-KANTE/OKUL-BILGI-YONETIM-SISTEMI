using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseManagementSystem_Project
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        int startPoint;
        private void Splash_Load(object sender, EventArgs e)
        {
            startPoint = 4;
            timer1.Start();
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint -=1;
            timer1.Interval = 500;
            if (startPoint == 0)
            {
                timer1.Stop();
                Home home = new Home();
                this.Hide();
                home.Show();
            }
            else
            {
                label2.Text += ".";
            }

            
        }

        private void Exit_label_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Exit_label_MouseMove(object sender, MouseEventArgs e)
        {
            Exit_label.ForeColor = Color.Red;
        }

        private void Exit_label_MouseLeave(object sender, EventArgs e)
        {
            Exit_label.ForeColor = Color.Transparent;
        }
    }
}
