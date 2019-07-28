using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork25_07_19.Model
{
    class ComboClass
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
