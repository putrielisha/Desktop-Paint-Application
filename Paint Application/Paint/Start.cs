using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Doodle_231962J
{
    public partial class Start : Form
    {             
        public Start()
        {
            InitializeComponent();

            this.BackColor = Color.White;           
            this.TransparencyKey = Color.White;
            startsound();
        }
        private SoundPlayer player;
        private void startsound()
        {
            MemoryStream ms = new MemoryStream(Properties.Resources.sound);
            player = new SoundPlayer(ms);  
            player.PlayLooping();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {                   
            MainForm_231962J frm = new MainForm_231962J();
            frm.Show();
            this.Hide();
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
            Application.Exit();
        }
    }
}
