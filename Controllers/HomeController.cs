using NewGlossary.Context;
using NewGlossary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewGlossary.Controllers
{
    public class HomeController : Controller
    {
        //Returns the Index.Html and keeps the list sorted at all times
        public ViewResult Index()
        {
            try
            {
                using (var db = new GlossaryModelContainer())
                {
                    var entries = db.Tables.OrderBy(e => e.Term).ToList();
                    db.SaveChanges();
                    return View(entries);
                }
            }
             catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return View();
        }

        //Returns the Create.Html
        public ActionResult Create()
        {
            return View();
        }
                
        [HttpPost]
        //Takes the two input fields and creates a new entry in the database
        public ActionResult Create([Bind(Include = "Term, Definition")]Entry entry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using(var db= new GlossaryModelContainer())
                    {
                        var logData = new Table
                        {
                            Term = entry.Term,
                            Definition = entry.Definition
                        };
                        db.Tables.Add(logData);
                        db.SaveChanges();
                    }
                    
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
            }
            return View("Create");
        }

        //Finds the element and carries the object to the Edit.html
        public ActionResult Edit(Guid id)
        {
            try
            {
                using (var db = new GlossaryModelContainer())
                {
                    var entry = db.Tables.Find(id);
                    return View(entry);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Edit")]
        //Updates the model if it can, else keep on the same page
        public ActionResult EditPost(Guid id)
        {            
          try
            {
                using (var db = new GlossaryModelContainer())
                {
                    var entry = db.Tables.Find(id);
                    if (TryUpdateModel(entry, new string[] { "Term", "Definition" }))
                    {
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            return View();
        }

        //Deletes Entry (Ajax could work too)
        public ActionResult Delete(Guid id)
        {
            try
            {
                using(var db = new GlossaryModelContainer())
                {
                    var entry = db.Tables.Find(id);
                    db.Tables.Remove(entry);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }
    }
}
