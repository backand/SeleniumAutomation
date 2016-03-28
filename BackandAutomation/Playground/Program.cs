using Core;
using System;

namespace Playground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string active = "{active : dbedit.dataName === 'mysql'}";
            Array array = Enum.GetValues(typeof(DatabaseType));
            foreach (var type in array)
            {
                string enumtext = type.ToString().ToLower();
                if (active.Contains(enumtext))
                {
                    var typee =  enumtext.ToEnum<DatabaseType>();
                }
            }
        }

        public enum DatabaseType
        {
            [EnumText("mysql")]
            MySql,
            [EnumText("postgresql")]
            PostgreSql,
            [EnumText("sqlserver")]
            SqlServer
        }

        private static string MakeFolderPath()
        {
            DateTime datetimeNow = DateTime.Now;
            string time = datetimeNow.ToLongTimeString().Replace(':', '-');
            string date = datetimeNow.ToShortDateString().Replace('/', '.');
            string folderName = $"Results - {date} {time}";
            return folderName;
        }
    }
}