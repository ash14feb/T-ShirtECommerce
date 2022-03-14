using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; // Required for using Dataset , Datatable and Sql
using System.Data.SqlClient; // Required for Using Sql 
using System.Configuration; // for Using Connection From Web.config
using BLL;

namespace DataAccess
{

    public class DAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectStr"].ToString());


        public int AddProduct(Product ObjProd) // passing Bussiness object Here 
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Proc_AddUpdateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ObjProd.Id);
                cmd.Parameters.AddWithValue("@Description", ObjProd.Description);
                cmd.Parameters.AddWithValue("@Size", ObjProd.Size);
                cmd.Parameters.AddWithValue("@Colour", ObjProd.Colour);
                cmd.Parameters.AddWithValue("@Price", ObjProd.Price);
                cmd.Parameters.AddWithValue("@Made", ObjProd.Made);
                cmd.Parameters.AddWithValue("@Style", ObjProd.Style);
                cmd.Parameters.AddWithValue("@Gender", ObjProd.Gender);
                cmd.Parameters.AddWithValue("@Image", ObjProd.Image);
                con.Open();
                int Result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return Result;

            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
        }

        public int DeleteProduct(int Id) // passing Bussiness object Here 
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Proc_DeleteProd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                int Result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return Result;

            }
            catch
            {
                throw;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
        }

        public DataSet GetAllorSingleProducts(int Id)
        {
            DataSet dsProd = new DataSet();
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                SqlCommand objSqlCommand = new SqlCommand("Proc_GetAllorSingleProduct", connection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                try
                {
                    objSqlDataAdapter.Fill(dsProd);
                    dsProd.Tables[0].TableName = "Products";
                }
                catch (Exception ex)
                {
                    return dsProd;
                }
            }
            return dsProd;
        }
    }
}
