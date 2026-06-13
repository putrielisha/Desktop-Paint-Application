using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Doodle_231962J
{
    public partial class MainForm_231962J : Form
    {
        Bitmap bm;
        Graphics g;

        Pen pen = new Pen(Color.Black, 5);
        SolidBrush brush = new SolidBrush(Color.Black);
        Point startP = new Point(0, 0);
        Point endP = new Point(0, 0);
        Point cursor;

        bool flagDraw = false;
        bool canDraw = false;
        bool flagErase = false;
        bool flagText = false;
        bool flagFill = false;
        bool flagCircle = false;
        bool flagRectangle = false;
        bool flagLine = false;
        bool smileybrush = false;
        bool sadbrush = false;
        bool angrybrush = false;
        bool scaredbrush = false;
        bool lovebrush = false;

        string strText;
        int eraserSize;
        int EraseSizeNumber;
        int PenSizeNumber;               
        int x, y, sX, sY, cX, cY;
        
        public MainForm_231962J()
        {
            InitializeComponent();
        }
        private void putriElishaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            Clipboard.SetText(attribute.Value.ToString());
        }
        private void MainForm_231962J_Load(object sender, EventArgs e)
        {
            bm = new Bitmap(picBoxMain.Width, picBoxMain.Height);
            picBoxMain.Image = bm;          

            pen.Width = trkBrushSize.Value;
            eraserSize = trkEraserSize.Value;            
            trkBrushSize.Value = 25;
            txtBoxESize.Text = "25";
            txtBoxPSize.Text = "25";
            trkEraserSize.Value = 25;
            lblXYpos.Text = "X:0     Y:0"; // update status bar

            foreach (FontFamily font in FontFamily.Families)
            {
                cmbBoxFont.Items.Add(font.Name);
            }
            cmbBoxFont.SelectedItem = "Arial";
            cmbBoxSize.Items.AddRange(new object[] { 8, 10, 12, 14, 16, 18, 20, 30, 50 });
            cmbBoxSize.SelectedIndex = 2;
        }
        private void picBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            startP = e.Location;
            if (canDraw == true && !flagText && !flagFill && !flagCircle && !flagRectangle && !flagLine)
            {
                if (e.Button == MouseButtons.Left)
                    flagDraw = true;
            }
            else if (flagFill == true)
            {
                int x = e.X;    // Get pixel location
                int y = e.Y;
                Color newColor = picBoxBrushColor.BackColor;  // Get the selected brush color               
                Fill(bm, x, y, newColor);   // Perform the fill operation         
                picBoxMain.Invalidate();
            }
            else if (flagText == true)
            {
                strText = txtBoxText.Text;
                string selectedFont = cmbBoxFont.SelectedItem.ToString();
                int fontSize = Convert.ToInt32(cmbBoxSize.SelectedItem ?? 12);
                g = Graphics.FromImage(bm);
                Font font = new Font(selectedFont, fontSize);
                g.DrawString(strText, font, brush, startP.X, startP.Y);
                g.Dispose();
                picBoxMain.Invalidate();
            }            
            cX = e.X;
            cY = e.Y;
        }
       
        private void picBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (canDraw && flagDraw)
            {
                endP = e.Location;
                g = Graphics.FromImage(bm);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (flagErase == true)
                {
                    Pen eraser = new Pen(picBoxMain.BackColor, eraserSize);
                    eraser.StartCap = LineCap.Round;
                    eraser.EndCap = LineCap.Round;
                    g.FillEllipse(new SolidBrush(picBoxMain.BackColor), endP.X, endP.Y, eraserSize, eraserSize);
                }
                else
                {
                    if (smileybrush == true)
                    {
                        Image smiley = Properties.Resources.happy;
                        g.DrawImage(smiley, endP.X, endP.Y, pen.Width, pen.Width);
                    }
                    else if (sadbrush == true)
                    {
                        Image crying = Properties.Resources.crying;
                        g.DrawImage(crying, endP.X, endP.Y, pen.Width, pen.Width);
                    }
                    else if (angrybrush == true)
                    {
                        Image angry = Properties.Resources.angry; 
                        g.DrawImage(angry, endP.X, endP.Y, pen.Width, pen.Width); 
                    }
                    else if (scaredbrush == true)
                    {
                        Image scared = Properties.Resources.scared; 
                        g.DrawImage(scared, endP.X, endP.Y, pen.Width, pen.Width); 
                    }
                    else if (lovebrush == true)
                    {
                        Image love = Properties.Resources.love; 
                        g.DrawImage(love, endP.X, endP.Y, pen.Width, pen.Width); 
                    }
                    else if (canDraw == true)
                    {
                        g.FillEllipse(brush, endP.X, endP.Y, pen.Width, pen.Width);
                        pen.Color = brush.Color;
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                    }                  
                }
                g.Dispose();
                picBoxMain.Invalidate();
            }
            startP = endP;
            cursor = this.PointToClient(Cursor.Position); // get cursor position
            lblXYpos.Text = "X:" + (cursor.X - 15) + "   Y:" + (cursor.Y - 15); // update status bar
            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;
        }
        private void picBoxMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (canDraw)
            {
                flagDraw = false;
            }
            sX = x - cX;
            sY = y - cY;
            g = Graphics.FromImage(bm);
            pen.Color = picBoxBrushColor.BackColor;

            if (flagCircle == true)
            {              
                g.DrawEllipse(pen, cX, cY, sX, sY);
            }
            else if (flagRectangle == true)
            {
                g.DrawRectangle(pen, cX, cY, sX, sY);
            }
            else if (flagLine == true)
            {
                g.DrawLine(pen, cX, cY, x, y);
            }
            g.Dispose();
            picBoxMain.Invalidate();
        }
        private void picBoxColor_Click(object sender, EventArgs e)
        {
            PictureBox clickedBox = (PictureBox)sender;
            if (clickedBox != null)
            {
                brush.Color = clickedBox.BackColor;
                picBoxBrushColor.BackColor = clickedBox.BackColor;
                picBoxBrushColor.Image = null;
            }
        }
        private void pixBoxBrush_Click(object sender, EventArgs e)
        {
            canDraw = true;
            flagErase = false;
            flagText = false;
            flagCircle = false;
            flagFill = false;
            flagRectangle = false;
            flagLine = false;
            smileybrush = false;
            sadbrush = false;
            angrybrush = false;
            scaredbrush = false;
            lovebrush = false;
        
            brush.Color = picBoxBrushColor.BackColor;
            pen.Width = trkBrushSize.Value;
            picBoxAction.Image = Properties.Resources.brush;
            lblTool.Text = "Brush";
        }
        private void picBoxErase_Click(object sender, EventArgs e)
        {
            brush = new SolidBrush(picBoxMain.BackColor);
            picBoxAction.Image = Properties.Resources.erase;

            flagErase = true;
            flagText = false;
            flagCircle= false;
            flagRectangle= false;
            flagLine = false;
            eraserSize = trkEraserSize.Value;
            lblTool.Text = "Erase";
        }
        private void picBoxClear_Click(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bm);
            Rectangle rect = picBoxMain.ClientRectangle;
            g.FillRectangle(new SolidBrush(Color.GhostWhite), rect);
            g.Dispose();
            picBoxMain.Invalidate();          
        }
        private void picBoxText_Click(object sender, EventArgs e)
        {
            picBoxAction.Image = Properties.Resources.text;
            flagDraw = false;
            flagErase = false;
            flagFill = false;
            flagText = true;
            smileybrush = false;
            sadbrush = false;
            angrybrush = false;
            scaredbrush = false;
            lovebrush = false;
            flagLine = false;
            flagCircle = false;
            flagRectangle = false;
            brush.Color = picBoxBrushColor.BackColor;
            lblTool.Text = "Text";
        }
        private void trkBrushSize_Scroll(object sender, EventArgs e)
        {
            if (canDraw == true)
            {
                pen.Width = trkBrushSize.Value;
                PenSizeNumber = trkBrushSize.Value;
                txtBoxPSize.Text = PenSizeNumber.ToString();              
            }
        }
        private void trkEraserSize_Scroll(object sender, EventArgs e)
        {
            if (flagErase == true)
            {
                eraserSize = trkEraserSize.Value;
                EraseSizeNumber = trkEraserSize.Value;
                txtBoxESize.Text = EraseSizeNumber.ToString();               
            }
        }
        private void picBoxSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfdlg = new SaveFileDialog())
            {
                sfdlg.Title = "Save Dialog";
                sfdlg.Filter = "Image Files(*.BMP)|*.BMP|All files (*.*)|*.*";
                if (sfdlg.ShowDialog(this) == DialogResult.OK)
                {
                    using (Bitmap bmp = new Bitmap(picBoxMain.Width, picBoxMain.Height))
                    {
                        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                        picBoxMain.DrawToBitmap(bmp, rect);
                        bmp.Save(sfdlg.FileName, ImageFormat.Bmp);
                        MessageBox.Show("File Saved Successfully");
                    }
                    picBoxAction.Image = Properties.Resources.save;
                    lblTool.Text = "Save";
                }
            }
        }
        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) // opens color spectrum
            {
                brush.Color = colorDialog.Color;
                picBoxBrushColor.BackColor = colorDialog.Color;
                pen.Color = picBoxBrushColor.BackColor;
            }
        }
        private void picBoxLoad_Click(object sender, EventArgs e)
        {
            picBoxAction.Image = Properties.Resources.load;
            using (OpenFileDialog lfdlg = new OpenFileDialog())
            {
                lfdlg.Title = "Load Dialog";    // name FileLoad Dialog box title

                //set FileLoad Dialog filter type = "Image type descriptor | image file type | Image type descriptor | image file type";
                lfdlg.Filter = "Image Files(*.BMP)|*.BMP|Image Files(All files (*.*))|*.*";

                //Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|

                if (lfdlg.ShowDialog(this) == DialogResult.OK)
                {
                    Image img = Image.FromFile(lfdlg.FileName);
                    g = Graphics.FromImage(bm); //Creates a new Graphics from the specified image
                    g.DrawImage(img, picBoxMain.ClientRectangle);   // Draws the specified image, using its original physical size, at the specified location
                    picBoxMain.Refresh();   // put the specified image onto pixBoxMain
                }
                picBoxAction.Image = Properties.Resources.load;
                lblTool.Text = "Load";
            }
        }
        public void Fill(Bitmap bm, int x, int y, Color newColor)   // flood fill, queue based approach (faster, no lag)
        {
            flagFill = true;
            Color oldColor = bm.GetPixel(x, y); // get original color
            if (oldColor.ToArgb() == newColor.ToArgb()) return; // if color is the same, don't do anything

            Queue<Point> queue = new Queue<Point>(); // a queue to store and check pixels
            queue.Enqueue(new Point(x, y));

            while (queue.Count > 0) // continues running until no pixels left to fill
            {
                Point pt = queue.Dequeue(); // retrieves next pixel from queue
                if (pt.X < 0 || pt.Y < 0 || pt.X >= bm.Width || pt.Y >= bm.Height) continue;

                if (bm.GetPixel(pt.X, pt.Y).ToArgb() == oldColor.ToArgb())
                {
                    bm.SetPixel(pt.X, pt.Y, newColor);
                    queue.Enqueue(new Point(pt.X - 1, pt.Y));   // add top, bottom, left, right pixels to queue
                    queue.Enqueue(new Point(pt.X + 1, pt.Y));
                    queue.Enqueue(new Point(pt.X, pt.Y - 1));
                    queue.Enqueue(new Point(pt.X, pt.Y + 1));
                }
            }
        }
        private void picBoxFill_Click(object sender, EventArgs e)
        {
            flagFill = true;
            flagCircle = false;
            flagRectangle = false;
            smileybrush = false;
            sadbrush = false;
            angrybrush = false;
            scaredbrush = false;
            lovebrush = false;
            flagLine = false;
            picBoxAction.Image = Properties.Resources.fill;
            lblTool.Text = "Fill";
        }
        private void picBoxLine_Click(object sender, EventArgs e)
        {
            flagLine = true;
            flagRectangle = false;
            flagCircle = false;
            flagFill = false;
            smileybrush = false;
            sadbrush = false;
            angrybrush = false;
            scaredbrush = false;
            lovebrush = false;
            flagText = false;
            picBoxAction.Image = Properties.Resources.line;
            lblTool.Text = "Line";
        }
        private void picBoxCircle_Click(object sender, EventArgs e)
        {
            flagCircle = true;
            flagRectangle = false;
            flagFill = false;
            flagLine = false;
            flagText = false;
            
            picBoxAction.Image = Properties.Resources.ellipse;
            lblTool.Text = "Ellipse";
        }
        private void picBoxCampus_Click(object sender, EventArgs e)
        {
            if (picBoxCampus.Image != null)
            {              
                using (Graphics g = Graphics.FromImage(bm))
                {
                    // Draw the image stretched to fill the entire canvas
                    g.DrawImage(picBoxCampus.Image, picBoxMain.ClientRectangle);
                    picBoxMain.Refresh();
                }                
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfdlg = new SaveFileDialog())
            {
                sfdlg.Title = "Save Dialog";
                sfdlg.Filter = "Image Files(*.BMP)|*.BMP|All files (*.*)|*.*";
                if (sfdlg.ShowDialog(this) == DialogResult.OK)
                {
                    using (Bitmap bmp = new Bitmap(picBoxMain.Width, picBoxMain.Height))
                    {
                        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                        picBoxMain.DrawToBitmap(bmp, rect);
                        bmp.Save(sfdlg.FileName, ImageFormat.Bmp);
                        MessageBox.Show("File Saved Successfully");
                    }
                }
            }
        }               
        private void picBoxColor1_Click(object sender, EventArgs e)
        {
            if (picBoxColor1.Image != null)
            {
                using (Graphics g = Graphics.FromImage(bm))
                {
                    // Draw the image stretched to fill the entire canvas
                    g.DrawImage(picBoxColor1.Image, picBoxMain.ClientRectangle);
                    picBoxMain.Refresh();
                }
            }
        }
        private void picBoxColor2_Click(object sender, EventArgs e)
        {
            if (picBoxColor2.Image != null)
            {
                using (Graphics g = Graphics.FromImage(bm))
                {
                    // Draw the image stretched to fill the entire canvas
                    g.DrawImage(picBoxColor2.Image, picBoxMain.ClientRectangle);
                    picBoxMain.Refresh();
                }
            }
        }
        private void ptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flagErase == true)
            {
                eraserSize = 10;
            }
            else if (canDraw == true)
            {
                pen.Width = 10;
            }
        }
        private void ptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flagErase == true)
            {
                eraserSize = 20;
            }
            else if (canDraw == true)
            {
                pen.Width = 20;
            }
        }
        private void ptsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (flagErase == true)
            {
                eraserSize = 30;
            }
            else if (canDraw == true)
            {
                pen.Width = 30;
            }
        }
        private void ptsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (flagErase == true)
            {
                eraserSize = 50;
            }
            else if (canDraw == true)
            {
                pen.Width = 50;
            }
        }
        private void picBoxPlaid_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.plaidback;
        }
        private void picBoxBlueback_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.blueback;
        }
        private void picBoxGWC_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.wcback;
        }
        private void picBoxcheckered_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.pink;
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            BackgroundImage = null;
            BackColor = Color.Moccasin;
        }
        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bm == null)
                return;
            
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {                  
                    Color col = bm.GetPixel(i, j);    // Get the color of the current pixel            
                    int gray = (int)((col.R + col.G + col.B) / 3);

                    bm.SetPixel(i, j, Color.FromArgb(gray, gray, gray));    // Set the pixel to the new grayscale color
                }
            }           
            picBoxMain.Image = bm;
            picBoxMain.Invalidate();  
        }
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bm == null)
                return;
            
            Color filterColor = brush.Color;  // Replace with user's selected color
            
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {                   
                    Color originalColor = bm.GetPixel(i, j); // Get the current pixel's color
                   
                    int r = (originalColor.R + filterColor.R) / 2; // Apply the color filter
                    int g = (originalColor.G + filterColor.G) / 2;
                    int b = (originalColor.B + filterColor.B) / 2;
                    
                    bm.SetPixel(i, j, Color.FromArgb(r, g, b)); 
                }
            }           
            picBoxMain.Image = bm;
            picBoxMain.Invalidate();  
        }
        private void smilingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            smileybrush = true;
            sadbrush = false;
            angrybrush = false;
            scaredbrush = false;
            lovebrush = false;
            flagText = false;
            flagFill = false;
            flagCircle = false;
            flagLine = false;
            flagRectangle = false;

            picBoxAction.Image = Properties.Resources.happy;
            lblTool.Text = "Smiling";
            
        }
        private void cryingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sadbrush = true;
            angrybrush = false;
            scaredbrush = false;
            lovebrush = false;
            smileybrush = false;
            flagText = false;
            flagFill = false;
            flagCircle = false;
            flagLine = false;
            flagRectangle = false;

            picBoxAction.Image = Properties.Resources.crying;
            lblTool.Text = "Crying";

        }
        private void angryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            angrybrush = true;
            scaredbrush = false;
            lovebrush = false;
            smileybrush = false;
            sadbrush = false;
            flagText = false;
            flagFill = false;
            flagCircle = false;
            flagLine = false;
            flagRectangle = false;

            picBoxAction.Image = Properties.Resources.angry;
            lblTool.Text = "Angry";
        }

        private void scaredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scaredbrush = true;
            lovebrush = false;
            smileybrush = false;
            sadbrush = false;
            angrybrush = false;
            flagText = false;
            flagFill = false;
            flagCircle = false;
            flagLine = false;
            flagRectangle = false;

            picBoxAction.Image = Properties.Resources.scared;
            lblTool.Text = "Scared";
        }
        private void loveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lovebrush = true;
            smileybrush = false;
            sadbrush = false;
            angrybrush = false;
            scaredbrush = false;
            flagText = false;
            flagFill = false;
            flagCircle = false;
            flagLine = false;
            flagRectangle = false;

            picBoxAction.Image = Properties.Resources.love;
            lblTool.Text = "Love";
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog lfdlg = new OpenFileDialog())
            {
                lfdlg.Title = "Load Dialog";    // name FileLoad Dialog box title

                //set FileLoad Dialog filter type = "Image type descriptor | image file type | Image type descriptor | image file type";
                lfdlg.Filter = "Image Files(*.BMP)|*.BMP|Image Files(All files (*.*))|*.*";

                //Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|

                if (lfdlg.ShowDialog(this) == DialogResult.OK)
                {
                    Image img = Image.FromFile(lfdlg.FileName);
                    g = Graphics.FromImage(bm); //Creates a new Graphics from the specified image
                    g.DrawImage(img, picBoxMain.ClientRectangle);   // Draws the specified image, using its original physical size, at the specified location
                    picBoxMain.Refresh();   // put the specified image onto pixBoxMain
                }               
            }
        }        
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bm != null) 
            {               
                var result = MessageBox.Show("Do you want to save your work before creating a new canvas?",
                                             "Save Changes",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes) // prompts user to save file
                {
                    using (SaveFileDialog sfdlg = new SaveFileDialog())
                    {
                        sfdlg.Title = "Save Dialog";
                        sfdlg.Filter = "Image Files(*.BMP)|*.BMP|All files (*.*)|*.*";
                        if (sfdlg.ShowDialog(this) == DialogResult.OK)
                        {
                            using (Bitmap bmp = new Bitmap(picBoxMain.Width, picBoxMain.Height))
                            {
                                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                                picBoxMain.DrawToBitmap(bmp, rect);
                                bmp.Save(sfdlg.FileName, ImageFormat.Bmp);
                                MessageBox.Show("File Saved Successfully");
                            }
                        }
                    }
                }
                else if (result == DialogResult.No) //if user doesn't want to save file
                {
                    g = Graphics.FromImage(bm);
                    Rectangle rect = picBoxMain.ClientRectangle;
                    g.FillRectangle(new SolidBrush(Color.GhostWhite), rect);
                    BackgroundImage = null;
                    BackColor = Color.Moccasin;
                    g.Dispose();
                    picBoxMain.Invalidate();
                }                
            }
            else // new canvas
            {

                g = Graphics.FromImage(bm);
                Rectangle rect = picBoxMain.ClientRectangle;
                g.FillRectangle(new SolidBrush(Color.GhostWhite), rect);
                BackgroundImage = null;
                BackColor = Color.Moccasin;
                g.Dispose();
                picBoxMain.Invalidate();            
            }
            BackColor = Color.Moccasin;
        }
        private void picBoxRectangle_Click(object sender, EventArgs e)
        {
            flagRectangle = true;
            flagCircle = false;
            flagFill = false;
            flagLine = false;
            flagText = false;
            picBoxAction.Image = Properties.Resources.quadrilateral;
        }                         
    }
}
