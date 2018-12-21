using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ExtensionMethods
/// </summary>
public static class ExtensionMethods
{   
    /// <summary>
    /// Allow for dasiy chain adding parameters and execute in one statement on DbCommand class
    /// </summary>
    /// <param name="sql">self reference</param>
    /// <param name="paramName">SQL Parameter to add</param>
    /// <param name="objValue">SQL Value to add</param>
    /// <returns>Return back the same DbCommand object to chain up more function call</returns>
    public static DataAccessLayer.DBCommand Add(this DataAccessLayer.DBCommand sql, string paramName, object objValue)
    {
        sql.AddParameter(paramName, objValue);
        return sql;
    }

    public static DataAccessLayer.DBCommand Update(this DataAccessLayer.DBCommand sql, string paramName, object objValue)
    {
        sql.UpdateParameter(paramName, objValue);
        return sql;
    }

    public static DataAccessLayer.DBCommand Add(this DataAccessLayer.DBCommand sql, string paramName,
        System.Data.ParameterDirection direction,  System.Data.DbType type, object objValue = null)
    {
        sql.AddParameter(paramName, direction, type, objValue);
        return sql;
    }

    public static DataAccessLayer.DBCommand Add(this DataAccessLayer.DBCommand sql, 
        DataAccessLayer.DBCommand dbCommand, string paramName, string outputParamName, 
        System.Data.ParameterDirection direction = System.Data.ParameterDirection.Input)
    {
        sql.AddParameter(dbCommand, paramName, outputParamName, direction);
        return sql;
    }

    public static string ToHtmlTable(this System.Data.DataTable dt)
    {
        return dt.ToHtmlTable("", "", "");
    }

    public static string ToHtmlTable(this System.Data.DataTable dt, string tableCss, string rowCss, string altRowCss)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder("<table id='" + dt.TableName + "'");
        if (tableCss != "") sb.Append(" class='" + tableCss + "'");
        sb.Append(">");

        // build header row
        sb.Append("<thead>");
        sb.Append(" <tr>");
        foreach (System.Data.DataColumn c in dt.Columns)
        {
            if (c.Caption.Trim() != "")
                sb.Append("<th>" + c.Caption + "</th>");
            else
                sb.Append("<th>" + c.ColumnName + "</th>");
        }
        sb.Append(" </tr>");
        sb.Append("</thead>");

