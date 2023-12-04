using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _24
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
    }
    static class Database
    {
        public static string connection = @"Data Source=24.db; Integrated Security=False; MultipleActiveResultSets=True";
    }
    static class qtable
    {
        public static string main = "Process";
        public static string ID = "Id";
        public static string NP = "NameProcess";
        public static string Pr = "Priority";
        public static string Class = "Class";
        public static string OI = "OwnerID";
    }
    static class wtable
    {
        public static string main = "Recourcces";
        public static string ID = "Id";
        public static string RN = "ResoirceName";
        public static string Kol = "Kol-voR";
        public static string Price = "Price";
    }
    static class etable
    {
        public static string main = "PlannedResources";
        public static string ID = "Id";
        public static string ProcessID = "ProcessID";
        public static string Priority = "Priority";
        public static string ResourceID = "ResourceID";
        public static string Kolvo = "Kolvo";
        public static string Requested = "Requested";
        public static string Highlighted = "Highlighted";
        public static string Owner = "Owner";
        public static string Price = "Price";
    }
}
