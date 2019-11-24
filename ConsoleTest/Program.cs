using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using OneNote.SQLite;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection c = new Connection();
            c.Database.EnsureCreated();
        }
    }
}
