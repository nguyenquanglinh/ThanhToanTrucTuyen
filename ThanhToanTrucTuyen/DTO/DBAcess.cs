using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ThanhToanTrucTuyen.DTO
{
    public class DBAcess
    {
        #region parameter connection
        SqlConnection connection = new SqlConnection();
        public static string strConnString = "Data Source =" + "DESKTOP-NOP0M9L\\SQLEXPRESS;Database = ThanhToanTrucTuyen; Integrated Security=SSPI;";
        #endregion

        #region function cơ bản 
        private void MoKetNoi()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }
            }
            catch
            {
                throw new Exception("Khong the mo duoc cong ket noi");
            }
        }
        public DBAcess()
        {
            MoKetNoi();
        }
        public bool HuyKetNoi()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return true;
            }
            catch
            {
                throw new Exception("Khong the huy cong ket noi");
            }
        }
        public DataTable LayDuLieuTruyVanSQL(string Sql)
        {
            MoKetNoi();
            var ada = new SqlDataAdapter(Sql, connection);
            var dta = new DataTable();
            ada.Fill(dta);
            return dta;
        }
        public bool ThucThiTruyVan(string sql)
        {
            try
            {
                MoKetNoi();
                var cmd = new SqlCommand(sql, connection);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    HuyKetNoi();
                    return true;
                }
                return false;

            }
            catch
            {
                throw new Exception("Khong the thuc thi truy van");
            }
        }
        #endregion

        #region Tai khoản
        public List<TaiKhoanDangNhapDTo> GetListTaiKhoanDangNhap(string id = null)
        {
            var ret = new List<TaiKhoanDangNhapDTo>();
            string sql = "select * from TaiKhoanDangNhap";
            if (id != null)
                sql = "select * from TaiKhoanDangNhap where id =" + id;
            var x = LayDuLieuTruyVanSQL(sql);
            for (int i = 0; i < x.Rows.Count; i++)
            {
                ret.Add(new TaiKhoanDangNhapDTo(x.Rows[i]));
            }
            return ret;
        }
        public List<TaiKhoanDangNhapDTo> ThemTaiKhoanDangNhap(TaiKhoanDangNhapDTo da)
        {
            string sql = "insert into TaiKhoanDangNhap(tenDangNhap,sdt,matKhau,maPin,quyen,cks)values(N'" + da.TenDangNhap +
                "',N'" + da.SDT + "',N'" + da.MatKhau + "',N'" + da.MaPin + "',N'" + da.Quyen + "',N'" + da.Cks + "')";
            ThucThiTruyVan(sql);
            return GetListTaiKhoanDangNhap();
        }
        #endregion
        #region thẻ
        public List<SoTaiKhoanTheDTO> GetListSoTaiKhoanThe(string cks=null, string idTK = null)
        {
            var ret = new List<SoTaiKhoanTheDTO>();
            string sql = "select * from SoTaiKhoanThe";
            if (idTK != null)
                sql = "select * from SoTaiKhoanThe where idTK =" + idTK;
            var x = LayDuLieuTruyVanSQL(sql);
            for (int i = 0; i < x.Rows.Count; i++)
            {
                ret.Add(new SoTaiKhoanTheDTO(x.Rows[i]));
            }
            return ret;
        }
        public bool ThemSoTaiKhoanThe(SoTaiKhoanTheDTO da)
        {
            string sql = "insert into SoTaiKhoanThe(stk,soDuHienTai,idTK,cks)values(N'" + da.Stk +
                "',N'" + da.SoDuHienTai + "',N'" + da.IdTK + "',N'" + da.Cks + "')";
            return ThucThiTruyVan(sql);
        }
        public bool XoaSoTaiKhoanThe(string id)
        {
            string sql = "delete SoTaiKhoanThe where id ='" + id + "'";
            return ThucThiTruyVan(sql);
        }
        public bool SuaSoTaiKhoanThe(SoTaiKhoanTheDTO da)
        {
            da.EncryptSoTaiKhoanThe();
            string sql = "update SoTaiKhoanThe set soDuHienTai='" + da.SoDuHienTai + "' where id =" + da.Id;
            return ThucThiTruyVan(sql);
        }
        #endregion
    }
}