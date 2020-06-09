using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tireFitting.Models;

namespace tireFitting.Controllers
{
    public class CustomerController : Controller
    {
        TireFittingContext db;

        public CustomerController(TireFittingContext context)
        {
            db = context;
        }

        public static People Customer;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(People people)
        {
            if(people.Name is null )
                return RedirectToAction("Index");
            if(people.Password is null)
                return RedirectToAction("Index");

            if (people.Name.Length > 0 && people.Password.Length > 0)
            {
                var pls = db.Peoples.Where(p => p.Name == people.Name && p.Password == people.Password).ToList();
                if (pls == null || pls.Count == 0)
                {
                    ViewBag.Response = "Не найден ни один пользователь с данным Логином/паролем. Зарегистрируйтесь!";
                    return RedirectToAction("AddPeople");
                }
                else if (pls.Count > 1)
                {
                    ViewBag.Response = "Для пользователя " + people.Name + "найдено несколько учетных записей. Обратитесь к администратору.";
                    return RedirectToAction("Index");
                }
                else if (pls.Count == 1 && pls[0].Name != "Администратор")
                {
                    ViewBag.Response = people.Name + "Добро пожаловать в личный кабинет";
                    Customer = new People { PeopleId = pls[0].PeopleId, Name = pls[0].Name, Password = pls[0].Password, Phone = pls[0].Phone };
                    return RedirectToAction("MyOrders");
                }
            }  
            return RedirectToAction("Index");
        }

        public IActionResult MyOrders()
        {
            if(Customer==null)
            {
                Customer = new People
                {
                    PeopleId = -1,
                    Name = "НЕОПРЕДЕЛЕН",
                    Phone = "",
                    Password = "notfind"
                };
            }
            ViewBag.ID = Customer.PeopleId;
            ViewBag.Name = Customer.Name;
            return View(db.Orders.Where(o => o.PeopleId == Customer.PeopleId).ToList());
        }

        [HttpGet]
        public IActionResult AddPeople()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPeople(People people)
        {
            if(people.Name is null || people.Password is null)
                return RedirectToAction("Index");

            db.Peoples.Add(people);
            // сохраняем в бд все изменения
            db.SaveChanges();
            Customer = people;
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult AddOrder()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            if (order.Comment is null )
                return RedirectToAction("MyOrders");
            if(Customer is null)
                return RedirectToAction("MyOrders");

            order.DateCompletion = DateTime.Now;
            order.IsReady = false;
            order.PeopleId = Customer.PeopleId;

            db.Orders.Add(order);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("MyOrders");
        }


        [HttpGet]
        public IActionResult DeleteOrder(int? id)
        {
            if (id > 0)
            {
                var orders = db.Orders.Where(o => o.OrderId == id).ToList();
                foreach (var o in orders)
                {
                    db.Orders.Attach(o);
                    db.Orders.Remove(o);
                }
                db.SaveChanges();
            }
            return RedirectToAction("MyOrders");
        }

        [HttpGet]
        public IActionResult UpdateOrder(int? id)
        {
            if (id > 0)
            {
                var wUpdate = db.Orders.Find(id);
                wUpdate.IsReady = true;
                db.SaveChanges();
            }
            return RedirectToAction("MyOrders");
        }
    }
}