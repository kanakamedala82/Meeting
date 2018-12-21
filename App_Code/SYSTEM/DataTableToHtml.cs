using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public enum RowType
    {
        header,
        body,
        footer
    }

    /// <summary>
    /// when type = body then cols is null, when type = header then row is null
    /// </summary>
    public class RowEventArgs : EventArgs
    {
        string html = "";
        System.Data.DataRow row;
        System.Data.DataColumnCollection cols;
        RowType type;

        public string Html
        {
            get { return html; }
            set { html = value; }
        }

        public System.Data.DataRow Row
        {
            get { return row; }
            set { row = value; }
        }

        public System.Data.DataColumnCollection Cols
        {
            get { return cols; }
            set { cols = value; }
        }

        public RowType Type
        {
            get { return type; }
            set { type = value; }
        }
    }

    public delegate void RowCreatingEventHandler(object sender, RowEventArgs e);

    /// <summary>
    /// Take in a DataTable object and convert it to HTML string
    /// </summary>
    public class DataTableToHtml
    {
        #region Local Variables

        private System.Data.DataTable table;
        private string tableCss = "";
        private string rowCss = "";
        private string altRowCss = "";
        private bool enableTableSections = true;
        /// <summary>
        /// if event is hooked, then user need to build own HTML
        /// </summary>
        public event RowCreatingEventHandler RowCreating;

        #endregion

        #region Properties

        public System.Data.DataTable Table
        {
            get { return table; }
            set { table = value; }
        }

        public string TableCss
        {
            get { return tableCss; }
            set { tableCss = value; }
        }

        public string RowCss
        {
            get { return rowCss; }
            set { rowCss = value; }
        }

        public string AltRowCss
        {
            get { return altRowCss; }
            set { altRowCss = value; }
        }

        /// <summary>
        /// set to true to include thead, tbody and tfooter sections in HTML table output
        /// </summary>
        public bool EnableTableSections
        {
            get { return enableTableSections; }
            set { enableTableSections = value; }
        }

        #endregion

        #region Constructors

        public DataTableToHtml()
        { }

        public DataTableToHtml(System.Data.DataTable dataTable)
        {
            table = dataTable;
        }

        #endregion

        #region Methods

        protected virtual void OnRowCreating(RowEventArgs e)
        {
            if (RowCreating != null)
                RowCreating(this, e);
        }

        public string ToHtml()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("<table id='" + table.TableName + "'");
            if (tableCss != "") sb.Append(" class='" + tableCss + "'");
            sb.Append(">");

            // build header row
            if (enableTableSections) sb.Append("<thead>");

            if (true)
            {
                var e = new RowEventArgs() { Cols = table.Columns, Type = RowType.header };
                OnRowCreating(e);

                if (e.Html == "")
                {
                    sb.Append(" <tr>");
                    foreach (System.Data.DataColumn c in table.Columns)
                    {
                        if (c.Caption.Trim() != "")
                            sb.Append("<th sort='" + c.ColumnName + "'>" + c.Caption + "</th>");
                        else
                            sb.Append("<th sort='" + c.ColumnName + "'>" + c.ColumnName + "</th>");
                    }
                    sb.Append(" </tr>");
                }
                else
                {
                    sb.Append(e.Html);
                }
            }
            if (enableTableSections) sb.Append("</thead>");

            string css = "";
            // build data row
            if (enableTableSections) sb.Append("<tbody>");
            foreach (System.Data.DataRow r in table.Rows)
            {
                var e = new RowEventArgs() { Row = r, Type = RowType.body };
                OnRowCreating(e);
                
                // allow css calculation even if user defined their own html for a particular row or all rows
                if (css != rowCss)
                    css = rowCss;
                else
                    css = altRowCss;

                if (e.Html == "")
                {
                    if (css != "")
                        sb.Append("<tr class='" + css + "'>");
                    else
                        sb.Append("<tr>");

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        sb.Append("<td>" + r[i].ToString() + "</td>");
                    }
                    sb.Append("</tr>");
                }
                else
                {
                    sb.Append(e.Html);
                }
            }
            if (enableTableSections) sb.Append("</tbody>");

            // add footer for jTPS plugin
            if (enableTableSections)
            {
                sb.Append("<tfoot class='nav'>");
                sb.Append(" <tr>");
                sb.Append("  <td colspan='" + table.Columns.Count.ToString() + "'>");
                sb.Append("   <div class='pagination'></div>");
                sb.Append("   <div class='paginationTitle'></div>");
                sb.Append("   <div class='selectPerPage'></div>");
                sb.Append("   <div class='status'></div>");
                sb.Append("  </td>");
                sb.Append(" </tr>");
                sb.Append("</tfoot>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }

        #endregion
    }
}