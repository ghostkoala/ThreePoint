using System;
using System.Collections.Generic;

namespace ThreePoint.Repository.DbContextService
{
    /// <summary>
    /// MySql连接上下文
    /// </summary>
    public class MySqlConnect
    {
        /// <summary>
        /// 主数据库连接
        /// </summary>
        private static string WriteConnectString = "server=localhost;port=3306;database=cc;uid=root;pwd=Mysql888;CharSet=utf8;sslMode=None";

        /// <summary>
        /// 从数据库连接
        /// </summary>
        private static List<string> ReadConnectStrings = new List<string>()
        {
            "server=localhost;port=3306;database=cc;uid=root;pwd=Mysql888;CharSet=utf8;sslMode=None"
        };

        public static string GetReadConnectString()
        {
            var i = ReadConnectStrings.Count;
            return ReadConnectStrings[new Random().Next(0, i - 1)];
        }

        public static string GetWriteConnectString()
        {
            return WriteConnectString;
        }

    }
}