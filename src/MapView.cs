using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SnakeOJTester
{
    public sealed class MapView : Control
    {
        private GridSnapshot _snapshot;

        public MapView()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            Font = new Font("Consolas", 11f, FontStyle.Bold);
            MinimumSize = new Size(360, 360);
        }

        public GridSnapshot Snapshot
        {
            get { return _snapshot; }
            set
            {
                _snapshot = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.Clear(Color.FromArgb(248, 250, 252));

            if (_snapshot == null)
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(80, 90, 105)))
                using (Font emptyFont = new Font("Microsoft YaHei UI", 12f))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString("暂无地图，请先运行一个测试用例", emptyFont, brush, ClientRectangle, sf);
                }
                return;
            }

            int margin = 12;
            int labelArea = 30;
            int size = Math.Min(ClientSize.Width - labelArea, ClientSize.Height - labelArea) - margin * 2;
            if (size < 100) size = 100;
            int cell = size / 20;
            int board = cell * 20;
            int groupWidth = board + labelArea;
            int groupHeight = board + labelArea;
            int labelLeft = (ClientSize.Width - groupWidth) / 2;
            int labelTop = (ClientSize.Height - groupHeight) / 2;
            int left = labelLeft + labelArea;
            int top = labelTop + labelArea;

            DrawAxes(e.Graphics, labelLeft, labelTop, left, top, board, cell, labelArea);

            using (Brush boardBrush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(boardBrush, left, top, board, board);
            }

            for (int r = 0; r < 20; r++)
            {
                for (int c = 0; c < 20; c++)
                {
                    char ch = _snapshot.Map[r, c];
                    Rectangle rect = new Rectangle(left + c * cell, top + r * cell, cell, cell);
                    using (Brush b = new SolidBrush(CellColor(ch)))
                    {
                        e.Graphics.FillRectangle(b, rect);
                    }

                    if (ch != '.')
                    {
                        DrawCellText(e.Graphics, ch.ToString(), rect, ch);
                    }
                }
            }

            using (Pen gridPen = new Pen(Color.FromArgb(210, 216, 224)))
            {
                for (int i = 0; i <= 20; i++)
                {
                    int x = left + i * cell;
                    int y = top + i * cell;
                    e.Graphics.DrawLine(gridPen, x, top, x, top + board);
                    e.Graphics.DrawLine(gridPen, left, y, left + board, y);
                }
            }

            using (Pen borderPen = new Pen(Color.FromArgb(60, 75, 95), 2f))
            {
                e.Graphics.DrawRectangle(borderPen, left, top, board, board);
            }
        }

        private void DrawAxes(Graphics g, int labelLeft, int labelTop, int boardLeft, int boardTop, int board, int cell, int labelArea)
        {
            using (Brush axisBrush = new SolidBrush(Color.FromArgb(236, 241, 247)))
            using (Pen axisPen = new Pen(Color.FromArgb(190, 199, 212)))
            {
                g.FillRectangle(axisBrush, boardLeft, labelTop, board, labelArea);
                g.FillRectangle(axisBrush, labelLeft, boardTop, labelArea, board);
                g.FillRectangle(axisBrush, labelLeft, labelTop, labelArea, labelArea);
                g.DrawRectangle(axisPen, labelLeft, labelTop, labelArea + board, labelArea + board);

                for (int i = 0; i <= 20; i++)
                {
                    int x = boardLeft + i * cell;
                    int y = boardTop + i * cell;
                    g.DrawLine(axisPen, x, labelTop, x, labelTop + labelArea + board);
                    g.DrawLine(axisPen, labelLeft, y, labelLeft + labelArea + board, y);
                }
            }

            // 200% 缩放下，10~19 是两位数；字体必须比原来更小，否则只会显示成“1”。
            float fontSize = Math.Max(6f, Math.Min(8.5f, cell * 0.26f));
            using (Font axisFont = new Font("Microsoft YaHei UI", fontSize, FontStyle.Regular))
            using (Brush textBrush = new SolidBrush(Color.FromArgb(55, 70, 90)))
            {
                StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.None;

                for (int i = 0; i < 20; i++)
                {
                    RectangleF colRect = new RectangleF(boardLeft + i * cell, labelTop, cell, labelArea);
                    RectangleF rowRect = new RectangleF(labelLeft, boardTop + i * cell, labelArea, cell);
                    g.DrawString(i.ToString(), axisFont, textBrush, colRect, sf);
                    g.DrawString(i.ToString(), axisFont, textBrush, rowRect, sf);
                }
            }
        }

        private void DrawCellText(Graphics g, string text, Rectangle rect, char ch)
        {
            float fontSize = Math.Max(8f, Math.Min(15f, rect.Width * 0.48f));
            using (Font f = new Font("Consolas", fontSize, FontStyle.Bold))
            using (Brush b = new SolidBrush(TextColor(ch)))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(text, f, b, rect, sf);
            }
        }

        private Color CellColor(char ch)
        {
            if (ch == '#') return Color.FromArgb(45, 55, 72);
            if (ch == 'H') return Color.FromArgb(30, 135, 84);
            if (ch == 'B') return Color.FromArgb(125, 211, 168);
            if (ch == 'F') return Color.FromArgb(239, 83, 80);
            if (ch == 'O') return Color.FromArgb(116, 125, 136);
            return Color.FromArgb(255, 255, 255);
        }

        private Color TextColor(char ch)
        {
            if (ch == 'B') return Color.FromArgb(26, 83, 57);
            return Color.White;
        }
    }
}
