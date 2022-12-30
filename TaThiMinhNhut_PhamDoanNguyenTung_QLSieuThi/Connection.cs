using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi
{
    class Connection
    {
        public static SqlConnection conn = new SqlConnection(TaThiMinhNhut_PhamDoanNguyenTung_QLSieuThi.Properties.Settings.Default.strConnect);

         public void Connect()
        {
            if (conn.State.ToString() == "Closed")
            {
                conn.Open();
            }
        }
        public void Disconnect()
        {
            if (conn.State.ToString() == "Open")
            {
                conn.Close();
            }
        }
        //Thực thi câu lênh insert, delete, update
        public void ExecuteNonQuery(string sql)
        {
            Connect();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            Disconnect();
        }
        //Trả về DataReader
        public SqlDataReader getDataReader(string sql)
        {
            Connect();

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }
        // trả về một DataTable .

        public DataTable getDataTable(string sql)
        {
            Connect();

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            //Disconnect();

            return dt;

        }

        public SqlDataAdapter getDataAdapter(string sql)
        {
            Connect();

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);

            return da;

        }

        public SqlCommand getSqlCommand(string sql)
        {

            SqlCommand cmd = new SqlCommand(sql, conn);

            return cmd;

        }
    }
}
