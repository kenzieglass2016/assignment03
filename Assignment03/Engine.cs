using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Assignment03
{
    public partial class Engine : Form
    {
        public static SoundPlayer player = new SoundPlayer(Properties.Resources.takeoff);
        public static Sprite parent;
        public static Engine form;
        public Thread rthread;
        public Thread uthread;
        public static int fps = 60;
        public static double running_fps = 60.0;

        public Engine()
        {
            DoubleBuffered = true;
            InitializeComponent();
            parent = new Sprite();
            form = this;
            rthread = new Thread(new ThreadStart(render));
            uthread = new Thread(new ThreadStart(update));
            rthread.Start();
            uthread.Start();
            parent.add(Program.bb);
            player.PlayLooping();
        }

        public static void render()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                form.Invoke(new MethodInvoker(form.Refresh));
            }
        }

        public static void update()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                parent.update();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(Brushes.Gray, ClientRectangle);
            parent.render(e.Graphics);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            uthread.Abort();
            rthread.Abort();
        }

    }

}
