using Devart.Data.Oracle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Common.DbHelper
{
    /// <summary>
    /// 基于 Devart.Data.Oracle
    /// </summary>
    public static class OracleDbHelper
    {
        /// <summary>
        /// 查询单个值
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText">执行的语句</param>
        /// <param name="type">执行语句类型</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, string commandText, CommandType type, params OracleParameter[] paras)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.CommandType = type;
                if (paras != null)
                    cmd.Parameters.AddRange(paras);

                return cmd.ExecuteScalar();

            }
        }

        public static int ExecuteNonQuery(string connectionString, string commandText, CommandType type, params OracleParameter[] paras)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.CommandType = type;
                if (paras != null)
                    cmd.Parameters.AddRange(paras);

                return cmd.ExecuteNonQuery();

            }
        }


        public static DataSet ExecuteDataset(string connectionString, string commandText, CommandType type, params OracleParameter[] paras)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.CommandType = type;
                if (paras != null)
                    cmd.Parameters.AddRange(paras);

                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds;
            }
        }

    }
}
