using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver rec = new Receiver();
            rec.Start();            
        }
    }
}
