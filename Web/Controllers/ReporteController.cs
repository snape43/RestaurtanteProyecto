using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult RptVentasFecha()
        {

            return View();

        }
        public ActionResult DescargarReporteVentasFecha(String fechaInicial, String fechafinal)
        {
            try
            {
                var rpt = new ReportClass();
                rpt.FileName = Server.MapPath("/Reports/ReporteVentas.rpt");
                rpt.Load();

                rpt.SetParameterValue("PIfecha", fechaInicial);
                rpt.SetParameterValue("PFfecha", fechafinal);

                // Report connection
                var connInfo = CrystalReportsRest.GetConnectionInfo();
                TableLogOnInfo logonInfo = new TableLogOnInfo();
                Tables tables;
                tables = rpt.Database.Tables;
                foreach (Table table in tables)
                {
                    logonInfo = table.LogOnInfo;
                    logonInfo.ConnectionInfo = connInfo;
                    table.ApplyLogOnInfo(logonInfo);
                }

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
                rpt.Dispose();
                rpt.Clone();
                return new FileStreamResult(stream, "application.pdf");
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }


        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult RptVentasPago()
        {

            return View();

        }
        public ActionResult DescargarReporteVentasPago(String fechaInicial, String fechafinal, string tipopago)
        {
            try
            {
                var rpt = new ReportClass();
                rpt.FileName = Server.MapPath("/Reports/ReporteVentasTipo.rpt");
                rpt.Load();

                rpt.SetParameterValue("PIfecha", fechaInicial);
                rpt.SetParameterValue("PFfecha", fechafinal);
                rpt.SetParameterValue("Ppago", tipopago);

                // Report connection
                var connInfo = CrystalReportsRest.GetConnectionInfo();
                TableLogOnInfo logonInfo = new TableLogOnInfo();
                Tables tables;
                tables = rpt.Database.Tables;
                foreach (Table table in tables)
                {
                    logonInfo = table.LogOnInfo;
                    logonInfo.ConnectionInfo = connInfo;
                    table.ApplyLogOnInfo(logonInfo);
                }

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
                rpt.Dispose();
                rpt.Clone();
                return new FileStreamResult(stream, "application.pdf");
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        public JsonResult GetTipoPago()
        {
            MyContext ctx = new MyContext();
            var cats = ctx.TipoPago.Select(x => x.TipoPago1).ToList();
            return Json(new { resultado = cats }, JsonRequestBehavior.AllowGet);
        }

    }
}