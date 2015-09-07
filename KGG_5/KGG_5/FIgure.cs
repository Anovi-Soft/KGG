using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KGG_5
{
    public interface Figure
    {
        double ZZ();
        Vector[] Vertex();
        Brush Color();
        void SetBrush(Brush brush);
    }
}
