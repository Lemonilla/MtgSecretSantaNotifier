using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgSecretSantaNotifier
{
    public class Attribute
    {
        public string Name { get; set; }
        public int Row { get; set; }
        public Attribute(string name, int row) { this.Name = name; this.Row = row; }
    }
}
