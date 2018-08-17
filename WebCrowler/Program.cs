using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebCrowler
{
    class Program
    {
        static void Main(string[] args)
        {
            
                var sites = File.ReadAllLines("hosts");
                foreach (var site in sites)
                {

                    Console.WriteLine($"start {site}");
                    CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                    CancellationToken token = cancelTokenSource.Token;
                    Console.WriteLine("enter S to stop processing");
                    new Crowler(site).StartAsync(token);
                    Console.WriteLine("finish");
                    Console.WriteLine("press 'enter' to move next site or exit");
                    string s = Console.ReadLine();
                    if (s == "S")
                        cancelTokenSource.Cancel();
                    

                }
            
        }

    }

    
}
