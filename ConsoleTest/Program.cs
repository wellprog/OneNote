using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using OneNote.Model;
//using OneNote.SQLite;
//using OneNote.SQLite.Helper;


using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace ConsoleTest
{
    //interface IT
    //{
    //    void it();
    //}

    //class TTest : IT
    //{
    //    public void it()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    class Program
    {

        static void Main(string[] args)
        {
            //var loader = OneNote.Helpers.ClassLoader.Instance;

            //var element = new TTest();

            //loader.Register(typeof(IT), element);
            //loader.Register<IT>(element);

            //IT d = loader.GetElement<IT>();

            
            for (int i = 0; i < 10; i++)
            {
                byte[] rnd = new byte[4];
                RNGCryptoServiceProvider.Create().GetBytes(rnd);

                int res = BitConverter.ToInt32(rnd, 0);
                res = Math.Abs(res);
                res = 10 + res % (20 - 10);

                Console.WriteLine(res);
            }
        }
    }
}
