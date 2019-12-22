using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneNote.Model;
using OneNote.SQLite;
using OneNote.SQLite.Helper;


using System.Net.Http;
using Newtonsoft.Json;

namespace ConsoleTest
{
    interface IT
    {
        void it();
    }

    class TTest : IT
    {
        public void it()
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var loader = OneNote.Helpers.ClassLoader.Instance;

            var element = new TTest();

            loader.Register(typeof(IT), element);
            loader.Register<IT>(element);

            IT d = loader.GetElement<IT>();
        }
    }
}
