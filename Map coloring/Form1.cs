using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Map_coloring
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        Random rd1 = new Random();




        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
            G.n = (int)numericUpDown1.Value;
            G.a = new int[G.n];
            G.b = new int[G.n];
            G.line = new int[1000];
            for (int i = 0; i < G.n; i++)
            {

                G.a[i] = rd1.Next(1, 700);


            }
            for (int i = 0; i < G.n; i++)
            {


                G.b[i] = rd1.Next(1, 700);

            }
            for (int i = 0; i < G.n; i++)
            {
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(G.a[i] - 5, G.b[i] - 5, 10, 10));
                myBrush.Dispose();
                formGraphics.Dispose();
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            G.count = 0;
            var man = new int[G.n, G.n];
            var lined = new int[G.n, G.n];
            int nodex = G.a[0];                                                                                     // G.a =  values of x axis of the nodes
            G.no = 0;                                                                                               //G.b is the values of y axis of the nodes.
            int nodey = G.b[0];
            for (int o = 0; o < G.n; o++)
            {
                for (int k = 0; k < G.n; k++)
                {
                    lined[o, k] = 0;
                    int qw = G.a[o] - G.a[k];
                    int re = G.b[o] - G.b[k];

                    if (qw >= 0)
                    {
                        if (re >= 0)
                            man[o, k] = qw + re;
                        else
                            man[o, k] = qw - re;
                    }
                    else
                    {
                        if (re >= 0)
                            man[o, k] = -qw + re;
                        else
                            man[o, k] = -qw - re;
                    }



                }

            }
            for (int o = 0; o < G.n; o++)
            {

                man[o, o] = 9999;
            }



            int stx = 0;
            int sty = 0;


            var stack = new int[1000];
            for (int i = 0; i < 1000; i++)
                stack[i] = 9999;
            int top = 0;
            stack[top] = stx;
            int flag = 0;
            var drawn = new int[10000];
            int count = 0;
            int over;

            while (top != -1)
            {


                int small = 9990;

                
                int i;
                for (i = 0; i < G.n; i++)
                {

                    if (lined[stx, i] == 0)
                    {
                        if (man[stx, i] < small)
                        {

                            float x1 = G.a[stx];
                            float y1 = G.b[stx];
                            float x2 = G.a[i];
                            float y2 = G.b[i];
                            
                            over = check(x1, y1, x2, y2);

                            if (over == 1)
                            {
                                small = man[stx, i];
                                sty = i;
                            }

                        }
                    }
                }
                if (small != 9990)
                {
                    if (flag == 1) top++;
                    top++;
                    stack[top] = sty;
                    
                    lined[stx, sty] = 1;
                    lined[sty, stx] = 1;
                   
                    G.line[G.no] = stx + sty * 1000;
                    G.no++;
                    G.count++;
                    System.Drawing.Pen myPen;
                    myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
                    System.Drawing.Graphics formGraphics = this.CreateGraphics();
                    formGraphics.DrawLine(myPen, G.a[stx], G.b[stx], G.a[sty], G.b[sty]);
                    drawn[count] = stx * 10000 + sty;
                    myPen.Dispose();
                    formGraphics.Dispose();

                    stx = sty;
                    flag = 0;
                    count++;

                }

                else
                {
                    stx = stack[top];
                    top--;
                    flag = 1;
                }

            }
            



            //for (int i = 0; i < G.n; i++)
            //{
            //    for (int j = 0; j < G.n; j++)
            //    {
            //        Console.Write("{0} ", lined[i, j]);
            //    }
            //    Console.WriteLine(" ");

            //}

            G.ass = 0;

            G.col = new int[G.n];
            MessageBox.Show(string.Format("No of nodes expanded ={0}",G.count));
            Stopwatch stop = new Stopwatch();
            stop.Start();
            color(lined);


            stop.Stop();
            MessageBox.Show(string.Format("Time ellapsed = {0},Number of assignments ={1} ", stop.Elapsed,G.ass));




























        }

        public int[] color(int[,] lined)
      {
          int[,] assigned = new int[G.n,4];
          for (int i = 0; i < G.n; i++)
          {
              for(int j =0;j<4;j++)
              assigned[i,j] = 0;
          }
        for (int i = 0; i < G.n; i++)
        {
            int max =0;

            int[] check = { 0, 0, 0, 0 };
                int j;
                for (j = 0; j < 4; j++)
                {
                    if (assigned[i, j] == 1)
                        check[j] = 1;
                }
                
                    for (j = 0; j < G.n; j++)
                    {
                    
                        if (lined[i, j] == 2)
                        {

                            check[0] = 1;
                        
                        }
                        if (lined[i, j] == 3)
                        {

                            check[1] = 1;

                        }
                        if (lined[i, j] == 4)
                        {

                            check[2] = 1;

                        }
                        if (lined[i, j] == 5)
                        {

                            check[3] = 1;

                        }

                    }
                    for (j = 0; j < 4; j++)
                    {
                        if (check[j] == 0)
                            break;
                    }

                
                    max = j + 2;

                    if (max > 5)
                    {

                        
                        for (j = 0; j < 4; j++)
                            assigned[i, j] = 0;
                        i = i - 2;

                    }

                    else
                    {
                        lined[i, i] = max;
                        for (j = 0; j < G.n; j++)
                        {
                            if (lined[i, j] == 1)
                            {
                                G.ass++;
                                lined[j, i] = max;
                                
                            }
                        }
                            if (lined[i, i] == 2)
                            {
                                assigned[i, 0] = 1;
                                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Aqua);
                                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                                formGraphics.FillRectangle(myBrush, new Rectangle(G.a[i] - 10, G.b[i] - 10, 20, 20));
                                myBrush.Dispose();
                                formGraphics.Dispose();
                            }
                            if (lined[i, i] == 3)
                            {
                                assigned[i, 1] = 1;
                                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Orange);
                                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                                formGraphics.FillRectangle(myBrush, new Rectangle(G.a[i] - 10, G.b[i] - 10, 20, 20));
                                myBrush.Dispose();
                                formGraphics.Dispose();
                            }
                            if (lined[i, i] == 4)
                            {
                                assigned[i, 2] = 1;
                                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
                                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                                formGraphics.FillRectangle(myBrush, new Rectangle(G.a[i] - 10, G.b[i] - 10, 20, 20));
                                myBrush.Dispose();
                                formGraphics.Dispose();
                            }
                            if (lined[i, i] == 5)
                            {
                                assigned[i, 3] = 1;
                                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
                                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                                formGraphics.FillRectangle(myBrush, new Rectangle(G.a[i] - 10, G.b[i] - 10, 20, 20));
                                myBrush.Dispose();
                                formGraphics.Dispose();
                            }
                        
                    
                
                }
        }

        //for (int i = 0; i < G.n; i++)
        //{
           
        //    if (lined[i, i] == 2)
        //    {
        //        System.Threading.Thread.Sleep(200);
        //        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Aqua);
        //        System.Drawing.Graphics formGraphics = this.CreateGraphics();
        //        formGraphics.FillRectangle(myBrush, new Rectangle(G.a[i] - 10, G.b[i] - 10, 20, 20));
        //        myBrush.Dispose();
        //        formGraphics.Dispose();
        //    }
        //    if (lined[i, i] == 3)
        //    {
        //        System.Threading.Thread.Sleep(200);
        //        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Orange);
        //        System.Drawing.Graphics formGraphics = this.CreateGraphics();
        //        formGraphics.FillRectangle(myBrush, new Rectangle(G.a[i] - 10, G.b[i] - 10, 20, 20));
        //        myBrush.Dispose();
        //        formGraphics.Dispose();
        //    }
        //    if (lined[i, i] == 4)
        //    {
        //        System.Threading.Thread.Sleep(200);
        //        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
        //        System.Drawing.Graphics formGraphics = this.CreateGraphics();
        //        formGraphics.FillRectangle(myBrush, new Rectangle(G.a[i] - 10, G.b[i] - 10, 20, 20));
        //        myBrush.Dispose();
        //        formGraphics.Dispose();
        //    }
        //    if (lined[i, i] == 5)
        //    {
        //        System.Threading.Thread.Sleep(200);
        //        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
        //        System.Drawing.Graphics formGraphics = this.CreateGraphics();
        //        formGraphics.FillRectangle(myBrush, new Rectangle(G.a[i] - 10, G.b[i] - 10, 20, 20));
        //        myBrush.Dispose();
        //        formGraphics.Dispose();
        //    }
        //}




        return G.col;
        
            
    }
        private int check(float x1, float y1, float x2, float y2)
        {
            int yes = 1;
            for (int y = 0; y < G.no; y++)
            {

                int a = G.line[y] % 1000;
                int b = G.line[y] / 1000;
                float x3 = G.a[a];
                float x4 = G.a[b];
                float y3 = G.b[a];
                float y4 = G.b[b];
                float d = ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
                
                float u1 = 1, u2 = 1;
                if (d != 0)
                {

                    u1 = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3));
                    u2 = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3));
                    u1 = u1 / d;
                    u2 = u2 / d;

                    //MessageBox.Show(string.Format("{0}  {1}", u1.ToString(), u2.ToString()));

                    if (u1 < 1 && u1 > 0 && u2 < 1 && u2 > 0)
                    {
                        yes = 0;
                        float p1 = x1 + u1 * (x2 - x1);
                        float p2 = y1 + u1 * (y2 - y1);
                        //MessageBox.Show(string.Format("{0}  {1}", p1.ToString(), p2.ToString()));
                        if (p1 == x1)
                        {
                            if (p2 == y1)
                                yes = 1;
                        }
                        if (p1 == x2)
                        {
                            if (p2 == y2)
                                yes = 1;
                        }
                    }

                }
                if (yes == 0)
                    return 0;

            }
            
            return 1;
            

        }
    }
}

