using System;
using System.Collections.Generic;

namespace FCompile
{
    public class PT
    {
        public PTT type;

        public List<PT> children;

        public string name;

        public PT value;

        public PT(PTT type)
        {
            children = new List<PT>();
        }

        public override string ToString()
        {
            return String.Format(" type = {0} , name = {1} , value = {2} ", type, name, value);
        }
    }
}
