

            using System;
            using System.Collections.Generic;



public class Account
        {
            public string Name { get; set; }
            public double Balance { get; set; }

            public Account(string name = "Unnamed Account", double balance = 0.0)
            {
                this.Name = name;
                this.Balance = balance;
            }

            public virtual bool Deposit(double amount)
            {
                if (amount <= 0)
                    return false;
                else
                {
                    Balance += amount;
                    return true;
                }
            }

            public virtual bool Withdraw(double amount)
            {
                if (amount > 0 && Balance - amount >= 0)
                {
                    Balance -= amount;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public override string ToString()
            {
                return $"[Account: {Name}, Balance: {Balance:F2}]";
            }
        }


        public class SavingsAccount : Account
        {
            public double InterestRate { get; set; }

            public SavingsAccount(
                string name = "Unnamed Savings Account",
                double balance = 0.0,
                double interestRate = 0.0)
                : base(name, balance)
            {
                InterestRate = interestRate;
            }
            public override bool Deposit(double amount)
            {
                if (amount <= 0)
                    return false;

                double interest = amount * InterestRate / 100;
                double totalAmount = amount + interest;

                return base.Deposit(totalAmount);
         }

            public override string ToString()
            {
                return $"[Savings: {Name}, Balance: {Balance:F2}, Interest: {InterestRate}%]";
            }
        }


        public class CheckingAccount : Account
        {
            private const double WithdrawalFee = 1.50;

            public CheckingAccount(
                string name = "Unnamed Checking Account",
                double balance = 0.0)
                : base(name, balance)
            {
            }

            public override bool Withdraw(double amount)
            {
                if (amount <= 0)
                    return false;

                double totalAmount = amount + WithdrawalFee;

                return base.Withdraw(totalAmount);
            }

            public override string ToString()
            {
                return $"[Checking: {Name}, Balance: {Balance:F2}]";
            }
        }


        public class TrustAccount : SavingsAccount
        {

            private int withdrawalCount = 0;
            private int withdrawalYear = DateTime.Now.Year;

            public TrustAccount(
                string name = "Unnamed Trust Account",
                double balance = 0.0,
                double interestRate = 0.0)
                : base(name, balance, interestRate)
            {
            }

            public override bool Deposit(double amount)
            {
                if (amount <= 0)
                    return false;


                bool success = base.Deposit(amount);

                if (success && amount >= 5000)
                {
                    Balance += 50;
                }

                return success;
            }


            public override bool Withdraw(double amount)
            {

                if (DateTime.Now.Year != withdrawalYear)
                {
                    withdrawalCount = 0;
                    withdrawalYear = DateTime.Now.Year;
                }


                if (withdrawalCount >= 3)
                    return false;

                if (amount <= 0)
                    return false;

                if (amount >= Balance * 0.20)
                    return false;

                bool success = base.Withdraw(amount);

                if (success)
                {
                    withdrawalCount++;
                }

                return success;
            }

            public override string ToString()
            {
                return $"[Trust: {Name}, Balance: {Balance:F2}, Interest: {InterestRate}%, Withdrawals: {withdrawalCount}/3]";
            }
        }

        public static class AccountUtil
        {

            public static void Deposit(List<Account> accounts, double amount)
            {
                Console.WriteLine("\n=== Depositing to Accounts ===");

                foreach (Account acc in accounts)
                {
                    if (acc.Deposit(amount))
                    {
                        Console.WriteLine($"Deposited {amount} to {acc}");
                    }
                    else
                    {
                        Console.WriteLine($"Failed Deposit of {amount} to {acc}");
                    }
                }
            }

            public static void Withdraw(List<Account> accounts, double amount)
            {
                Console.WriteLine("\n=== Withdrawing from Accounts ===");

                foreach (Account acc in accounts)
                {
                    if (acc.Withdraw(amount))
                    {
                        Console.WriteLine($"Withdrew {amount} from {acc}");
                    }
                    else
                    {
                        Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
                    }
                }
            }
        }


        public class Program
        {
            public static void Main(string[] args)
            {


                var accounts = new List<Account>();

                accounts.Add(new Account());
                accounts.Add(new Account("Larry"));
                accounts.Add(new Account("Moe", 2000));
                accounts.Add(new Account("Curly", 5000));

                AccountUtil.Deposit(accounts, 1000);
                AccountUtil.Withdraw(accounts, 2000);

                var savAccounts = new List<Account>();

                savAccounts.Add(new SavingsAccount());
                savAccounts.Add(new SavingsAccount("Superman"));
                savAccounts.Add(new SavingsAccount("Batman", 2000));
                savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

                AccountUtil.Deposit(savAccounts, 1000);
                AccountUtil.Withdraw(savAccounts, 2000);



                var checAccounts = new List<Account>();

                checAccounts.Add(new CheckingAccount());
                checAccounts.Add(new CheckingAccount("Larry2"));
                checAccounts.Add(new CheckingAccount("Moe2", 2000));
                checAccounts.Add(new CheckingAccount("Curly2", 5000));

                AccountUtil.Deposit(checAccounts, 1000);
                AccountUtil.Withdraw(checAccounts, 2000);
                AccountUtil.Withdraw(checAccounts, 2000);


                var trustAccounts = new List<Account>();

                trustAccounts.Add(new TrustAccount());
                trustAccounts.Add(new TrustAccount("Superman2"));
                trustAccounts.Add(new TrustAccount("Batman2", 2000));
                trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

                AccountUtil.Deposit(trustAccounts, 1000);
                AccountUtil.Deposit(trustAccounts, 6000);
                AccountUtil.Withdraw(trustAccounts, 2000);
                AccountUtil.Withdraw(trustAccounts, 3000);
                AccountUtil.Withdraw(trustAccounts, 500);

                Console.WriteLine("\n== Program Finished ==");
            }
        }
