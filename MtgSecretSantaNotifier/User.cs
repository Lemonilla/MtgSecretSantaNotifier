using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgSecretSantaNotifier
{
    public class User
    {
        public List<Attribute> Attributes = new List<Attribute>();
        public string GetAttributeValue(string attrName)
        {
            foreach (var attr in Attributes)
            {
                if (attr.Name == attrName) return attr.Value;
            }
            return null;
        }
    }
}
