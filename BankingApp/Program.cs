using BankingApp;

class Program
{
    static void Main(string[] args)
    {
        var bank = new Bank();
        bool running = true;
        
        while (running)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Banking App ---");
            Console.ResetColor();
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. View All Accounts");
            Console.WriteLine("3. View Transactions");
            Console.WriteLine("4. Deposit");
            Console.WriteLine("5. Withdraw");
            Console.WriteLine("6. Transfer");
            Console.WriteLine("7. Clear All Accounts");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");
            var selection = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(selection))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please choose an option.");
                Console.ResetColor();
                continue;
            }
            selection = selection.Trim().ToLower();
            if (selection == "exit" || selection == "quit")
            {
                selection = "8"; // map to Exit case
            }

            switch (selection)
            {
                case "1":
                    Console.Write("Enter Owner Name: ");
                    string owner = Console.ReadLine();
                    decimal initBal;
                    while (true)
                    {
                        Console.Write("Enter Initial Balance: ");
                        if (decimal.TryParse(Console.ReadLine(), out initBal) && initBal >= 0)
                            break;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid amount. Please enter a positive number.");
                        Console.ResetColor();
                    }
                    bank.AddAccount(owner, initBal);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Account created successfully.");
                    Console.ResetColor();
                    Pause();
                    break;
                case "2":
                    bank.ShowAllAccounts();
                    Pause();
                    break;
                case "3":
                    Console.Write("Enter Account Number: ");
                    string txAcc = Console.ReadLine();
                    bank.ShowTransactions(txAcc ?? string.Empty);
                    Pause();
                    break;
                case "4":
                    Console.Write("Enter Account Number: ");
                    string depAcc = Console.ReadLine();
                    decimal depAmt;
                    while (true)
                    {
                        Console.Write("Enter Amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out depAmt) && depAmt > 0)
                            break;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid amount. Please enter a number greater than 0.");
                        Console.ResetColor();
                    }
                    bank.Deposit(depAcc, depAmt);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Deposit successful.");
                    Console.ResetColor();
                    Pause();
                    break;
                case "5":
                    Console.Write("Enter Account Number: ");
                    string withAcc = Console.ReadLine();
                    decimal withAmt;
                    while (true)
                    {
                        Console.Write("Enter Amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out withAmt) && withAmt > 0)
                            break;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid amount. Please enter a number greater than 0.");
                        Console.ResetColor();
                    }
                    bank.Withdraw(withAcc, withAmt);
                    var accountAfterWithdraw = bank.GetAccount(withAcc ?? string.Empty);
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (accountAfterWithdraw != null)
                    {
                        Console.WriteLine($"Withdrawal successful. New balance: {accountAfterWithdraw.Balance}");
                    }
                    else
                    {
                        Console.WriteLine("Withdrawal attempted, but account not found.");
                    }
                    Console.ResetColor();
                    Pause();
                    break;
                case "6":
                    Console.Write("Enter Source Account: ");
                    string fromAcc = Console.ReadLine();
                    Console.Write("Enter Target Account: ");
                    string toAcc = Console.ReadLine();
                    decimal transferAmt;
                    while (true)
                    {
                        Console.Write("Enter Amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out transferAmt) && transferAmt > 0)
                            break;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid amount. Please enter a number greater than 0.");
                        Console.ResetColor();
                    }
                    bank.Transfer(fromAcc ?? string.Empty, toAcc ?? string.Empty, transferAmt);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Transfer successful.");
                    Console.ResetColor();
                    Pause();
                    break;
                case "7":
                    Console.Write("Are you sure you want to clear all accounts? (y/n): ");
                    var confirm = Console.ReadLine();
                    if (confirm?.ToLower() == "y")
                    {
                        bank.ClearAllAccounts();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("All accounts have been cleared.");
                        Console.ResetColor();
                    }
                    Pause();
                    break;
                case "8":
                    running =  false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid selection, try again");
                    Console.ResetColor();
                    Pause();
                    break;
            }
        }
    }
    static void Pause()
    {
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey(true);
    }
}