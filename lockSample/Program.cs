using System;
using System.Threading;

namespace lockSample {
    class Account {
        private static readonly object m_mutex = new object();

        private float m_balance;
        public Account(int banlance) { 
            m_balance = banlance;
        }

        /// <summary>
        /// 取钱
        /// </summary>
        /// <param name="num"></param>
        public void Withdraw() {
            lock (m_mutex) { 
                m_balance -= 10;
                Console.WriteLine("-10. banlance:" + m_balance);
            }
        }

        /// <summary>
        /// 存钱
        /// </summary>
        /// <param name="num"></param>
        public void Deposit() {
            lock (m_mutex) { 
                m_balance += 10;
                Console.WriteLine("+10. banlance:" + m_balance);
            }            
        }

        public void ShowBanlance() {
            Console.WriteLine("banlance:" + m_balance);
        }
    }

    class Program {
        static void Main(string[] args) {
            Account account = new Account(100);
            Thread[] threads = new Thread[10];
            for (int i = 0; i <= 9; i++) {
                if (i % 2 == 0) { 
                    threads[i] = new Thread(new ThreadStart(account.Withdraw));
                } else { 
                    threads[i] = new Thread(new ThreadStart(account.Deposit));
                }
            }
            for (int i = 0; i <= 9; i++) { 
                threads[i].Start();
            }

            account.ShowBanlance();
            Console.Read();
        }
    }
}
