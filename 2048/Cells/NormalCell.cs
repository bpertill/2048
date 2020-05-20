using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _2048
{
    class NormalCell : Cell
    {
        public NormalCell(int i)
        {
            value = i;
            Recolour();
        }
        public TextBlock textBlock = new TextBlock()
        {
        Foreground = Brushes.Black,
        };
        public override bool IsBlank()
        {
            return false;
        }
        public override int DeleteCell(Canvas c)
        {
            if (c.Children.Contains(textBlock))
            {
                c.Children.Remove(textBlock);
            }
            return base.DeleteCell(c);
        }
        public override int Merge(int x)
        {
            if (x == value)
            {
                value += value;
                Recolour();
                return 1;
            }
            return -1;
        }
        private void Recolour()
        {
            textBlock.Text = value.ToString();

            if (value < 100)
                textBlock.FontSize = 50;
            else if (value < 1000)
                textBlock.FontSize = 45;
            else if (value < 10000)
                textBlock.FontSize = 30;
            else
                textBlock.FontSize = 24;

            switch (value)
            {
                case 2:     rect.Fill = Brushes.Beige;
                    break;
                case 4:     rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)220, (byte)200, (byte)170));
                    break;
                case 8:     rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)242, (byte)0));
                    break;
                case 16:    rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)165, (byte)0));
                    break;
                case 32:    rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)99, (byte)71));
                    break;
                case 64:    rect.Fill = Brushes.OrangeRed;
                    break;
                case 128:   rect.Fill = Brushes.Red;
                    break;
                case 256:   rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)136, (byte)0, (byte)21));
                    break;
                case 512:   rect.Fill = Brushes.Magenta;
                    break;
                case 1024:  rect.Fill = Brushes.DarkViolet;
                    break;
                case 2048:  rect.Fill = Brushes.Purple;
                    break;
                case 4096:  rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)64, (byte)0, (byte)128));
                    break;
                case 8192:  rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)181, (byte)230, (byte)29));
                    break;
                case 16384: rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)34, (byte)177, (byte)76));
                    break;
                case 32768: rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)0, (byte)100, (byte)0));
                    break;
                case 65536: rect.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)0, (byte)60, (byte)0));
                    break;
            }
        }
        private int TextOffsetY(int y)
        {
            int offset;
            if (value < 10)         offset = 30;
            else if (value < 100)   offset = 15;
            else if (value < 1000)  offset = 5;
            else if (value < 10000) offset = 10;
            else                    offset = 9;

           return offset + y * 80;
        }
        private int TextOffsetX(int x)
        {
            int offset;
            if (value < 10) offset = 10;
            else if (value < 100) offset = 10;
            else if (value < 1000) offset = 15;
            else if (value < 10000) offset = 23;
            else offset = 28;

            return offset + x * 80;
        }

        public override void Draw(Canvas c, int x, int y)
        {
            base.Draw(c, x, y);

            if (!c.Children.Contains(textBlock))
            {
                c.Children.Add(textBlock);
            }


            Canvas.SetTop(textBlock, TextOffsetX(x));
            Canvas.SetLeft(textBlock, TextOffsetY(y));
   
            
        }
    }
}
