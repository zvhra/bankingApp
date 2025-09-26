using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp;

public class Bank
{
    private List<Account> accounts = new List<Account>();

    public void AddAccount(int accountNumber, string owner, decimal initBalance = 0)
    {
        if (accounts.Any(a => a.AccountNumber == accountNumber))
        {
            Console.WriteLine("Account already exists");
            return;
        }

        var account = new Account(accountNumber, initBalance, owner);
        accounts.Add(account);
        Console.WriteLine($"Account for {owner} with number {accountNumber} has been added");
    }

    private Account? FindAccount(int accountNumber)
    {
        return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
    }

    public void Deposit(int accountNumber, decimal amount)
    {
        var account = FindAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found");
            return;
        }

        account.Deposit(amount);
        Console.WriteLine($"Deposited {amount} to {account.AccountNumber}");
    }

    public void Withdraw(int accountNumber, decimal amount)
    {
        var account = FindAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found");
            return;
        }

        account.Withdraw(amount);
    }

    public void Transfer(int fromAccount, int toAccount, decimal amount)
    {
        var source = FindAccount(fromAccount);
        var target = FindAccount(toAccount);

        if (source == null || target == null)
        {
            Console.WriteLine("Account not found");
            return;
        }

        if (source.Balance < amount)
        {
            Console.WriteLine("Insufficient funds");
            ;
        }

        source.Withdraw(amount);
        target.Deposit(amount);
        Console.WriteLine($"Deposited {amount} from {fromAccount} to {target.AccountNumber}");
    }
    
    public void ShowAccounts(int accountNumber)
    {
        var account = FindAccount (accountNumber);
        if (account == null)
            {
            Console.WriteLine("Account not found");
            return;
            }
        Console.WriteLine($"Account: {account.AccountNumber}");
        Console.WriteLine($"Owner: {account.Owner}");
        Console.WriteLine($"Balance: {account.Balance}");
    }
}