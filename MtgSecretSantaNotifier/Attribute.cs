using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgSecretSantaNotifier
{
    public class Attribute
    {
        public string Name { get; }
        public int Index { get; }
        public string Value { get; }
        public Attribute(string name, int row, string value = null) { this.Name = name; this.Index = row; this.Value = value; }
    }
}
