using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODOTNET_Concepts_ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection sourceCon = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products_Source", sourceCon);
                sourceCon.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    using (SqlConnection destinationCon = new SqlConnection(cs))
                    {
                        using (SqlBulkCopy copy = new SqlBulkCopy(destinationCon))
                        {
                            // If BatchSize not specified then by default 1 record will be read and sent to write to SqlBulkCopy which is not efficient
                            // When specified then only after specified batch size is read from source, it will be sent to SqlBulkCopy to be written to destination
                            copy.BatchSize = 10000;

                            // If NotifyAfter is set, then after specified read, it raises SqlRowsCopied event
                            copy.NotifyAfter = 5000;
                            // We can specify an handler for SqlRowsCopied event and do some processing or simply show progress
                            // copy.SqlRowsCopied += Copy_SqlRowsCopied;

                            // We can also do the same thing using anonymous function
                            copy.SqlRowsCopied += (send, copiedEventArgs) =>
                            {
                                Console.WriteLine(copiedEventArgs.RowsCopied + " loaded....");
                            };

                            copy.DestinationTableName = "Products_Destination";

                            destinationCon.Open();
                            copy.WriteToServer(rdr);
                        }
                    }
                }
            }
        }

        // Using event handler
        //private static void Copy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        //{
        //    Console.WriteLine(e.RowsCopied + " loaded....");
        //}
    }
}
