using Microsoft.Extensions.Logging;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ACME.WorkerService
{
    static class Extension
    {
        public static DataTable FileToTable(this string path, bool heading = true, char delimiter = '\t')
        {
            var table = new DataTable();
            string headerLine = File.ReadLines(path).FirstOrDefault(); // Read the first row for headings
            string[] headers = headerLine.Split(delimiter);
            int skip = 1;
            int num = 1;

            foreach (string header in headers)
            {
                if (heading)
                    table.Columns.Add(header);
                else
                {
                    table.Columns.Add("Field" + num); // Create fields header if heading is false
                    num++;
                    skip = 0; // Don't skip the first row if heading is false
                }
            }
            foreach (string line in File.ReadLines(path).Skip(skip))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    table.Rows.Add(line.Split(delimiter));
                }
            }

            return table;
        }

        //public static DataTable FileToTable(this string path, ILogger logger, bool heading = true, char delimiter = '|', int offset = 0, int limit = 100000)
        public static DataTable FileToTable(this string path, bool heading = true, char delimiter = '|', int offset = 0, int limit = 100000)
        {
            var table = new DataTable();
            //string headerLine = File.ReadLines(path).FirstOrDefault(); // Read the first row for headings
            //string[] headers = headerLine.Split(delimiter);
            int skip = 1;
            //int num = 1;
            //foreach (string header in headers)
            //{
            //    if (heading)
            //    {
            //        table.Columns.Add(header);
            //    }
            //    else
            //    {
            //        table.Columns.Add("Field" + num); // Create fields header if heading is false
            //        num++;
            //        skip = 0; // Don't skip the first row if heading is false
            //    }
            //}

            table.Columns.Add("RUC", typeof(string));
            table.Columns.Add("NOMBRE_O_RAZON_SOCIAL", typeof(string));
            table.Columns.Add("ESTADO_DEL_CONTRIBUYENTE", typeof(string));
            table.Columns.Add("CONDICION_DE_DOMICILIO", typeof(string));
            table.Columns.Add("UBIGEO", typeof(string));
            table.Columns.Add("TIPO_DE_VIA", typeof(string));
            table.Columns.Add("NOMBRE_DE_VIA", typeof(string));
            table.Columns.Add("CODIGO_DE_ZONA", typeof(string));
            table.Columns.Add("TIPO_DE_ZONA", typeof(string));
            table.Columns.Add("NUMERO", typeof(string));
            table.Columns.Add("INTERIOR", typeof(string));
            table.Columns.Add("LOTE", typeof(string));
            table.Columns.Add("DEPARTAMENTO", typeof(string));
            table.Columns.Add("MANZANA", typeof(string));
            table.Columns.Add("KILOMETRO", typeof(string));

            foreach (string line in File.ReadLines(path, Encoding.GetEncoding("iso-8859-15")).Skip(skip + offset).Take(limit))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string _line = line.Remove(line.Length - 1);
                    //byte[] bytes = Encoding.ASCII.GetBytes(_line);
                    //var myString = Encoding.UTF8.GetString(bytes);
                    //_line = Encoding.UTF8.Get(_line);
                    //string utf8_String = "dayâ€™s";
                    //byte[] bytes = Encoding.Default.GetBytes(utf8_String);
                    //utf8_String = Encoding.UTF8.GetString(bytes);

                    var data = _line.Split(delimiter);
                    if (data.Length > 15)
                    {
                        System.Array.Resize(ref data, 15);
                    }
                    try
                    {
                        table.Rows.Add(data);
                    }
                    catch (System.Exception ex)
                    {
                        Serilog.Log.Error(ex, ex.Message);
                        //logger.LogCritical(ex.StackTrace);
                        //throw;
                    }
                }
            }

            return table;
        }

        public static void TableToFile(this DataTable table, string path, bool append = true)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!File.Exists(path) || !append)
                stringBuilder.AppendLine(string.Join("\t", table.Columns.Cast<DataColumn>().Select(arg => arg.ColumnName)));

            using StreamWriter sw = new StreamWriter(path, append);
            foreach (DataRow dataRow in table.Rows)
                stringBuilder.AppendLine(string.Join("\t", dataRow.ItemArray.Select(arg => arg.ToString())));
            sw.Write(stringBuilder.ToString());
        }
    }
}
