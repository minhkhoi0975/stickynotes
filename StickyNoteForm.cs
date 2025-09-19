using System;
using System.Drawing;
using System.Windows.Forms;

namespace stickynotes
{
    public partial class StickyNoteForm : Form
    {
        #region generalWindow

        public Color[] availableBackgroundColors =
        {
        System.Drawing.Color.FromArgb(176, 204, 235),
        System.Drawing.Color.FromArgb(250, 185, 190),
        System.Drawing.Color.FromArgb(255, 250, 166),
        System.Drawing.Color.FromArgb(115, 201, 195),
        System.Drawing.Color.FromArgb(207, 227, 216),
        };

        private StickyNote _stickyNote = new StickyNote();

        public StickyNoteForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            SetColor(availableBackgroundColors[0], availableBackgroundColors[1]);

            base.OnLoad(e);
        }

        // Make toolstrips appear/disappear when active or unactive
        protected override void OnActivated(EventArgs e)
        {
            this.ControlBox = false;
            this.toolStripTop.Visible = true;
            this.toolStripBottom.Visible = true;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            this.ControlBox = false;
            this.toolStripTop.Visible = false;
            this.toolStripBottom.Visible = false;
        }

        // drag window when toolStrip1 is held
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        private void ToolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                _ = SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // renderer for toolStrip1 to remove border - need to change renderer in designer/onLoad
        public class StickNoteSystemRenderer : ToolStripSystemRenderer
        {
            public StickNoteSystemRenderer() { }
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                if (e.ToolStrip.GetType() == typeof(ToolStrip))
                {
                    // skip render border
                }
                else
                {
                    // do render border
                    base.OnRenderToolStripBorder(e);
                }
            }
        }

        #endregion
        #region colorMenu

        public void ColorButton1_Clicked(object sender, EventArgs e)
        {
            SetColor(availableBackgroundColors[0], availableBackgroundColors[0]);
        }
        public void ColorButton2_Clicked(object sender, EventArgs e)
        {
            SetColor(availableBackgroundColors[1], availableBackgroundColors[1]);
        }
        public void ColorButton3_Clicked(object sender, EventArgs e)
        {
            SetColor(availableBackgroundColors[2], availableBackgroundColors[2]);
        }
        public void ColorButton4_Clicked(object sender, EventArgs e)
        {
            SetColor(availableBackgroundColors[3], availableBackgroundColors[3]);
        }
        public void ColorButton5_Clicked(object sender, EventArgs e)
        {
            SetColor(availableBackgroundColors[4], availableBackgroundColors[4]);
        }

        private void SetColor(Color toolStripColor, Color backgroundColor)
        {
            _stickyNote.BackgroundColor = availableBackgroundColors[0];
            _stickyNote.ToolStripColor = availableBackgroundColors[0];

            toolStripTop.BackColor = toolStripBottom.BackColor = toolStripColor;
            panelText.BackColor = textboxNote.BackColor = backgroundColor;
        }

        private void NewToolStripButton_Clicked(object sender, EventArgs e)
        {
            var myForm = new StickyNoteForm();
            myForm.Show();
        }
        private void CloseToolStripButton_Clicked(object sender, EventArgs e)
        {
            Close();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #region textMethods
        private void BoldButton_Clicked(object sender, EventArgs e)
        {
            var selectionStart = textboxNote.SelectionStart;
            var selectionLength = textboxNote.SelectionLength;
            for (int i = 0; i < selectionLength; i++)
            {
                textboxNote.Select(selectionStart + i, 1);
                if (textboxNote.SelectionFont.Bold)
                {
                    textboxNote.SelectionFont = new Font(textboxNote.SelectionFont, textboxNote.SelectionFont.Style & ~FontStyle.Bold);
                }
                else
                {
                    textboxNote.SelectionFont = new Font(textboxNote.SelectionFont, textboxNote.SelectionFont.Style | FontStyle.Bold);
                }
            }
            textboxNote.SelectionStart = selectionStart;
            textboxNote.SelectionLength = selectionLength;
        }

        private void ItalicButton_Clicked(object sender, EventArgs e)
        {
            var selectionStart = textboxNote.SelectionStart;
            var selectionLength = textboxNote.SelectionLength;

            for (int i = 0; i < selectionLength; i++)
            {
                textboxNote.Select(selectionStart + i, 1);
                if (textboxNote.SelectionFont.Italic)
                {
                    textboxNote.SelectionFont = new Font(textboxNote.SelectionFont, textboxNote.SelectionFont.Style & ~FontStyle.Italic);
                }
                else
                {
                    textboxNote.SelectionFont = new Font(textboxNote.SelectionFont, textboxNote.SelectionFont.Style | FontStyle.Italic);
                }
            }

            textboxNote.SelectionStart = selectionStart;
            textboxNote.SelectionLength = selectionLength;
        }
        private void UnderlineButton_Clicked(object sender, EventArgs e)
        {
            var sel = this.textboxNote.SelectionStart;
            var selLen = this.textboxNote.SelectionLength;
            for (int i = 0; i < selLen; i++)
            {
                this.textboxNote.Select(sel + i, 1);
                if (this.textboxNote.SelectionFont.Underline)
                {
                    this.textboxNote.SelectionFont = new Font(this.textboxNote.SelectionFont, this.textboxNote.SelectionFont.Style & ~FontStyle.Underline);
                }
                else
                {
                    this.textboxNote.SelectionFont = new Font(this.textboxNote.SelectionFont, this.textboxNote.SelectionFont.Style | FontStyle.Underline);
                }
            }
            this.textboxNote.SelectionStart = sel;
            this.textboxNote.SelectionLength = selLen;
        }
        private void StrikeButton_Clicked(object sender, EventArgs e)
        {
            var sel = this.textboxNote.SelectionStart;
            var selLen = this.textboxNote.SelectionLength;
            for (int i = 0; i < selLen; i++)
            {
                this.textboxNote.Select(sel + i, 1);
                if (this.textboxNote.SelectionFont.Strikeout)
                {
                    this.textboxNote.SelectionFont = new Font(this.textboxNote.SelectionFont, this.textboxNote.SelectionFont.Style & ~FontStyle.Strikeout);
                }
                else
                {
                    this.textboxNote.SelectionFont = new Font(this.textboxNote.SelectionFont, this.textboxNote.SelectionFont.Style | FontStyle.Strikeout);
                }
            }
            this.textboxNote.SelectionStart = sel;
            this.textboxNote.SelectionLength = selLen;
        }
        private void BulletButton_Clicked(object sender, EventArgs e)
        {
            if (this.textboxNote.SelectionBullet)
            {
                textboxNote.SelectionBullet = false;
            }
            else
            {
                textboxNote.SelectionBullet = true;
            }

        }

#endregion
    }
}
