using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Tools
{
    public class Tools
    {
        public static void CopyFields(object source, object destination)
        {
            var sourceType = source.GetType();
            var destinationType = destination.GetType();

            foreach (var field in sourceType.GetFields())
            {
                var destField = destinationType.GetField(field.Name);
                if (destField != null)
                {
                    destField.SetValue(destination, field.GetValue(source));
                }
            }
        }
    }
}