        string css = "";
        // build data row
        sb.Append("<tbody>");
        foreach (System.Data.DataRow r in dt.Rows)
        {
            if (css != rowCss)
                css = rowCss;
            else
                css = altRowCss;

            if (css != "")
                sb.Append("<tr class='" + css + "'>");
            else
                sb.Append("<tr>");

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append("<td>" + r[i].ToString() + "</td>");
            }
            sb.Append("</tr>");
        }
        sb.Append("</tbody>");
        sb.Append("</table>");

        return sb.ToString();
    }

    public static string ToHtmlOptions(this System.Data.DataTable dt, string name, string value)
    {
        return dt.ToHtmlOptions(name, value, "", "", "");
    }

    public static string ToHtmlOptions(this System.Data.DataTable dt, string name, string value, string title)
    {
        return dt.ToHtmlOptions(name, value, title, "", "");
    }

    public static string ToHtmlOptions(this System.Data.DataTable dt, string name, string value, string title, string rowColor, string altRowColor)
    {
        string css = "";
        var sb = new System.Text.StringBuilder();
        foreach (System.Data.DataRow row in dt.Rows)
        {
            if (css == altRowColor)
                css = rowColor;
            else
                css = altRowColor;

            string bg = "";
            if (css != "") bg = " style='background-color:" + css + "'";

            string desc = "";
            if (title != "")
                desc = " title='" + row[title].ToBlank() + "' ";

            sb.Append("<option" + desc + bg + " value='" + row[value].ToString() + "'>" + row[name].ToString() + "</option>");
        }

        return sb.ToString();
    }

    #region Conversion Helper Functions

    public static string HtmlLineBreak(this object value)
    {
        return System.Text.RegularExpressions.Regex.Replace(value.ToString(), @"\r\n?|\n", "<br/>");
    }

    public static short? ToShortEx(this object value)
    {
        if (value == DBNull.Value) return null;
        if (value == null) return null;

        return Convert.ToInt16(value);
    }

    public static int ToInt(this object value)
    {
        if (value == DBNull.Value) return 0;
        if (value == null) return 0;

        return Convert.ToInt32(value);
    }

    public static int? ToIntEx(this object value)
    {
        if (value == DBNull.Value) return null;
        if (value == null) return null;

        return Convert.ToInt32(value);
    }

    public static long? ToLngEx(this object value)
    {
        if (value == DBNull.Value) return null;
        if (value == null) return null;

        return Convert.ToInt64(value);
    }

    public static long ToLng(this object value)
    {
        if (value == DBNull.Value) return 0;
        if (value == null) return 0;

        return Convert.ToInt64(value);
    }

    public static double? ToDoubleEx(this object value)
    {
        if (value == DBNull.Value) return null;
        if (value == null) return null;

        try
        {
            return Convert.ToDouble(value);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static double ToDouble(this object value)
    {
        if (value == DBNull.Value) return 0.0;
        if (value == null) return 0.0;

        try
        {
            return Convert.ToDouble(value);
        }
        catch (Exception)
        {
            return 0.0;
        }
    }

    public static decimal? ToDecimalEx(this object value)
    {
        if (value == DBNull.Value) return null;
        if (value == null) return null;

        try
        {
            return Convert.ToDecimal(value);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static bool? ToBoolEx(this object value)
    {
        if (value == DBNull.Value) return null;
        if (value == null) return null;

        return Convert.ToBoolean(value);
    }

    public static bool ToBool(this object value)
    {
        if (value == DBNull.Value) return false;
        if (value == null) return false;

        return Convert.ToBoolean(value);
    }

    public static DateTime? ToDateTimeEx(this object value)
    {
        if (value == DBNull.Value) return null;
        if (value == null) return null;
        if (value.ToBlank() == "") return null;

        return Convert.ToDateTime(value);
    }

    public static string ToShortDateFormatEx(this object value)
    {
        if (value == DBNull.Value) return "";
        if (value == null) return "";

        return Convert.ToDateTime(value).ToString("MM/dd/yyyy");
    }

    public static string ToStringEx(this object value)
    {
        if (value == DBNull.Value) return null;
        if (value == null) return null;

        return Convert.ToString(value);
    }

    public static string ToBlank(this object value)
    {
        if (value == DBNull.Value) return "";
        if (value == null) return "";

        return value.ToString().Trim();
    }

    public static string ToBlankHtml(this object value)
    {
        if (value == DBNull.Value) return "&nbsp;";
        if (value == null) return "&nbsp;";

        return value.ToString();
    }

    //public static string ToFixed(this object value, int numDigitAfterDecimal)
    //{
    //    if (value == DBNull.Value) value = 0.0;
    //    if (value == null) value = 0.0;

    //    return Microsoft.VisualBasic.Strings.FormatNumber(value, numDigitAfterDecimal, Microsoft.VisualBasic.TriState.True);
    //}

    //public static string ToFixedNoRounding(this object value, int numDigitAfterDecimal)
    //{
    //    if (value == DBNull.Value) value = 0.0;
    //    if (value == null) value = 0.0;

    //    string tmp = value.ToString();
    //    if (tmp.IndexOf('.') >= 0)
    //    {
    //        tmp = tmp.Substring(0, tmp.IndexOf('.') + numDigitAfterDecimal + 1);
    //    }

    //    return tmp.ToFixed(numDigitAfterDecimal);
    //} 

    //public static bool IsDate(this object value)
    //{
    //    return Microsoft.VisualBasic.Information.IsDate(value);
    //}

    #endregion
}