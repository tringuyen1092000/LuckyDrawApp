namespace LuckyDrawApp
{
   public class Employee
   {
      public string Id { get; set; }
      public string Name { get; set; }
      public bool IsAwarded { get; set; }
      public int PrizeAwarded { get; set; }

      public Employee()
      {
         Id = string.Empty;
         Name = string.Empty;
         IsAwarded = false;
         PrizeAwarded = 0;
      }

      public Employee(string id, string name, bool isAwarded = false, int prizeAwarded = 0)
      {
         Id = id;
         Name = name;
         IsAwarded = isAwarded;
         PrizeAwarded = prizeAwarded;
      }
   }
}
