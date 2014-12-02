using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestAPI
{
    public partial class Checklist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["RestAPIContext"].ConnectionString;

            Response.Write("CONNECTING TO: " + connectionString + System.Environment.NewLine);

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                Response.Write("SQL SERVER CONNECTED..." + System.Environment.NewLine);

                connection.Open();

                Response.Write("CONNECTION STATUS: " + connection.State);
            }
            catch (Exception ex)
            {
                Response.Write("ERROR CONNECTING SQL SERVER..." + System.Environment.NewLine);
                Response.Write(ex.Message);
            }
        }
    }
}