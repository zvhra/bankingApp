using System.IO;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp;

public class Bank
{
    private List<Account> accounts = new List<Account>();
    private readonly string _dataFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "BankingApp",
        "accounts.json");

    public Bank()
    {
        var dir = Path.GetDirectoryName(_dataFilePath);
        if (!string.IsNullOrEmpty(dir))
        {
            Directory.CreateDirectory(dir);
        }
        // Debug: show data file path before loading
        Console.WriteLine($"[DEBUG] Data file path: {_dataFilePath}");
        LoadFromDisk();
        // Debug: show account count after loading
        Console.WriteLine($"[DEBUG] Accounts loaded: {accounts.Count}");
    }

    public void AddAccount(string owner, decimal initBalance = 0)
    {
        var account = new Account(initBalance, owner);
        accounts.Add(account);
        SaveToDisk();
        Console.WriteLine($"Account for {owner} with number {account.AccountNumber} has been added");
    }

    private Account? FindAccount(string accountNumber)
    {
        var key = NormalizeAccountNumber(accountNumber);
        return accounts.FirstOrDefault(a => a.AccountNumber == key);
    }

    private Account? RequireAccount(string accountNumber)
    {
        var account = FindAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found");
        }
        return account;
    }

    public void Deposit(string accountNumber, decimal amount)
    {
        var account = RequireAccount(accountNumber);
        if (account == null) return;

        account.Deposit(amount);
        SaveToDisk();
        Console.WriteLine($"Deposited {amount} to {account.AccountNumber}");
    }

    public void Withdraw(string accountNumber, decimal amount)
    {
        var account = RequireAccount(accountNumber);
        if (account == null) return;

        account.Withdraw(amount);
        SaveToDisk();
    }

    public void Transfer(string fromAccount, string toAccount, decimal amount)
    {
        fromAccount = NormalizeAccountNumber(fromAccount);
        toAccount = NormalizeAccountNumber(toAccount);

        if (string.IsNullOrEmpty(fromAccount) || string.IsNullOrEmpty(toAccount))
        {
            Console.WriteLine("Account not found");
            return;
        }

        if (amount <= 0)
        {
            Console.WriteLine("Amount must be positive");
            return;
        }

        var source = FindAccount(fromAccount);
        var target = FindAccount(toAccount);

        if (source == null || target == null)
        {
            Console.WriteLine("Account not found");
            return;
        }

        if (ReferenceEquals(source, target))
        {
            Console.WriteLine("Cannot transfer to the same account");
            return;
        }

        if (source.Balance < amount)
        {
            Console.WriteLine("Insufficient funds");
            return;
        }

        Console.WriteLine($"Before: From {source.AccountNumber} balance={source.Balance}, To {target.AccountNumber} balance={target.Balance}");

        source.Withdraw(amount);
        target.Deposit(amount);
        SaveToDisk();

        Console.WriteLine($"Transferred {amount} from {fromAccount} to {toAccount}");
        Console.WriteLine($"After:  From {source.AccountNumber} balance={source.Balance}, To {target.AccountNumber} balance={target.Balance}");
    }

    public void ShowAllAccounts()
    {
        if (!accounts.Any())
        {
            Console.WriteLine("No accounts available.");
            return;
        }

        Console.WriteLine("\n--- All Accounts ---");
        Console.WriteLine("{0,-15} {1,-20} {2,10}", "Account Number", "Owner", "Balance");

        foreach (var account in accounts)
        {
            Console.WriteLine("{0,-15} {1,-20} {2,10:C}",
                account.AccountNumber, account.Owner, account.Balance);
        }
    }

    public void ShowTransactions(string accountNumber)
    {
        var account = RequireAccount(accountNumber);
        if (account == null) return;

        Console.WriteLine($"\n--- Transactions for {account.AccountNumber} ({account.Owner}) ---");
        if (!account.Transactions.Any())
        {
            Console.WriteLine("No transactions yet.");
            return;
        }

        foreach (var tx in account.Transactions)
        {
            Console.WriteLine(tx.ToString());
        }
    }

    public Account? GetAccount(string accountNumber)
    {
        return RequireAccount(accountNumber);
    }

    public void ClearAllAccounts()
    {
        accounts.Clear();
        SaveToDisk();
    }

    private void SaveToDisk()
    {
        try
        {
            var json = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_dataFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save data: {ex.Message}");
        }
    }

    private void LoadFromDisk()
    {
        try
        {
            if (File.Exists(_dataFilePath))
            {
                var json = File.ReadAllText(_dataFilePath);
                var loaded = JsonSerializer.Deserialize<List<Account>>(json);
                if (loaded != null)
                {
                    accounts = loaded;
                    Account.SeedNextFromExisting(accounts);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load data: {ex.Message}");
        }
    }

    private string NormalizeAccountNumber(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var t = input.Trim();
        if (int.TryParse(t, out var n)) return n.ToString("D5");
        return t;
    }
}