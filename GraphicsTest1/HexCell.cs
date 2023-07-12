using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphicsTest1
{
    public class HexCell
    {
        public Point center;
        public float? size;
        public Brush? backgroundColor;
        public Brush? borderColor;

        public HexCell(Point center, Brush? backgroundColor, Brush? borderColor)
        {
            this.center = center;
            this.backgroundColor = backgroundColor;
            this.borderColor = borderColor;
        }   
    }
}
