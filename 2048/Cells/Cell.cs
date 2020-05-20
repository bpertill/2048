using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _2048
{
    abstract class Cell
    {
        public int value = 0;
        public Rectangle rect = new Rectangle { Height = 75, Width = 75, Fill = Brushes.DarkGray};

        public abstract int Merge(int x);
        public abstract bool IsBlank();

        public virtual int Moveable()
        {
            return value;
        }
        public virtual void Draw(Canvas c, int x, int y)
        {
            if (!c.Children.Contains(rect)){
                 c.Children.Add(rect);  
            }
            Canvas.SetTop(rect, 5 + x * 80);
            Canvas.SetLeft(rect, 5 + y * 80);
        }
        public virtual int DeleteCell(Canvas c)
        {
            if (c.Children.Contains(rect)){
                c.Children.Remove(rect);
            }
            return 0;
         }   
    }
}
