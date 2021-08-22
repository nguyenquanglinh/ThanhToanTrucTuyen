using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThanhToanTrucTuyen.DTO;

namespace ThanhToanTrucTuyen.Controllers
{
    public class HomeController : Controller
    {
        private static DBAcess _ = new DBAcess();
        public ActionResult Index()
        {
            if (Session["cks"] == null || Session["id"] == null)
                return RedirectToAction("Login");
            return View(_.GetListSoTaiKhoanThe(Session["cks"].ToString(), Session["id"].ToString()));
        }
        public ActionResult Logout()
        {
            Session["cks"] = null; Session["id"] = null;
            return RedirectToAction("index");
        }
        public ActionResult ChuyenTien(string id)
        {
            if (Session["cks"] == null || Session["id"] == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpPost]
        public ActionResult ChuyenTien(string id, string Stk, string SoDuHienTai)
        {
            if (Session["cks"] == null || Session["id"] == null)
                return RedirectToAction("Login");
            var chuyen = _.GetListSoTaiKhoanThe(Session["cks"].ToString(), Session["id"].ToString()).Where(i => i.Id == id).FirstOrDefault();
            int soDuConLai = int.Parse(chuyen.SoDuHienTai) - int.Parse(SoDuHienTai);
            if (soDuConLai >= 0)
            {
                chuyen.SoDuHienTai = soDuConLai.ToString();
                var nhan = _.GetListSoTaiKhoanThe().Where(i => i.Stk == Stk).FirstOrDefault();
                int xxx = int.Parse(nhan.SoDuHienTai) + int.Parse(SoDuHienTai);
                nhan.SoDuHienTai = xxx.ToString();
                _.SuaSoTaiKhoanThe(chuyen);
                _.SuaSoTaiKhoanThe(nhan);
                return RedirectToAction("Index");
            }
            return View();


        }
        public ActionResult Details(string id)
        {
            if (Session["cks"] == null || Session["id"] == null)
                return RedirectToAction("Login");
            return View(_.GetListSoTaiKhoanThe(Session["cks"].ToString(), Session["id"].ToString()).Where(i => i.Id == id).FirstOrDefault());
        }
        public ActionResult Delete(string id)
        {
            if (Session["cks"] == null || Session["id"] == null)
                return RedirectToAction("Login");
            return View(_.GetListSoTaiKhoanThe(Session["cks"].ToString(), Session["id"].ToString()).Where(i => i.Id == id).FirstOrDefault());

        }
        [HttpPost]
        public ActionResult Delete(string id, string IdTK)
        {
            _.XoaSoTaiKhoanThe(id);
            return RedirectToAction("Index");
        }
        public ActionResult CreateTaiKhoanDangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTaiKhoanDangNhap(string TenDangNhap, string MatKhau, string MaPin, string SDT, string Quyen = "user")
        {
            _.ThemTaiKhoanDangNhap(new TaiKhoanDangNhapDTo(TenDangNhap, MatKhau, SDT, MaPin, Quyen));
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string TenDangNhap, string MatKhau, string SDT)
        {
            var v = _.GetListTaiKhoanDangNhap().Where(x => x.TenDangNhap == TenDangNhap && x.MatKhau == MatKhau && x.SDT == SDT).FirstOrDefault();
            if (v != null)
            {
                Session["id"] = v.Id;
                Session["cks"] = v.Cks;
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddSTK()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSTK(string Stk, string IdTK, string SoDuHienTai = "5000000")
        {
            if (Session["cks"] == null || Session["id"] == null)
                return RedirectToAction("Login");
            IdTK = Session["id"].ToString();
            _.ThemSoTaiKhoanThe(new SoTaiKhoanTheDTO(Stk, SoDuHienTai, IdTK, Session["cks"].ToString()));
            return RedirectToAction("Index");
        }
    }
}
