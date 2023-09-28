using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using U4_W4_D3.Models;

namespace U4_W4_D3.Controllers
{
    public class ScarpaController : Controller
    {
        // GET: Scarpa
        public ActionResult Index()
        {
            List<Scarpa> lista = new List<Scarpa>();
            lista = DB.getProdotti();
            List<Scarpa> listaDisponibili = new List<Scarpa>();
            foreach (Scarpa scarpa in lista)
            {
                if (scarpa.Disponibile == true)
                {
                    listaDisponibili.Add(scarpa);
                }
            }
            return View(listaDisponibili);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UsernameL, string PasswordL)
        {
            List<Utente> userList = new List<Utente>();
            userList = DB.getUsers();
            foreach (Utente u in userList)
            {
                if (UsernameL == u.Username && PasswordL == u.Password)
                {
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ErrorMessage = "Utente non trovato";
            return View("Login");
        }

        [HttpPost]
        public ActionResult Register(string UsernameR, string PasswordR)
        {
            if (!string.IsNullOrEmpty(UsernameR) && !string.IsNullOrEmpty(PasswordR))
            {
                DB.insertUser(UsernameR, PasswordR);
                FormsAuthentication.SetAuthCookie(UsernameR, false);
                return RedirectToAction("Index"); 
            }
            ViewBag.ErrorMessage = "Errore durante la registrazione";
            return View("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize(Users = "Admin")]
        public ActionResult Admin()
        {
            List<Scarpa> lista = new List<Scarpa>();
            lista = DB.getProdotti();
            return View(lista);
        }

        public ActionResult Detail(int id)
        {
            Scarpa scarpaSelected = new Scarpa();
            List<Scarpa> lista = new List<Scarpa>();
            lista = DB.getProdotti();
            foreach(Scarpa scarpa in lista)
            {
                if(scarpa.IdProdotto == id)
                {
                    scarpaSelected = scarpa;
                    break;
                }
            }
            ViewData["Scarpa"] = scarpaSelected;
            return View(scarpaSelected);
        }

        [Authorize(Users = "Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Users = "Admin")]
        [HttpPost]
        public ActionResult Add(Scarpa s, HttpPostedFileBase Image, HttpPostedFileBase Image1, HttpPostedFileBase Image2)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    string nomeFile = Image.FileName;
                    string pathToSave = Path.Combine(Server.MapPath("~/Content/Img"), nomeFile);
                    Image.SaveAs(pathToSave);
                    s.Image = Image.FileName;
                }
                else
                {
                    s.Image = "";
                }
                if (Image1 != null && Image1.ContentLength > 0)
                {
                    string nomeFile = Image1.FileName;
                    string pathToSave = Path.Combine(Server.MapPath("~/Content/Img"), nomeFile);
                    Image1.SaveAs(pathToSave);
                    s.Image1 = Image1.FileName;
                }
                else
                {
                    s.Image1 = "";
                }
                if (Image2 != null && Image2.ContentLength > 0)
                {
                    string nomeFile = Image2.FileName;
                    string pathToSave = Path.Combine(Server.MapPath("~/Content/Img"), nomeFile);
                    Image2.SaveAs(pathToSave);
                    s.Image2 = Image2.FileName;
                }
                else
                {
                    s.Image2 = "";
                }
                DB.insertScarpa(s);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Messaggio = "Campi non inseriti correttamente";
                return View();
            }
        }

        [Authorize(Users = "Admin")]
        public ActionResult Edit(int id)
        {
            List<Scarpa> lista = new List<Scarpa>();
            lista = DB.getProdotti();
            Scarpa scarpaSelected = new Scarpa();
            foreach(Scarpa s in lista)
            {
                if(s.IdProdotto == id)
                {
                    scarpaSelected = s;
                    break;
                }
            }
            return View(scarpaSelected);
        }

        [Authorize(Users = "Admin")]
        [HttpPost]
        public ActionResult Edit(Scarpa s, HttpPostedFileBase Image, HttpPostedFileBase Image1, HttpPostedFileBase Image2)
        {
            int idProd = Convert.ToInt16(TempData["IdProdotto"]);
            List<Scarpa> lista = new List<Scarpa>();
            lista = DB.getProdotti();
            string image = "";
            string image1 = "";
            string image2 = "";
            foreach (Scarpa scarpa in lista)
            {
                if (scarpa.IdProdotto == idProd)
                {
                    image = scarpa.Image;
                    image1 = scarpa.Image;
                    image2 = scarpa.Image;
                    s.IdProdotto = scarpa.IdProdotto;
                    break;
                }
            }
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    string nomeFile = Image.FileName;
                    string pathToSave = Path.Combine(Server.MapPath("~/Content/Img"), nomeFile);
                    Image.SaveAs(pathToSave);
                    s.Image = Image.FileName;
                }
                else
                {
                    s.Image = image;
                }
                if (Image1 != null && Image1.ContentLength > 0)
                {
                    string nomeFile = Image1.FileName;
                    string pathToSave = Path.Combine(Server.MapPath("~/Content/Img"), nomeFile);
                    Image1.SaveAs(pathToSave);
                    s.Image1 = Image1.FileName;
                }
                else
                {
                    s.Image1 = image1;
                }
                if (Image2 != null && Image2.ContentLength > 0)
                {
                    string nomeFile = Image2.FileName;
                    string pathToSave = Path.Combine(Server.MapPath("~/Content/Img"), nomeFile);
                    Image2.SaveAs(pathToSave);
                    s.Image2 = Image2.FileName;
                }
                else
                {
                    s.Image2 = image2;
                }
                DB.editScarpa(s);
                TempData["Salvataggio"] = "Modifica effettuata";
                return RedirectToAction("Admin");
            }
            else
            {
                ViewBag.MessaggioErrore = "Modifica non riuscita";
                return View();
            }
        }

        [Authorize(Users = "Admin")]
        public ActionResult Delete(int id)
        {
            DB.deleteScarpa(id);
            TempData["Eliminazione"] = "Il prodotto è stato eliminato con successo.";
            return RedirectToAction("Admin");
        }

    }
}