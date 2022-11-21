using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Media.Animation;


namespace timer_vr4
{

    /// <summary>
    /// Ti.xaml の相互作用ロジック
    /// </summary>
    public partial class Ti : Page
    {
        /// <summary>
        /// 計測用たいまー
        /// </summary>
        Timer timer ;
        /// <summary>
        /// 表示用タイマー_メインスレッド
        /// </summary>
        DispatcherTimer MainTimer;

        public Ti()
        {
            InitializeComponent();
            //size = OverElipse.Height;

        }

        /// <summary>
        /// 秒数保管先
        /// </summary>
        private static int SEcond;//変化なし
        private static int second;
        bool []timerstate=new bool[2];//[0]->見た目上タイマーが動いているか[1]->別スレッドのタイマーが待機状態か
        static System.Media.SoundPlayer Player = new(Properties.Resources.TimeUp);

        double size;

        /// <summary>
        /// 指定秒数のカウントダウンを実行
        /// </summary>
        /// <param name="second"></param>
        void CreateTimer()
        {
            timer = new(1000);
            second = SEcond;
            timerstate[1] = true;


            timer.Elapsed += (sender, e) =>
            {
                second--;
                if (second == 0)
                {
                    Player.Play();
                    timer.Close();
                    timerstate[1] = false;
                }
            };
            timer.Start();
        }

        /// <summary>
        /// 見た目用のタイマー
        /// </summary>
        void ShowTimer()//
        {






            second = SEcond;
            MainTimer = new DispatcherTimer(DispatcherPriority.Input);
            MainTimer.Interval = TimeSpan.FromMilliseconds(300);
            

            MainTimer.Tick += (sender, e) =>
            {
                MiddleContent.Content = Format(second);
                //SizeChanger();
                if(second == 0)
                {
                    MainTimer.Stop();
                    return;
                }

            };
            MainTimer.Start();


        }






        /// <summary>
        /// 秒→HH:MM:SS
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        static string Format(int second)
        {
            double H, M, S;
            H = (second / 3600) % 100;
            M = (second / 60) % 60;
            S = second % 60;
            return $"{H.ToString("00")}:{M.ToString("00")}:{S.ToString("00")}";
        }
        static string Format(string second)
        {
            return int.Parse(second).ToString("00:00:00");
        }

        /// <summary>
        /// OverElipseの大きさ変えよう
        /// </summary>
        /*void SizeChanger()
        {

            double nowsize=size*((double)second/SEcond);
            Debug.Print(""+size);
            OverElipse.Height = nowsize;
            OverElipse.Width = nowsize;

        }*/



        private void SetTimer_TextChanged(object sender, TextChangedEventArgs e)//全角ではエラー発生<<<<---要対応<<---無理かも
        {
            SetTimer.IsEnabled = false;//連続で0を入力すると桁制限を圧迫して入力できなくなる<<<---要対応<<---対応済み
            string Text = SetTimer.Text;
            timerstate[1] = false;

            /*OverElipse.Height = size;
            OverElipse.Width = size;*/



            if (timerstate[1]==true) timer.Close();

            if (Text.Length==6&& int.TryParse(Text, out int result))
            {
                Text = $"{result}";
                SetTimer.Text = Text;
            }

            if (Text == "0")
            {
                Text = "";
            }

            if (int.TryParse(Text, out int second))
            {
                SEcond = (int)((double)(second / 10000) % 100) * 3600 + (int)((double)(second / 100) % 100) * 60 + second % 100;

                if (Format(Text) == Format(SEcond))
                {
                    MiddleContent.Content = Format(SEcond);
                    MiddleButton.BorderBrush = new SolidColorBrush(Colors.Orange);
                }
                else
                {
                    MiddleContent.Content = Format(Text);
                    MiddleButton.BorderBrush = new SolidColorBrush(Colors.Red);
                }

            }
            else if (Text == "")
            {
                MiddleContent.Content=Format(0);
            }
            else
            {
                string Texttext = Text;
                Text ="";

                for (int i = 0; i < Texttext.Length-1; i++)
                {
                    Text += $"{Texttext[i]}";
                }
                SetTimer.Text = Text;

            }
            SetTimer.IsEnabled = true;

        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)//要修正中断とリセットが一緒くたになってる
        {
            if (timerstate[0] == true)
            {
                MainTimer.Stop();

                if (timerstate[1] == true)//実行時の中断
                {
                    timer.Stop();
                    SetTimer.IsEnabled = true;
                }
                else //時間経過後の音止めるやつ
                {
                    Player.Stop();
                    MiddleButton.BorderBrush = new SolidColorBrush(Colors.LawnGreen);
                    MiddleContent.Content = Format(SEcond);
                    /*OverElipse.Height = size;
                    OverElipse.Width = size;*/
                }
                timerstate[0] = false;
                SetTimer.IsEnabled = true;
                return;
            }

            Player.Stop();

        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (SEcond < 1)
            {
                return;
            }
            System.Diagnostics.Trace.WriteLine($"[0]={timerstate[0]}[1]={timerstate[1]}");
            if (timerstate[0] == false)
            {
                SetTimer.IsEnabled = false;

                if ( timerstate[1] == false)//タイマースタート
                {
                    if (timer != null)
                    {
                        timer.Close();
                    }
                    ShowTimer();
                    CreateTimer();
                    MiddleButton.BorderBrush = new SolidColorBrush(Colors.DeepSkyBlue);

                    
                }
                else //タイマーリスタート
                {
                    SetTimer.IsEnabled = false;
                    timer.Start();
                    MainTimer.Start();
                    MiddleButton.BorderBrush = new SolidColorBrush(Colors.DeepSkyBlue);
                }
                timerstate[0] = true;

            }


        }

        private void SetTimer_MouseEnter(object sender, MouseEventArgs e)
        {
           MiddleButton.BorderBrush = new SolidColorBrush(Colors.Orange);
        }

        private void SetTimer_MouseLeave(object sender, MouseEventArgs e)
        {
            MiddleButton.BorderBrush = new SolidColorBrush(Colors.LawnGreen);
        }
    }


}


