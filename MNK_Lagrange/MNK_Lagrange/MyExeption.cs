using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNK_Lagrange
{
    class MyExeption: Exception
    {
        public MyExeption(String message)
           : base(message)
        {
        }
    }
}
