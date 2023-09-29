using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Paint
{
    public partial class PaintForm : Form
    {
        private bool flag = false;
        private Point prevPoint;
        private Color penColor;

        public PaintForm()
        {
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                Button button = new Button();
                button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                button.BackColor = colors[i];
                button.Location = new System.Drawing.Point(3, 3);
                button.Size = new System.Drawing.Size(26, 26);
                button.TabIndex = i;
                button.UseVisualStyleBackColor = false;
                button.Click += ColorButton_Click;
                ColorButtons[i] = button;
            }
            foreach (var a in ColorButtons)
            {
                this.flowLayoutPanel1.Controls.Add(a);
            }
            penColor = ColorButtons[0].BackColor;
        }
        private void ColorButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            penColor = (Color)button.BackColor;
        }
        private void InfoItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\t\tО программе\n\nразработчик : Bat0n\nверсия : 0.1\nподдержать проект : +7 961 180 94 84 (сбер.)");
        }

        private void InfoItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("связаться с разработчиком\n-------------------------------------\nтелефон : +7 961 180 94 84\nпочта : vlaknig@gmail.com\n");
        }

        private void MenuItem3_Click(object sender, EventArgs e)
        {
            int width = panel2.Size.Width;
            int height = panel2.Size.Height;

            Bitmap bm = new Bitmap(width, height);
            //panel2.DrawToBitmap(bm, new Rectangle(0, 0, width, height));
            panel2.DrawToBitmap(bm, panel2.ClientRectangle);
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            sf.ShowDialog();
            var path = sf.FileName;
            bm.Save(path, ImageFormat.Bmp);
            
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
            flag = true;
            
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            Color color = penColor;
            using (Pen p = new Pen(color, 2))
            {
                if (flag)
                {
                    Graphics g = panel2.CreateGraphics();
                    int x, y;
                    if(e.Location.X < prevPoint.X) x = prevPoint.X + 1;
                    else x = prevPoint.X - 1;
                    if (e.Location.Y < prevPoint.Y) y = prevPoint.Y + 1;
                    else y = prevPoint.Y - 1;
                    Point a = new Point(x, y);
                    g.DrawLine(p, a, e.Location);
                    prevPoint = e.Location;
                }
            }
        }
    }
}