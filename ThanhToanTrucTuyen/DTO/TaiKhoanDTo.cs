using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ThanhToanTrucTuyen.DTO
{
    public abstract class Table
    {
        public string Id { get; set; }
    }
    public class TaiKhoanDangNhapDTo : Table
    {
        public String TenDangNhap { get; set; }
        public String SDT { get; set; }
        public String MatKhau { get; set; }
        public String MaPin { get; set; }
        public String Cks { get; set; }
        public String Quyen { get; set; }
        public TaiKhoanDangNhapDTo(string tenDangNhap, string matKhau, string sdt, string maPin, string quyen = "user")
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            SDT = sdt;
            MaPin = maPin;
            Quyen = quyen;
            EncryptTaiKhoanDangNhap();
        }

        public TaiKhoanDangNhapDTo(DataRow dataRow)
        {
            Cks = dataRow["cks"].ToString().Trim();
            Id = dataRow["id"].ToString().Trim();
            TenDangNhap = dataRow["tenDangNhap"].ToString().Trim();
            MatKhau = dataRow["matKhau"].ToString().Trim();
            MaPin = dataRow["maPin"].ToString().Trim();
            SDT = dataRow["sdt"].ToString().Trim();
            Quyen = dataRow["quyen"].ToString().Trim();
            DecryptTaiKhoanDangNhap();
        }
        public void EncryptTaiKhoanDangNhap()
        {
            Cks = AesOperation.Genera(TenDangNhap + MatKhau + SDT + Quyen + MaPin);
            TenDangNhap = AesOperation.EncryptString(TenDangNhap, Cks);
            MatKhau = AesOperation.EncryptString(MatKhau, Cks);
            MaPin = AesOperation.EncryptString(MaPin, Cks);
            SDT = AesOperation.EncryptString(SDT, Cks);
            Quyen = AesOperation.EncryptString(Quyen, Cks);
        }
        public void DecryptTaiKhoanDangNhap()
        {
            TenDangNhap = AesOperation.DecryptString(TenDangNhap, Cks);
            MatKhau = AesOperation.DecryptString(MatKhau, Cks);
            MaPin = AesOperation.DecryptString(MaPin, Cks);
            SDT = AesOperation.DecryptString(SDT, Cks);
            Quyen = AesOperation.DecryptString(Quyen, Cks);
        }
    }
    public class SoTaiKhoanTheDTO : Table
    {
        public String Stk { get; set; }
        public String SoDuHienTai { get; set; }
        public String IdTK { get; set; }
        public String Cks { get; set; }
        public SoTaiKhoanTheDTO(DataRow dataRow)
        {
            Id = dataRow["id"].ToString().Trim();
            Stk = dataRow["stk"].ToString().Trim();
            SoDuHienTai = dataRow["soDuHienTai"].ToString().Trim();
            IdTK = dataRow["idTK"].ToString().Trim();
            Cks = dataRow["cks"].ToString().Trim();
            DecryptSoTaiKhoanThe();
        }

        public SoTaiKhoanTheDTO(string stk, string soDuHienTai, string idTK, string cks)
        {
            Stk = stk;
            SoDuHienTai = soDuHienTai;
            IdTK = idTK;
            Cks = cks;
            EncryptSoTaiKhoanThe();
        }

        public void EncryptSoTaiKhoanThe()
        {
            Stk = AesOperation.EncryptString(Stk, Cks);
            SoDuHienTai = AesOperation.EncryptString(SoDuHienTai, Cks);
        }
        public void DecryptSoTaiKhoanThe()
        {
            Stk = AesOperation.DecryptString(Stk, Cks);
            SoDuHienTai = AesOperation.DecryptString(SoDuHienTai, Cks);
        }

    }
}