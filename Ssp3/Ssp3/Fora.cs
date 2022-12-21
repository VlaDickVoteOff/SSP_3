using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ssp3
{
    public class Fora : System.Windows.Forms.Form

    {
        private System.ComponentModel.Container components = null;

        Random rand; // объект, для получения случайных чисел
        float height;
        float width;
        float x1, x2, diameter, y1, y2, x3, y3;
        float finish_width, finish_height;
        float speedx1, speedx2, speedx3, speedBHx;
        float speedy1, speedy2, speedy3, speedBHy;
        float blackHoleX;
        float blackHoleY;
        bool goUp = false;
        bool goUp2 = true;
        bool goUp3 = true;
        bool goUpBlackHole = true;
        bool goRight1 = true;
        bool right2 = true;
        bool right3 = true;
        bool goRightBlackHole=true ;
        public bool th1 = true;
        public bool th2 = true;
        public bool th3 = true;
        double n = 0;
        private Font fnt = new Font("Arial", 40);

        ArrayList list = new ArrayList();

        public Fora()
        {
            InitializeComponent();
            rand = new Random();
            diameter = 40;
            height = this.Size.Height;
            width = this.Size.Width;
            x1 = x2 = x3 = 0;
            y1 = height / 2;
            y2 = height / 3;
            y3 = 2 * height / 3;
            finish_height = height - 2 * diameter;
            finish_width = width - 3 * diameter / 2;
            speedx1 = (float)rand.Next(5, 10) / 5;
            speedy1 = (float)rand.Next(5, 10) / 5;
            speedx2 = (float)rand.Next(5, 10) / 5;
            speedy2 = (float)rand.Next(5, 10) / 5;
            speedx3 = (float)rand.Next(5, 10) / 5;
            speedy3 = (float)rand.Next(5, 10) / 5;
            speedBHx = (float)rand.Next(1, 2) / 1;
            speedBHy = (float)rand.Next(1, 2) / 1;
            blackHoleX = (float)rand.Next(1, 280) + 40;
            blackHoleY = (float)rand.Next(1, 280) + 40;
            
            //list.Add("Yellow");
           // list.Add("Purple");
           // list.Add("Red");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()

        {

            this.components = new System.ComponentModel.Container();
            this.Size = new System.Drawing.Size(600, 600);

            this.Text = "SpiceX";
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.painting);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.painting1);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.painting2);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.painting3);
            //this.Paint += new System.Windows.Forms.PaintEventHandler(this.painting);
        }

        public void painting(object sender, System.Windows.Forms.PaintEventArgs e)

        {
            e.Graphics.FillEllipse(Brushes.Red, x1, y1, diameter, diameter);
            //if (list.Count == 1)
            //{
            //    e.Graphics.DrawString("win " + list[0],
            //        fnt, System.Drawing.Brushes.White, new Point(300, 300));

            //}

        }

        public void painting1(object sender, System.Windows.Forms.PaintEventArgs e)

        {

            e.Graphics.FillEllipse(Brushes.Purple, x2, y2, diameter, diameter);
            //if (list.Count == 1)
            //{
            //    e.Graphics.DrawString("win " + list[0],
            //        fnt, System.Drawing.Brushes.White, new Point(300, 300));

            //}

        }

        public void painting2(object sender, System.Windows.Forms.PaintEventArgs e)

        {
            e.Graphics.FillEllipse(Brushes.Yellow, x3, y3, diameter, diameter);
            //if (list.Count == 1)
            //{
            //    e.Graphics.DrawString("win " + list[0],
            //        fnt, System.Drawing.Brushes.White, new Point(300, 300));
            //}

        }

        public void painting3(object sender, System.Windows.Forms.PaintEventArgs e)

        {
            e.Graphics.FillEllipse(Brushes.Black, blackHoleX, blackHoleY, diameter * 4f, diameter * 4f);

        }

        public void painting_result(object sender, System.Windows.Forms.PaintEventArgs e)

        {
            if (list.Count+n == 3)
            {

                if (n == 3)
                {
                    e.Graphics.DrawString("No winner... " ,
                        fnt, System.Drawing.Brushes.White, new Point(300, 300));
                }
                else
                {
                    e.Graphics.DrawString("win " + list[0],
                        fnt, System.Drawing.Brushes.White, new Point(300, 300));
                }
            }

        }

        private bool IsNotInBlackHole(float x, float y, int a = 1)
        {

            return !(blackHoleX < x && blackHoleX + 4 * diameter > x + diameter && blackHoleY < y &&
                     blackHoleY + 4 * diameter > y);
        }

        public void cycl1()
        {
            while (IsNotInBlackHole(x1, y1, 2))
            {

                this.Invalidate();
                Thread.Sleep(1);
                double cRedYellow = Math.Sqrt(Math.Pow(Math.Abs(x1 - x3), 2) + Math.Pow(Math.Abs(y1 - y3), 2));
                double cRedPurple = Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));

                if (cRedYellow <= diameter / 2)
                {
                    if (x1 < x3)
                    {
                        goRight1 = false;
                    }
                    if (goUp) { goUp = false; }
                    else { goUp = true; }
                }
                if (cRedPurple <= diameter / 2)
                {
                    if (x1 < x2)
                    {
                        goRight1 = false;
                    }
                    if (goUp) { goUp = false; }
                    else { goUp = true; }
                }
                if (goUp)
                {
                    if (y1 > 0) { y1 -= speedy1; }
                    else { goUp = false; y1 += speedy1; }
                }
                else
                {
                    if (y1 < finish_height) { y1 += speedy1; }
                    else { goUp = true; y1 -= speedy1; }
                }

                if (goRight1)
                {
                    if (x1 > 0) { x1 -= speedy1; }
                    else { goRight1 = false; x1 += speedy1; }
                }
                else
                {
                    if (x1 < finish_height) { x1 += speedx1; }
                    else
                    {
                        goRight1 = false;
                        speedx1 = 0;
                        speedy1 = 0;
                    }
                }

                continue;
            }

            n = n + 1;
            x1 = 900;
            y1 = 900;
            Invalidate();
            //list.Remove("Red");
        }

      public void cycl2()
        {
            while (x2 < finish_width)
            {
                while (IsNotInBlackHole(x2, y2))
                {
                    this.Invalidate();
                    Thread.Sleep(1);
                    double cPurpleYellow = Math.Sqrt(Math.Pow(Math.Abs(x2 - x3), 2) + Math.Pow(Math.Abs(y2 - y3), 2));
                    double cPurpleRed = Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));

                    if (cPurpleYellow <= diameter / 2)
                    {
                        if (x2 < x3)
                        {
                            right2 = false;
                        }

                        if (goUp2)
                        {
                            goUp2 = false;
                        }
                        else
                        {
                            goUp2 = true;
                        }
                    }
                    else if (cPurpleRed <= diameter / 2)
                    {
                        if (x2 < x1)
                        {
                            right2 = false;
                        }

                        if (goUp2)
                        {
                            goUp2 = false;
                        }
                        else
                        {
                            goUp2 = true;
                        }
                    }

                    if (goUp2)
                    {
                        if (y2 > 0)
                        {
                            y2 -= speedy2;
                        }
                        else
                        {
                            goUp2 = false;
                            y2 += speedy2;
                        }
                    }
                    else
                    {
                        if (y2 < finish_height)
                        {
                            y2 += speedy2;
                        }
                        else
                        {
                            goUp2 = true;
                            y2 -= speedy2;
                        }
                    }

                    if (right2)
                    {
                        if (x2 > 0)
                        {
                            x2 -= speedy2;
                        }
                        else
                        {
                            right2 = false;
                            x2 += speedy2;
                        }
                    }
                    else
                    {
                        if (x2 < finish_height)
                        {
                            x2 += speedx2;
                        }
                        else
                        {
                            right2 = false;

                            speedx2 = 0;
                            speedy2 = 0;
                            list.Add("Purple");
                            //x2 -= speedx2;
                        }
                    }

                    continue;
                }

                //list.Remove("Purple");
                //x2 -= speedx2;
                //if (goUp) { y2 += speedy2; }
                //else
                //{
                //    { y2 -= speedy2; }
                //}
                n = n + 1;
                x2 = 900;
                y2 = 900;
                Invalidate();
            }
        }



        //public void cycl2()
