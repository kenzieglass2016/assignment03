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

namespace Assignment03
{
    public class Sprite
    {
        private Sprite parent = null;

        //instance variable
        private float x = 0;

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y = 0;

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        private float scale = 1;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private float rotation = 0;

        /// <summary>
        /// The rotation in degrees.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }


        public List<Sprite> children = new List<Sprite>();


        public void Kill()
        {
            parent.children.Remove(this);
        }

        //methods
        public void render(Graphics g)
        {
            Matrix original = g.Transform.Clone();
            g.TranslateTransform(x, y);
            g.ScaleTransform(scale, scale);
            g.RotateTransform(rotation);
            paint(g);
            foreach (Sprite s in children)
            {
                s.render(g);
            }
            g.Transform = original;
        }

        public void update()
        {
            act();
            foreach (Sprite s in children)
            {
                s.update();
            }
        }

        public virtual void paint(Graphics g)
        {

        }

        public virtual void act()
        {

        }

        public void add(Sprite s)
        {
            s.parent = this;
            children.Add(s);
        }
    }

    public class SlideSprite : Sprite
    {
        public int TargetX = 0;
        public int TargetY = 0;
        public int Velocity = 10;
        public Image image;

        public SlideSprite(Image img)
        {
            image=img;
            X = 0;
            Y = 0;
        }

        public override void act()
        {
            if (X + Velocity < TargetX)
            {
                X += Velocity;
            }
            else if(X - Velocity > TargetX)
            {
                X -= Velocity;
            }
            else if (Math.Abs(X - TargetX) <= Velocity)
            {
                X = TargetX;
            }
            if (Y + Velocity < TargetY)
            {
                Y += Velocity;
            }
            else if (Y - Velocity > TargetY)
            {
                Y -= Velocity;
            }
            else if (Math.Abs(Y - TargetY) <= Velocity)
            {
                Y = TargetY;
            }
            
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(image, X - (image.Width / 5), Y - (image.Height / 5));
        }

       
    }

}