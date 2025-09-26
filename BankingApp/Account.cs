namespace BankingApp;

public class Account
{
   public int AccountNumber { get; set; }
   public string Bank { get; set; }
   public decimal Balance { get; set; }
   public string Owner { get; set; }

   public Account(int accountNumber, decimal initBalance, string owner)
   {
      AccountNumber = accountNumber;
      Balance = initBalance;
      Owner = owner;
   }
   
   public void Deposit(decimal amount) => Balance += amount;
   public void  Withdraw(decimal amount)
   {
      if (amount > Balance)
         Console.WriteLine("Insufficient funds");
      else
         Balance -= amount;
   }
}