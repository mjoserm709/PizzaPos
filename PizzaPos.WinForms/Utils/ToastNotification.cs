using System;
using System.Drawing;
using System.Windows.Forms;

namespace PizzaPos.WinForms.Utils
{
    public class ToastNotification : Form
    {
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Timer _animationTimer;
        private int _duration = 4000;
        private int _targetY;
        private Panel _pnlProgress;
        private int _elapsed = 0;

        public ToastNotification(string title, string message, Color accentColor, int duration = 4000)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.White;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.Opacity = 0;
            _duration = duration;

            Panel pnlAccent = new Panel { BackColor = accentColor, Dock = DockStyle.Left, Width = 6 };
            this.Controls.Add(pnlAccent);

            _pnlProgress = new Panel { BackColor = accentColor, Dock = DockStyle.Bottom, Height = 3, Width = 0 };
            this.Controls.Add(_pnlProgress);

            Label lblTitle = new Label
            {
                Text = title,
                ForeColor = Color.FromArgb(45, 45, 48),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(15, 10),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            Label lblMessage = new Label
            {
                Text = message,
                ForeColor = Color.FromArgb(100, 100, 100),
                Font = new Font("Segoe UI", 9.5f),
                Location = new Point(15, 35),
                Width = 310,
                AutoSize = false
            };
            
            using (Graphics g = this.CreateGraphics())
            {
                SizeF size = g.MeasureString(message, lblMessage.Font, 310);
                lblMessage.Height = (int)size.Height + 5;
                this.Width = 350;
                this.Height = Math.Max(80, lblMessage.Bottom + 15);
            }
            this.Controls.Add(lblMessage);

            _timer = new System.Windows.Forms.Timer { Interval = 50 };
            _timer.Tick += (s, e) => {
                _elapsed += 50;
                if (this.IsDisposed) return;
                _pnlProgress.Width = (int)((double)_elapsed / _duration * this.Width);
                if (_elapsed >= _duration) this.Close();
            };
            
            _animationTimer = new System.Windows.Forms.Timer { Interval = 10 };
            _animationTimer.Tick += (s, e) => {
                if (this.Opacity < 1) this.Opacity += 0.1;
                if (this.Location.Y > _targetY) this.Location = new Point(this.Location.X, this.Location.Y - 2);
                else _animationTimer.Stop();
            };

            this.Load += ToastNotification_Load;
            this.Paint += (s, e) => {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(230, 230, 230)), 0, 0, this.Width - 1, this.Height - 1);
            };
        }

        private void ToastNotification_Load(object? sender, EventArgs e)
        {
            Rectangle workingArea = Screen.PrimaryScreen?.WorkingArea ?? Screen.GetWorkingArea(this);
            _targetY = 20;
            this.Location = new Point(workingArea.Width - this.Width - 20, _targetY + 30);
            _animationTimer.Start();
            _timer.Start();
        }

        public static void Success(string message) 
            => new ToastNotification("¡Éxito!", message, Color.FromArgb(40, 167, 69)).Show();

        public static void Error(string message) 
            => new ToastNotification("Error", message, Color.FromArgb(220, 53, 69)).Show();

        public static void Info(string message) 
            => new ToastNotification("Información", message, Color.FromArgb(0, 123, 255)).Show();
    }
}
