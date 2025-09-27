BankingApp

A simple console-based banking system written in C#.
This project demonstrates object-oriented programming, persistence with JSON, and basic error handling in a clean and extensible way.

⸻

🚀 Features
	•	Create accounts with auto-generated account numbers (00001, 00002, etc.)
	•	Deposit and withdraw money with validation
	•	Transfer funds between accounts
	•	View all accounts in a table format
	•	View transaction history per account
	•	Clear all accounts (reset system)
	•	Data persistence using JSON (accounts and transactions are saved locally and reloaded on startup)
	•	Input validation and colored console output for a better user experience

⸻

🏗️ Project Structure
	•	Account.cs
Represents a bank account. Handles balance operations and transaction logging.
	•	Transaction.cs
Represents individual transactions (Deposit, Withdraw, Transfer).
	•	Bank.cs
Manages accounts, persistence, and high-level banking operations.
	•	Program.cs
Handles user interaction (console menu, input validation, colors, etc.).

⸻

📋 Menu Options
	1.	Create Account
	2.	View All Accounts
	3.	View Transactions
	4.	Deposit
	5.	Withdraw
	6.	Transfer
	7.	Clear All Accounts
	8.	Exit

⸻

💾 Data Persistence
	•	Accounts are saved in a JSON file:
	•	Windows: C:\Users\<YourName>\AppData\Roaming\BankingApp\accounts.json
	•	macOS: /Users/<YourName>/Library/Application Support/BankingApp/accounts.json
	•	Linux: /home/<YourName>/.config/BankingApp/accounts.json

The file is automatically created, updated, and loaded.

▶️ How to Run
	1.	Clone this repository or copy the project files.
	2.	Open the solution in JetBrains Rider or Visual Studio.
	3.	Build and run the project:

  dotnet run

  4.	Use the numbered menu to interact with the banking system.
