﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SimpleAds.Data
{
    public static class Extensions
    {
        public static T GetOrNull<T>(this SqlDataReader reader, string column)
        {
            object obj = reader[column];
            if (obj == DBNull.Value)
            {
                return default(T);
            }
            return (T)obj;
        }
    }
}
