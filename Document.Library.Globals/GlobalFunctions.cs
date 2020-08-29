using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Globals
{
    public static class GlobalFunctions
    {
        /// <summary>
        /// Using to send DataTable parameter in Store procedure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">list of int</param>
        /// <returns>DataTable</returns>
        public static DataTable ListToIdDataTable(List<int> list)
        {
            DataTable Ids = new DataTable();
            Ids.Columns.Add("Id", typeof(int));
            foreach (int id in list)
            {
                DataRow dataRow = Ids.NewRow();
                dataRow["Id"] = id;
                Ids.Rows.Add(dataRow);
            }
            return Ids;
        }
        

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> func)
        {
            foreach (var i in source)
            {
                func(i);
            }
            return source;
        }

    }
}
