using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _300_Hackathon
{
    public partial class fight : UserControl
    {
        public fight()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.textBox2.Text = DateTime.Now.ToString("HH:mm:ss");


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public TextBox GetText1() { return this.textBox1; }
        public TextBox GetText2() { return this.textBox2; }
        public TextBox GetText3() { return this.textBox3; }

        private void fight_Load(object sender, EventArgs e)
        {
            
        }
    }
}
