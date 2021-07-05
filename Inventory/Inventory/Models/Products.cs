using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Products
    {
        public Products() { }

        #region Params
        public string Id { get; set; }
        public string tb_pro_name { get; set; }
        public string tb_pro_des { get; set; }
        public decimal? tb_pro_price { get; set; }
        public int tb_pro_status { get; set; }
        public string Mode { get; set; }
        public List<Products> _listProducts { get; set; }

        #endregion
        #region function
        public void GetApiProductData(ref List<dynamic> _list, Products _objProParameter)
        {
            
            DataSet ds = new DataSet();
            SqlParameter[] oparam = new SqlParameter[3];

            oparam[0] = new SqlParameter("@Id", _objProParameter.Id);
            oparam[1] = new SqlParameter("@tb_pro_name", _objProParameter.tb_pro_name);
            oparam[2] = new SqlParameter("@tb_pro_status", _objProParameter.tb_pro_name);

            try
            {
                ds = SqlHelper.ExecuteDataset(AppConfig.GetConnectionString(), CommandType.StoredProcedure, "tbp_products_select", oparam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtAPIUser = ds.Tables[0];
                        if (dtAPIUser.Rows.Count > 0)
                        {
                            foreach (DataRow drApplicant in dtAPIUser.Rows)
                            {
                                //this code is used for creating dynamic property according to database column name
                                // property name will be same as column name as per store procedure
                                dynamic apiUserLoginData = new ExpandoObject();
                                foreach (DataColumn dc in dtAPIUser.Columns)
                                {
                                    var expandoAPIUserLoginDic = apiUserLoginData as IDictionary<string, object>;
                                    expandoAPIUserLoginDic.Add(dc.ToString(), Convert.ToString(drApplicant[dc]));
                                }
                                _list.Add(apiUserLoginData);

                            }
                        }
                    }
                }
            }
            catch { }
        }

        public List<string> AddEditDeleteProductData(ref List<string> pro, Products _objProParameter)
        {
            SqlParameter[] oParam = new SqlParameter[5];

            oParam[0] = new SqlParameter("@Id", _objProParameter.Id);
            oParam[1] = new SqlParameter("@tb_pro_name", _objProParameter.tb_pro_name);
            oParam[2] = new SqlParameter("@tb_pro_des", _objProParameter.tb_pro_des);
            oParam[3] = new SqlParameter("@tb_pro_price", _objProParameter.tb_pro_price);          
            oParam[4] = new SqlParameter("@Mode", _objProParameter.Mode);

            oParam[0].Direction = ParameterDirection.InputOutput;
            oParam[0].DbType = DbType.String;
            oParam[0].Size = 256;

            try
            {
                SqlHelper.ExecuteNonQuery(AppConfig.GetConnectionString(), CommandType.StoredProcedure, "[tbp_products_insert_update]", oParam);
                pro.Add(oParam[0].Value.ToString());
                return pro;
            }
            catch (Exception ex)
            {
                pro.Add("-2");
                return pro;
            }
        }


        #endregion
    }
}