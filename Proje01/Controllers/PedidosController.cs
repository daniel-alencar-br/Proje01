using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Proje01.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IncluirPedido()
        {
            ViewData["MSG"] = "Preencha os Dados...";

            return View(); 
        }

        [HttpPost]
        public ActionResult IncluirPedido(FormCollection Form)
        {
            try
            {
                // validações
                int iCod = Convert.ToInt32(Form["id"].ToString());
                DateTime Data = Convert.ToDateTime(Form["data"].ToString()); 
                string Produto = Form["prod"].ToString();
                int Qtd = Convert.ToInt32(Form["qtd"].ToString());

                // criar linha para guardar no arquivo
                string sLinha = "Pedido: " + iCod.ToString() + 
                                " - Data: " + Data.ToString() + 
                                " - Produto: " + Produto.ToString() + 
                                " - Quantidade: " + Qtd.ToString();

                StreamWriter Arq = new StreamWriter(
                        Server.MapPath("~/Dados/Pedidos.txt"), true);
                Arq.WriteLine(sLinha);
                Arq.Close();
            }
            catch (Exception erro)
            {
                ViewData["MSG"] = erro.Message;
                return View();
            }

            return Redirect("Index");
        }

        public ActionResult VerPedido()
        {
            StreamReader Arq = new StreamReader(
                        Server.MapPath("~/Dados/Pedidos.txt"));

            string sPedidos = Arq.ReadToEnd();
            
            Arq.Close();

            ViewData["Arq"] = sPedidos;

            return View();
        }
    }
}