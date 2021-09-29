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
            return Attributes.FirstOrDefault(x => x.Name == attrName)?.Value;
        }
    }
}
