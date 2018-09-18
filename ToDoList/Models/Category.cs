using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Category
  {
    private string _name;
    private int _id;
    private List<Item> _items;

    public Category(string categoryName, int id = 0)
    {
      _name = categoryName;
      _id = id;
      _items = new List<Item>{};
    }

    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    public static List<Category> GetAll()
    {
      List<Category> allCategories = new List<Category> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM categories;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int categoryId = rdr.GetInt32(0);
              string categoryDescription = rdr.GetString(1);
              Category newCategory = new Category(categoryDescription, categoryId);
              allCategories.Add(newCategory);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCategories;
    }

    public static Category Find(int id)
    {
      MySqlConnection conn = DB.Connection();
           conn.Open();

           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM `categories` WHERE id = @thisId;";

           MySqlParameter thisId = new MySqlParameter();
           thisId.ParameterName = "@thisId";
           thisId.Value = id;
           cmd.Parameters.Add(thisId);

           var rdr = cmd.ExecuteReader() as MySqlDataReader;

           int categoryId = 0;
           string categoryDescription = "";

           while (rdr.Read())
           {
               categoryId = rdr.GetInt32(0);
               categoryDescription = rdr.GetString(1);
           }

           Category foundCategory= new Category(categoryDescription, categoryId);  // This line is new!

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

           return foundCategory;  // This line is new!
    }

    public List<Item> GetItems()
    {
      return _items;
    }
    public void AddItem(Item item)
    {
      _items.Add(item);
    }
  }
}
