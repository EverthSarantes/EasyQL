using Microsoft.Data.SqlClient;

namespace EasyQL
{
    public class Model
    {
        private string identifier;
        private List<string> fields;
        private string table;
        private SqlConnection con;

        public string Identifier { get => identifier; set => identifier = value; }
        public List<string> Fields { get => fields; set => fields = value; }
        public string Table { get => table; set => table = value; }
        public SqlConnection Con { get => con; set => con = value; }

        public Model(SqlConnection conection)
        {
            Fields = new List<string>();
            Table = "";
            Identifier = "id";
            Con = conection;
        }

        public SqlDataReader find(string id, List<string> fields)
        {
            try
            {
                string query = $"Select ";

                foreach (string i in fields)
                {
                    query = query + $"{i},";
                }
                query = query.Remove(query.Length - 1);
                query = query + $" from {Table} Where {Identifier}  = {id};";

                SqlCommand comand = new SqlCommand(query, Con);
                SqlDataReader reader = comand.ExecuteReader();
                return reader;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public SqlDataReader find(string id)
        {
            try
            {
                string query = $"Select *";

                query = query + $" from {Table} Where {Identifier}  = {id};";

                SqlCommand comand = new SqlCommand(query, Con);
                SqlDataReader reader = comand.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public SqlDataReader all(List<string> fields)
        {
            try
            {
                string query = $"Select ";

                foreach (string i in fields)
                {
                    query = query + $"{i},";
                }
                query = query.Remove(query.Length - 1);

                query = query + $" from {Table};";
                SqlCommand comand = new SqlCommand(query, Con);
                SqlDataReader reader = comand.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public SqlDataReader all()
        {
            try
            {
                string query = $"Select *";

                query = query + $" from {Table};";
                SqlCommand comand = new SqlCommand(query, Con);
                SqlDataReader reader = comand.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public SqlDataReader where(string field, string comparator, string value, List<string> fields)
        {
            try
            {
                string query = $"Select ";

                foreach (string i in fields)
                {
                    query = query + $"{i},";
                }
                query = query.Remove(query.Length - 1);

                query = query + $" from {Table} where {field} {comparator} '{value}';";
                SqlCommand comand = new SqlCommand(query, Con);
                SqlDataReader reader = comand.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public SqlDataReader where(string field, string comparator, string value)
        {
            try
            {
                string query = $"Select *";

                query = query + $" from {Table} where {field} {comparator} '{value}';";
                SqlCommand comand = new SqlCommand(query, Con);
                SqlDataReader reader = comand.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public int insert(List<string> fields, List<string> values)
        {
            try
            {
                string query = $"insert into {Table} (";

                for(int i = 0; i < fields.Count(); i++)
                {
                    query = query + $"{fields[i]},";
                }
                query = query.Remove(query.Length - 1);
                query = query + ") values( ";

                for (int i = 0; i < values.Count(); i++)
                {
                    query = query + $"'{values[i]}',";
                }
                query = query.Remove(query.Length - 1);
                query = query + ");";

                SqlCommand comand = new SqlCommand(query, Con);
                int rows = comand.ExecuteNonQuery();

                return rows;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public int delete(string id)
        {
            try
            {
                string query = $"delete from {Table} where {id} = {Identifier};";
                SqlCommand comand = new SqlCommand(query, Con);
                int rows = comand.ExecuteNonQuery();
                return rows;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public int deleteWhere(string field, string comparator, string value)
        {
            try
            {
                string query = $"delete from {Table} where {field} {comparator} {value};";
                SqlCommand comand = new SqlCommand(query, Con);
                int rows = comand.ExecuteNonQuery();
                return rows;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public int update(List<List<string>> data, string id)
        {
            try
            {
                string query = $"update {Table} set";

                foreach(List<string> i in data)
                {
                    query = query + $" {i[0]} = '{i[1]}',";
                }
                query = query.Remove(query.Length - 1);

                query = query + $" where {Identifier} = {id};";
                
                SqlCommand comand = new SqlCommand(query, Con);
                int rows = comand.ExecuteNonQuery();
                return rows;
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }
    }
}
