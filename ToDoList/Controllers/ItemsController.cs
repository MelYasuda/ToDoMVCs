using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    [HttpGet("/items")] //REQUEST
    public ActionResult Index()
    {
      List<Item> allItems = new List<Item> {};
      return View();
    }

    [HttpGet("/items/new")] //REQUEST
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/items")] //UPDATE/SEND
    public ActionResult Create()
    {
      Item newItem = new Item(Request.Form["new-item"]);
      newItem.Save();
      List<Item> allItems = Item.GetAll();
      return View("Index", allItems);
    }
  }
}
