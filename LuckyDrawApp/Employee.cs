namespace LuckyDrawApp
{
   public class Employee
   {
      public string Id { get; set; }
      public string Name { get; set; }

      public Employee(string id, string name)
      {
         Id = id;
         Name = name;
      }

      public Employee()
      {
         Id = string.Empty;
         Name = string.Empty;
      }
   }
}
