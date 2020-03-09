using System.Windows.Forms;

namespace IHM
{
    class DataGridViewOptimized : DataGridView
    {
        public DataGridViewOptimized() : base()
        {
            DoubleBuffered = true;
        }
    }
}