//        {
//            while (x2 < finish_width)
//            {
//                this.Invalidate();
//                Thread.Sleep(1);
//                double cPurpleYellow = Math.Sqrt(Math.Pow(Math.Abs(x2 - x3), 2) + Math.Pow(Math.Abs(y2 - y3), 2));
//                double cPurpleRed = Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));

        //                
        //            }
        //            list.Add("Purple");
        //            //x2 -= speedx2;
        //            //if (goUp) { y2 += speedy2; }
        //            //else
        //            //{
        //            //    { y2 -= speedy2; }
        //            //}
        //        }




        public void cycl3()
        {
            while (x3 < finish_width)
            {
                while (IsNotInBlackHole(x3, y3))
                {
                    this.Invalidate();
                    Thread.Sleep(1);
                    double cYellowRed = Math.Sqrt(Math.Pow(Math.Abs(x1 - x3), 2) + Math.Pow(Math.Abs(y1 - y3), 2));
                    double cYellowPurple = Math.Sqrt(Math.Pow(Math.Abs(x3 - x2), 2) + Math.Pow(Math.Abs(y3 - y2), 2));
                    if (cYellowPurple <= diameter / 2)
                    {
                        if (x3 < x2)
                        {
                            right3 = false;
                        }

                        if (goUp3)
                        {
                            goUp3 = false;
                        }
                        else
                        {
                            goUp3 = true;
                        }
                    }

                    if (cYellowRed <= diameter / 2)
                    {
                        if (x3 < x1)
                        {
                            right3 = false;
                        }

                        if (goUp3)
                        {
                            goUp3 = false;
                        }
                        else
                        {
                            goUp3 = true;
                        }
                    }

                    if (goUp3)
                    {
                        if (y3 > 0)
                        {
                            y3 -= speedy3;
                        }
                        else
                        {
                            goUp3 = false;
                            y3 += speedy3;
                        }
                    }
                    else
                    {
                        if (y3 < finish_height)
                        {
                            y3 += speedy3;
                        }
                        else
                        {
                            goUp3 = true;
                            y3 -= speedy3;
                        }
                    }

                    if (right3)
                    {
                        if (x3 > 0)
                        {
                            x3 -= speedy3;
                        }
                        else
                        {
                            right3 = false;
                            x3 += speedy3;
                        }
                    }
                    else
                    {
                        if (x3 < finish_height)
                        {
                            x3 += speedx3;
                        }
                        else
                        {
                            right3 = false;
                            speedx3 = 0;
                            speedy3 = 0;
                            list.Add("Yellow");
                            //x3 -= speedx2;
                        }
                    }

                    continue;
                }

                //list.Remove("Yellow");
                n = n + 1;
                x3 = 900;
                y3 = 900;

                Invalidate();
            }
        }



        //public void cycl3()
        //{
        //    while (x3 < finish_width)
        //    {
        //        this.Invalidate();
        //        Thread.Sleep(1);
        //        double cYellowRed = Math.Sqrt(Math.Pow(Math.Abs(x1 - x3), 2) + Math.Pow(Math.Abs(y1 - y3), 2));
        //        double cYellowPurple = Math.Sqrt(Math.Pow(Math.Abs(x3 - x2), 2) + Math.Pow(Math.Abs(y3 - y2), 2));
        //        
        //    }
        //    list.Add("Yellow");
        //    //x3 -= speedx2;
        //    //if (goUp) { y3 += speedy2; }
        //    //else
        //    //{
        //    //    { y3 -= speedy2; }
        //    //}
        //}




        public void cycl4()
        {
            bool t = true;
            while (t)
            {
                if (list.Count == 3)
                {
                    this.Invalidate();
                    t = false;
                }

            }

        }




        public void cycl5()
        {
            this.Invalidate();
            Thread.Sleep(1);
            while (true)
            {

                this.Invalidate();
                Thread.Sleep(1);
                {

                    if (goUpBlackHole)
                    {
                        if (blackHoleY > 0)
                        {
                            blackHoleY -= speedBHy;
                        }
                        else
                        {
                            goUpBlackHole = false; blackHoleY += speedBHy;
                        }
                    }
                    else
                    {
                        if (blackHoleY < 600 - 4 * diameter)
                        {
                            blackHoleY += speedBHy;
                        }
                        else
                        {
                            goUpBlackHole = true; blackHoleY -= speedBHy;
                        }
                    }


                    

                    if (goRightBlackHole)
                    {
                        if (blackHoleX > 0)
                        {
                            blackHoleX -= speedBHx;
                        }
                        else
                        {
                            goRightBlackHole = false;
                            blackHoleX += speedBHx;
                        }
                    }
                    else
                    {
                        if (blackHoleX < 600 - 4 * diameter)
                        {
                            blackHoleX += speedBHx;
                        }
                        else
                        {
                            goRightBlackHole = true;
                            blackHoleX -= speedBHx;
                        }
                    }

                    continue;

                    //Invalidate();
                }

            }

        }
    }
}
