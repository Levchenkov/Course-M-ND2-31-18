using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Validation_CardHolder.Models;

namespace Validation_CardHolder.Controllers
{
    public class CardHolderController : Controller
    {
        private static readonly List<CardHolderViewModel> _cardHoldersList = new List<CardHolderViewModel>();

        // GET: CardHolder
        public ActionResult Index()
        {
            return View(_cardHoldersList);
        }


        // GET: CardHolder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardHolder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CardHolderViewModel cardHolderViewModel)
        {
            if (!ModelState.IsValid)
            { 
                // re-render the view when validation failed.
                return View("Create", cardHolderViewModel);
            }
            _cardHoldersList.Add(cardHolderViewModel);
            return RedirectToAction("Index");
        }
    }
}