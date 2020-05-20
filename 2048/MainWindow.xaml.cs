
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board b;
        bool debugMode = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    if (b.ShiftUp())
                        AfterShift();
                    break;
                case Key.A:
                case Key.Left:
                    if (b.ShiftLeft())
                        AfterShift();
                    break;
                case Key.S:
                case Key.Down:
                    if (b.ShiftDown())
                        AfterShift();
                    break;
                case Key.D:
                case Key.Right:
                    if (b.ShiftRight())
                        AfterShift();
                    break;
                case Key.Space:
                    InitializeBoard();
                    break;
                case Key.Escape:
                    this.Close();
                    break;
                case Key.P://Debugging
                    b = new Board(canv);
                    b.SpawnAllColours();
                    b.Draw();
                    debugMode = true;
                    break;
                case Key.L:
                    b.Draw();

                    b.Draw();

                    break;
            }
            if (b.LoseCondition())
            {
                if (!debugMode)
                {
                    DoubleAnimation animation = new DoubleAnimation(5, TimeSpan.FromSeconds(10));
                    LoseBg.BeginAnimation(Rectangle.OpacityProperty, animation);
                    LoseLabel.BeginAnimation(Label.OpacityProperty, animation);
                }
            }

        }
        private void AfterShift()
        {
            b.CreateCell();
            Scoreboard.Content = b.getScore;
            b.Draw();
        }
        private void InitializeBoard()
        {
            canv.Children.Clear();
            b = new Board(canv);
            b.CreateCell();
            b.CreateCell();
            Scoreboard.Content = b.getScore;
            b.Draw();
            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
            LoseBg.BeginAnimation(Rectangle.OpacityProperty, animation);
            LoseLabel.BeginAnimation(Label.OpacityProperty, animation);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeBoard();
        }
    }
}
