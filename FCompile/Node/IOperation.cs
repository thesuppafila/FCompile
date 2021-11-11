using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public interface IOperation
    {
        string ToString(string tab);
    }
}
