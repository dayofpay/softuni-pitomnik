using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Pitomnik
{
    class MoreFunctions
    {
        public static void Error()
        {
            Console.WriteLine("Неочаквана грешка ... връщаме ви в главното меню");
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}
