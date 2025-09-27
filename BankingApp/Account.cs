using System.Collections.Generic;

namespace BankingApp;

public class Transaction
{
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }

    public override string ToString()
    {
        return $"[{Date}] {Type}: {(Type == "Deposit" ? "+" : "-")}{Amount} (Balance: {Balance})";
    }
}

public class Account
{
   public string AccountNumber { get; set; }
   private static int _nextAccountNumber = 1;
   public string Bank { get; set; }
   public decimal Balance { get; set; }
   public string Owner { get; set; }

   public Account() { }
   public Account(decimal initBalance, string owner)
   {
      AccountNumber = _nextAccountNumber.ToString("D5");
      _nextAccountNumber++;
      Console.WriteLine($"[DEBUG] Created account {AccountNumber} for {owner} with balance {initBalance}");
      Balance = initBalance;
      Owner = owner;
   }
   
   public void Deposit(decimal amount)
   {
       Balance += amount;
       Transactions.Add(new Transaction
       {
           Date = DateTime.Now,
           Type = "Deposit",
           Amount = amount,
           Balance = Balance
       });
   }
   public void Withdraw(decimal amount)
   {
       if (amount > Balance)
       {
           Console.WriteLine("Insufficient funds");
       }
       else
       {
           Balance -= amount;
           Transactions.Add(new Transaction
           {
               Date = DateTime.Now,
               Type = "Withdraw",
               Amount = amount,
               Balance = Balance
           });
       }
   }

   public static void SeedNextFromExisting(IEnumerable<Account> existing)
   {
       int max = 0;
       if (existing != null)
       {
           foreach (var a in existing)
           {
               if (int.TryParse(a.AccountNumber, out var n))
               {
                   if (n > max) max = n;
               }
           }
       }
       _nextAccountNumber = max + 1;
   }
   
   public List<Transaction> Transactions { get; set; } = new List<Transaction>();
   
}