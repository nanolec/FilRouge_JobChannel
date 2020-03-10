using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHM
{
    public static class DateTimePickerExtension
    {
        public static DateTime? NullableValue(this DateTimePicker dtp)
        {
            return dtp.Checked ? dtp.Value : (DateTime?)null;
        }

        public static void NullableValue(this DateTimePicker dtp, DateTime? value)
        {
            dtp.Checked = value.HasValue;
            if (dtp.Checked)
            {
                dtp.Format = DateTimePickerFormat.Short;
                dtp.Value = value.Value;
            }
            else
            {
                dtp.Format = DateTimePickerFormat.Custom;

                dtp.CustomFormat = " ";
            }
        }
    }
}
