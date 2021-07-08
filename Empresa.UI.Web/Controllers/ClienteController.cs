using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Empresa.Db;
using Empresa.Models;

namespace Empresa.UI.Web.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            var db = new ClienteDb();
            var lista = db.Listar();
            return View(lista);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            var db = new ClienteDb();
            var cliente = db.ObterPorId(id);
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            var db = new ClienteDb();
            int Result = db.ValidaClienteUnico(cliente);

            if(Result < 0)
            {
                try
                {
                    db.incluir(cliente);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(cliente);
                }
                
            }
            else
            {
                switch (Result)
                {
                    case 2:
                        ModelState.AddModelError("", "O Id do usuário já está em uso");
                        break;
                    case 3:
                        ModelState.AddModelError("", "Nome do usuário já está em uso para outro usuário cadastrado");
                        break;
                    case 4:
                        ModelState.AddModelError("", "E-mail do usuário já está em uso para outro cadastro");
                        break;
                    case 5:
                        ModelState.AddModelError("", "Telefone informado já está em uso para outro usuário");
                        break;
                    default:
                        ModelState.AddModelError("", "Algo de errado ocorreu durante a validação de cadastro único");
                        break;
                }
                return View(cliente);
            }
            
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            var db = new ClienteDb();
            var cliente = db.ObterPorId(id);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cliente cliente)
        {
            try
            {
                var db = new ClienteDb();
                db.alterar(cliente);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(cliente);
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            var db = new ClienteDb();
            var cliente = db.ObterPorId(id);
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Cliente cliente)
        {
            try
            {
                var db = new ClienteDb();
                db.excluir(cliente);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(cliente);
            }
        }
    }
}
