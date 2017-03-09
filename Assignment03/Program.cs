using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment03
{
    public class Program : Engine
    {

        public static SlideSprite bb;


        protected override void OnKeyDown(KeyEventArgs e)
        {
            //base.OnKeyDown(e);
            //Console.WriteLine("asdffasdf");
            if (e.KeyCode == Keys.Left) bb.TargetX -= 30;
            if (e.KeyCode == Keys.Right) bb.TargetX += 30;
            if (e.KeyCode == Keys.Up) bb.TargetY -= 30;
            if (e.KeyCode == Keys.Down) bb.TargetY += 30;
            if (e.KeyCode == Keys.A) parent.add(bb);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bb = new SlideSprite(Properties.Resources.bb);
            bb.TargetX = 100;
            bb.TargetY = 100;
            bb.Velocity = 5;
            Application.Run(new Program());
        }
    }
}