using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeShifter_Katou{
    public partial class Form1 : Form
        {
        public Form1(){
            InitializeComponent();
        }

        const int N = 4;
        Button[,] buttons = new Button[N, N];

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateAllButtons();//产生所有按钮
        }

        private void button1_Click_1(object sender,EventArgs e)
        {
            Shuffle();//打乱顺序
        }

        //产生所有按钮
        void GenerateAllButtons()
        {
            int x0 = 100, y0 = 10, w = 150, d = 155;
            for (int r = 0; r < N; r++)
                for (int c = 0; c < N; c++)
                {
                    int num = r * N + c;
                    Button btn = new Button();
                    /*
                    增加难度:把每个button的text数字不显示了0 0
                    btn.Text = (num + 1).ToString();
                    */
                    btn.Top = y0 + r * d;
                    btn.Left = x0 + c * d;
                    btn.Width = w;
                    btn.Height = w;
                    btn.Visible = true;
                    btn.Tag = r * N + c; //存储它所在行列位置(num信息)

                    //注册事件
                    btn.Click += new EventHandler(btn_Click);
                    buttons[r, c] = btn; //放到数组中
                    this.Controls.Add(btn); //加到界面上
                }
            buttons[0, 0].BackgroundImage = ShapeShifter_Katou.Properties.Resources._00;
            buttons[0, 1].BackgroundImage = ShapeShifter_Katou.Properties.Resources._01;
            buttons[0, 2].BackgroundImage = ShapeShifter_Katou.Properties.Resources._02;
            buttons[0, 3].BackgroundImage = ShapeShifter_Katou.Properties.Resources._03;
            buttons[1, 0].BackgroundImage = ShapeShifter_Katou.Properties.Resources._10;
            buttons[1, 1].BackgroundImage = ShapeShifter_Katou.Properties.Resources._11;
            buttons[1, 2].BackgroundImage = ShapeShifter_Katou.Properties.Resources._12;
            buttons[1, 3].BackgroundImage = ShapeShifter_Katou.Properties.Resources._13;
            buttons[2, 0].BackgroundImage = ShapeShifter_Katou.Properties.Resources._20;
            buttons[2, 1].BackgroundImage = ShapeShifter_Katou.Properties.Resources._21;
            buttons[2, 2].BackgroundImage = ShapeShifter_Katou.Properties.Resources._22;
            buttons[2, 3].BackgroundImage = ShapeShifter_Katou.Properties.Resources._23;
            buttons[3, 0].BackgroundImage = ShapeShifter_Katou.Properties.Resources._30;
            buttons[3, 1].BackgroundImage = ShapeShifter_Katou.Properties.Resources._31;
            buttons[3, 2].BackgroundImage = ShapeShifter_Katou.Properties.Resources._32;
            buttons[3, 3].BackgroundImage = ShapeShifter_Katou.Properties.Resources._33;
            buttons[N - 1, N - 1].Visible = false; //最后一个不可见
        }

        //打乱顺序
        void Shuffle()
        {
            //多次随机交换两个按钮
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int a = rnd.Next(N);
                int b = rnd.Next(N);
                int c = rnd.Next(N);
                int d = rnd.Next(N);
                Swap(buttons[a, b], buttons[c, d]);
            }
        }

        //交换两个按钮
        void Swap(Button btna, Button btnb)
        {
            string t = btna.Text;
            btna.Text = btnb.Text;
            btnb.Text = t;

            bool v = btna.Visible;
            btna.Visible = btnb.Visible;
            btnb.Visible = v;

            Image i = btna.BackgroundImage;
            btna.BackgroundImage = btnb.BackgroundImage;
            btnb.BackgroundImage = i;
        }

        //按钮点击事件处理
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button; //当前点中的按钮
            Button blank = FindHiddenButton(); //空白按钮

            //判断是否与空白块相邻，如果是，则交换
            if (IsNeighbor(btn, blank))
            {
                Swap(btn, blank);
                blank.Focus();
            }

            //判断是否完成了
            if (ResultIsOk())
            {
                MessageBox.Show("Congratulations!");
            }
        }

        //查找要隐藏的按钮
        Button FindHiddenButton()
        {
            for (int r = 0; r < N; r++)
                for (int c = 0; c < N; c++)
                {
                    if (!buttons[r, c].Visible)
                    {
                        return buttons[r, c];
                    }
                }
            return null;
        }

        //判断是否相邻
        bool IsNeighbor(Button btnA, Button btnB)
        {
            int a = (int)btnA.Tag; //Tag中记录是行列位置
            int b = (int)btnB.Tag;
            int r1 = a / N, c1 = a % N;
            int r2 = b / N, c2 = b % N;

            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) //左右相邻
                || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))
                return true;
            return false;
        }

        //检查是否完成
        bool ResultIsOk()
        {
            for (int r = 0; r < N; r++)
                for (int c = 0; c < N; c++)
                {
                    if (buttons[r, c].Text != (r * N + c + 1).ToString())
                    {
                        return false;
                    }
                }
            return true;
        }
    }
}
