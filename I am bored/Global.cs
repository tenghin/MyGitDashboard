using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_am_bored
{
    internal class Global
    {

        public static User? CurrentUser { get; set; }

        public static string RootPath { get; set; } = "";

        public static bool Exit(bool value) 
        {

            value = false;
        
        return value;
        }


    }
}
