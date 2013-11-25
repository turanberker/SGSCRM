using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace VeriErisimKatmani
{
    public enum SorguTipi
    {
        text, procedure
    }
    public class Vek
    {
        SqlConnection con;
        public Vek(string ConnectionString)
        {
            con = new SqlConnection(ConnectionString);
        }
        #region Connection
        void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        #endregion
        #region Command
        SqlCommand CreateCommand(string sorgu, CommandType tip)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = tip;
            cmd.CommandText = sorgu;

            return cmd;
        }
        string[] GetParametersFromQuery(string sorgu)
        {
            MatchCollection col = Regex.Matches(sorgu, @"@\w+");
            string[] parametreler = new string[col.Count];
            for (int i = 0; i < parametreler.Length; i++)
            {
                parametreler[i] = col[i].Value;
            }
            return parametreler;
        }
        string[] GetParametersFromProcedure(string spname)
        {
            string query = "select PARAMETER_NAME from INFORMATION_SCHEMA.PARAMETERS where SPECIFIC_NAME=@name";
            SqlDataReader dr = ExecuteReader(CommandType.Text, query, spname);
            List<string> parametreler = new List<string>();
            while (dr.Read())
            {
                parametreler.Add(dr["PARAMETER_NAME"].ToString());
            }
            dr.Close();
            return parametreler.ToArray();
        }
        void SetParameters(SqlCommand cmd, object[] values)
        {
            string[] parametreler;
            if (cmd.CommandType == CommandType.Text)
            {
                parametreler = GetParametersFromQuery(cmd.CommandText);
            }
            else
            {
                parametreler = GetParametersFromProcedure(cmd.CommandText);
            }
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == null)
                {
                    cmd.Parameters.AddWithValue(parametreler[i], DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parametreler[i], values[i]);
                }

            }
        }

        #endregion


        #region ExecuteOtomatik
        ///<summary>
        ///İstenen Tipteki Entity'i İlgili Tabloya Ekler ve ID'sini döner.
        ///</summary>
        public long Insert<T>(Object Entity)
        {
            T entity = System.Activator.CreateInstance<T>();
            entity = Cast<T>(Entity);
            string EntityName = typeof(T).Name;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Insert into {0} ", EntityName);
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();
            string ilk = "(";
            string Values = "(";
            #region Sorgu
            int indexcount = 0;
            List<object> parametreler = new List<object>();
            foreach (PropertyInfo item in entityProperties)
            {
                TableProp attribute = Attribute.GetCustomAttribute(item, typeof(TableProp)) as TableProp;
                if (attribute.PK.ToString() == "PK")
                    continue;
                object obje = item.GetValue(entity, null);
                if (obje == null)
                    continue;
                parametreler.Add(obje);
                if (ilk.Count() > 1)
                {
                    ilk += " , ";
                    Values += " , ";
                }
                string propName = string.Empty;
                if (propName.Equals(string.Empty))
                    propName = item.Name;
                ilk += propName;
                Values += "@" + propName;
                indexcount++;
            }
            ilk += ")";
            Values += ")";
            #endregion
            string sorgu = sb.ToString() + ilk + " Values " + Values + " select @@Identity";
            return Convert.ToInt64(ExecuteScalar(CommandType.Text, sorgu, parametreler.ToArray()));
        }
        ///<summary>
        ///İstenen Tipteki Entity'nin Bütün Propertilerini Günceller.
        ///</summary>
        public bool Update<T>(Object Entity)
        {
            T entity = System.Activator.CreateInstance<T>();
            entity = Cast<T>(Entity);
            string EntityName = typeof(T).Name;
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();
            string Values = "";
            string WhereCriteri = " where ";
            object ID = 0;
            #region Sorgu
            List<object> parametreler = new List<object>();
            foreach (PropertyInfo item in entityProperties)
            {
                TableProp attribute = Attribute.GetCustomAttribute(item, typeof(TableProp)) as TableProp;
                object obje = item.GetValue(entity, null);

                if (attribute.PK.ToString() == "PK")
                {
                    WhereCriteri += item.Name + "=@" + item.Name;
                    ID = obje;
                }
                else
                {
                    if (string.IsNullOrEmpty(Values))
                        Values += " " + item.Name + " = @" + item.Name + " ";
                    else
                        Values += " ," + item.Name + " = @" + item.Name + " ";
                    parametreler.Add(obje);
                }
            }
            parametreler.Add(ID);
            sb.AppendFormat("Update {0} Set {1} {2} ", EntityName, Values, WhereCriteri);
            #endregion
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), parametreler.ToArray()) > 0 ? true : false;
        }


        ///<summary>
        ///Girilen Entity'nin propertilerine sahip satırları siler. Delete from TabloAdı where Prop1=val1 or prop2=Val2.
        ///</summary>
        public bool Delete<T>(Object Entity)
        {
            T entity = System.Activator.CreateInstance<T>();
            entity = Cast<T>(Entity);
            string EntityName = typeof(T).Name;
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();
            string WhereCriteri = "";
            #region Sorgu
            List<object> parametreler = new List<object>();
            foreach (PropertyInfo item in entityProperties)
            {
                TableProp attribute = Attribute.GetCustomAttribute(item, typeof(TableProp)) as TableProp;
                object obje = item.GetValue(entity, null);
                if (obje == null)
                    continue;
                else
                {
                    if (string.IsNullOrEmpty(WhereCriteri))
                        WhereCriteri += " " + item.Name + " = @" + item.Name + " ";
                    else
                        WhereCriteri += " OR " + item.Name + " = @" + item.Name + " ";
                    parametreler.Add(obje);
                }
            }
            sb.AppendFormat("Delete from {0} where {1}  ", EntityName, WhereCriteri);
            #endregion
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), parametreler.ToArray()) > 0 ? true : false;
        }
        ///<summary>
        ///İstenen Tablonun Bütün Satırlarını Döner.
        ///</summary>
        public List<T> Select<T>()
        {
            T entity = System.Activator.CreateInstance<T>();
            string EntityName = typeof(T).Name;
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();
            #region Sorgu
            List<object> parametreler = new List<object>();
            sb.AppendFormat("Select * from {0}", EntityName);
            #endregion
            return ExecuteReader<T>(CommandType.Text, sb.ToString());
        }
        ///<summary>
        ///Girilen Entity'nin propertilerine sahip satırları getirir. Select * from TabloAdı where Prop1=val1 or prop2=Val2.
        ///</summary>
        public List<T> Select<T>(Object Entity)
        {
            T entity = System.Activator.CreateInstance<T>();
            entity = Cast<T>(Entity);
            string EntityName = typeof(T).Name;
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();
            string WhereCriteri = "";
            #region Sorgu
            List<object> parametreler = new List<object>();
            foreach (PropertyInfo item in entityProperties)
            {
                TableProp attribute = Attribute.GetCustomAttribute(item, typeof(TableProp)) as TableProp;
                object obje = item.GetValue(entity, null);
                if (obje == null)
                    continue;
                else
                {
                    if (string.IsNullOrEmpty(WhereCriteri))
                        WhereCriteri += " " + item.Name + " = @" + item.Name + " ";
                    else
                        WhereCriteri += " OR " + item.Name + " = @" + item.Name + " ";
                    parametreler.Add(obje);
                }
            }
            sb.AppendFormat("Select * from {0} where {1}", EntityName, WhereCriteri);
            #endregion
            if (parametreler != null)
                return ExecuteReader<T>(CommandType.Text, sb.ToString(), parametreler.ToArray());
            else
                return ExecuteReader<T>(CommandType.Text, sb.ToString());
        }
        ///<summary>
        ///İstenen Tablonun Bütün Satırlarını Döner.
        ///</summary>
        public DataTable GetDataTable<T>()
        {
            T entity = System.Activator.CreateInstance<T>();
            string EntityName = typeof(T).Name;
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();
            #region Sorgu
            List<object> parametreler = new List<object>();
            sb.AppendFormat("Select * from {0}", EntityName);
            #endregion
            return GetDataTable(CommandType.Text, sb.ToString());
        }
        ///<summary>
        ///Girilen Entity'nin propertilerine sahip satırları getirir. Select * from TabloAdı where Prop1=val1 or prop2=Val2.
        ///</summary>
        public DataTable GetDataTable<T>(Object Entity)
        {
            T entity = System.Activator.CreateInstance<T>();
            entity = Cast<T>(Entity);
            string EntityName = typeof(T).Name;
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();
            string WhereCriteri = "";
            #region Sorgu
            List<object> parametreler = new List<object>();
            foreach (PropertyInfo item in entityProperties)
            {
                TableProp attribute = Attribute.GetCustomAttribute(item, typeof(TableProp)) as TableProp;
                object obje = item.GetValue(entity, null);
                if (obje == null)
                    continue;
                else
                {
                    if (string.IsNullOrEmpty(WhereCriteri))
                        WhereCriteri += " " + item.Name + " = @" + item.Name + " ";
                    else
                        WhereCriteri += " OR " + item.Name + " = @" + item.Name + " ";
                    parametreler.Add(obje);
                }
            }
            sb.AppendFormat("Select * from {0} where {1}", EntityName, WhereCriteri);
            #endregion
            if (parametreler != null)
                return GetDataTable(CommandType.Text, sb.ToString(), parametreler.ToArray());
            else
                return GetDataTable(CommandType.Text, sb.ToString());
        }
        #endregion
        #region Execute Metotları


        ///<summary>
        ///Verilen Sorguyu Çalıştırır. Etkilenen Satırların Sayısını Döner.
        ///</summary>
        public int ExecuteNonQuery(CommandType tip, string sorgu, params object[] degerler)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            SetParameters(cmd, degerler);
            OpenConnection();
            int sonuc = cmd.ExecuteNonQuery();
            CloseConnection();
            return sonuc;
        }
        ///<summary>
        ///Verilen Sorguyu Çalıştırır. Etkilenen Satırların Sayısını Döner.
        ///</summary>
        public int ExecuteNonQuery(CommandType tip, string sorgu)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            OpenConnection();
            int sonuc = cmd.ExecuteNonQuery();
            CloseConnection();
            return sonuc;
        }
        ///<summary>
        ///Verilen Sorguyu Çalıştırır. İlk satırın İlk stunundaki veriyi Döner.
        ///</summary>
        public object ExecuteScalar(CommandType tip, string sorgu, params object[] degerler)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            SetParameters(cmd, degerler);
            OpenConnection();
            object sonuc = cmd.ExecuteScalar();
            CloseConnection();
            return sonuc;
        }
        ///<summary>
        ///Verilen Sorguyu Çalıştırır. İlk satırın İlk stunundaki veriyi Döner.
        ///</summary>
        public object ExecuteScalar(CommandType tip, string sorgu)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            OpenConnection();
            object sonuc = cmd.ExecuteScalar();
            CloseConnection();
            return sonuc;
        }
        ///<summary>
        ///Verilen Sorguyu Çalıştırır.DataReader Döner.
        ///</summary>
        public SqlDataReader ExecuteReader(CommandType tip, string sorgu, params object[] degerler)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            SetParameters(cmd, degerler);
            OpenConnection();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
        ///<summary>
        ///Verilen Sorguyu Çalıştırır.DataReader Döner.
        ///</summary>
        public SqlDataReader ExecuteReader(CommandType tip, string sorgu)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            OpenConnection();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
        ///<summary>
        ///Verilen Sorguyu Çalıştırır.İstenen Tipte Liste Verir.
        ///</summary>
        public List<T> ExecuteReader<T>(CommandType tip, string sorgu)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            OpenConnection();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return DataTableToEntityList<T>(DataReadertoDataTable(dr, typeof(T)));
        }
        ///<summary>
        ///Verilen Sorguyu Çalıştırır.İstenen Tipte Liste Verir.
        ///</summary>
        public List<T> ExecuteReader<T>(CommandType tip, string sorgu, params object[] degerler)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            SetParameters(cmd, degerler);
            OpenConnection();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return DataTableToEntityList<T>(DataReadertoDataTable(dr, typeof(T)));
        }

        //public DataTable GetDataTable(CommandType tip, string sorgu, params object[] degerler)
        //{
        //    SqlCommand cmd = CreateCommand(sorgu, tip);
        //    SetParameters(cmd, degerler);
        //    OpenConnection();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    for (int i = 0; i < dr.FieldCount; i++)
        //    {
        //        dt.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
        //    }
        //    while (dr.Read())
        //    {
        //        DataRow drow = dt.NewRow();
        //        for (int i = 0; i < dr.FieldCount; i++)
        //        {
        //            drow[i] = dr[i];
        //        }
        //        dt.Rows.Add(drow);
        //    }
        //    dr.Close();
        //    CloseConnection();
        //    return dt;


        //}

        ///<summary>
        ///Verilen Sorguyu Çalıştırır.DataTable Döner.
        ///</summary>
        public DataTable GetDT(CommandType tip, DataTable dt, string sorgu, params object[] degerler)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            SetParameters(cmd, degerler);
            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            Da.Fill(dt);
            return (dt);
        }
        ///<summary>
        ///Verilen Sorguyu Çalıştırır.DataTable Döner.
        ///</summary>
        public DataTable GetDataTable(CommandType tip, string sorgu, params object[] degerler)
        {
            SqlCommand cmd = CreateCommand(sorgu, tip);
            SetParameters(cmd, degerler);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataColumn col1 = new DataColumn();
            col1.ColumnName = "SNo";
            col1.AutoIncrement = true;
            col1.AutoIncrementSeed = 1;
            col1.AutoIncrementStep = 1;
            dt.Columns.Add(col1);
            //foreach (IDataParameter data in da.GetFillParameters())
            //{
            //    dt.Columns.Add(data.ParameterName,DbTypetoType(data.DbType));
            //}
            da.Fill(dt);
            return dt;
        }

        #endregion


        private DataTable DataTableOlustur(Type Entity)
        {
            DataTable dt = new DataTable();
            //foreach (PropertyInfo info in Entity.GetProperties())
            //{
            //   // dt.Columns.Add(new DataColumn(info.Name, info.PropertyType.GetType()));
            //    dt.Columns.Add(new DataColumn());
            //}
            return dt;
        }
        ///<summary>
        ///Listeyi'yi DataTable'a Dönüştürür.
        ///</summary>
        public DataTable DataReadertoDataTable(SqlDataReader dr, Type Entity)
        {
            DataTable dt = DataTableOlustur(Entity);
            dt.Load(dr, LoadOption.PreserveChanges);
            return dt;
        }

        //List<T> ListeVer<T>(SqlDataReader dr)
        //{
        //    DataTable dt = DataTableOlustur(typeof(T));
        //    dt.Load(dr);
        //    List<T> liste = new List<T>();
        //    object obj;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        obj = row;
        //        liste.Add((T)obj);
        //    }
        //    List<T> listOfEntityObjects = liste.Cast<T>().ToList();
        //    return listOfEntityObjects;
        //}

        private List<T> DataTableToEntityList<T>(DataTable dtSource)
        {
            string propName = string.Empty;
            List<T> entityList = new List<T>();

            foreach (DataRow dr in dtSource.Rows)
            {
                // Create Instance of the Type T
                T entity = System.Activator.CreateInstance<T>();

                // Get all properties of the Type T
                System.Reflection.PropertyInfo[]
                entityProperties = typeof(T).GetProperties();

                // Loop through the properties defined in the 
                // entityList entity object and mapped the value
                foreach (System.Reflection.PropertyInfo item in
                        entityProperties)
                {
                    propName = string.Empty;
                    if (propName.Equals(string.Empty))
                        propName = item.Name;

                    if (dtSource.Columns.Contains(propName))
                    {
                        // Assign value to the property
                        try
                        {
                            item.SetValue(entity, dr[propName].GetType().Name.Equals(typeof(DBNull).Name) ? null : dr[propName], null);
                        }
                        catch
                        {
                        }
                    }
                }
                entityList.Add(entity);
            }
            return entityList;
        }

        private Type DbTypetoType(DbType dbType)
        {
            Type toReturn = typeof(DBNull);
            switch (dbType)
            {
                case DbType.String:
                    toReturn = typeof(string);
                    break;

                case DbType.UInt64:
                    toReturn = typeof(UInt64);
                    break;

                case DbType.Int64:
                    toReturn = typeof(Int64);
                    break;

                case DbType.Int32:
                    toReturn = typeof(Int32);
                    break;

                case DbType.UInt32:
                    toReturn = typeof(UInt32);
                    break;

                case DbType.Single:
                    toReturn = typeof(float);
                    break;

                case DbType.Date:
                    toReturn = typeof(DateTime);
                    break;

                case DbType.DateTime:
                    toReturn = typeof(DateTime);
                    break;

                case DbType.Time:
                    toReturn = typeof(DateTime);
                    break;

                case DbType.StringFixedLength:
                    toReturn = typeof(string);
                    break;

                case DbType.UInt16:
                    toReturn = typeof(UInt16);
                    break;

                case DbType.Int16:
                    toReturn = typeof(Int16);
                    break;

                case DbType.SByte:
                    toReturn = typeof(byte);
                    break;

                case DbType.Object:
                    toReturn = typeof(object);
                    break;

                case DbType.AnsiString:
                    toReturn = typeof(string);
                    break;

                case DbType.AnsiStringFixedLength:
                    toReturn = typeof(string);
                    break;

                case DbType.VarNumeric:
                    toReturn = typeof(decimal);
                    break;

                case DbType.Currency:
                    toReturn = typeof(double);
                    break;

                case DbType.Binary:
                    toReturn = typeof(byte[]);
                    break;

                case DbType.Decimal:
                    toReturn = typeof(decimal);
                    break;

                case DbType.Double:
                    toReturn = typeof(Double);
                    break;

                case DbType.Guid:
                    toReturn = typeof(Guid);
                    break;

                case DbType.Boolean:
                    toReturn = typeof(bool);
                    break;
            }

            return toReturn;
        }

        private T Cast<T>(object o)
        {
            return (T)o;
        }
    }
}
