/*
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
   */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Configuration;

namespace LinkedInEndorser
{
    class Program
    {
        private static string logFilename = String.Empty;
        private const int WaitTimeShortest = 5000;
        private const int WaitTimeLongest = 15000;

        private static void Log(string msg)
        {
            if (String.IsNullOrWhiteSpace(logFilename))
            {
                logFilename = Path.ChangeExtension(Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName), "log");
            }

            File.AppendAllText(logFilename, msg);
            Console.WriteLine(msg);
        }

        [STAThread]
        static void Main(string[] args)
        {
            string linkedInUserName= ConfigurationManager.AppSettings["LinkedInUserName"];
            if (String.IsNullOrWhiteSpace(linkedInUserName))
            {
                Log("Your LinkedIn username is blank - you need to set this in your app.config");
                Environment.Exit(-1);
            }

            string linkedInPassword = ConfigurationManager.AppSettings["LinkedInPassword"];
            if (String.IsNullOrWhiteSpace(linkedInPassword))
            {
                Log("Your LinkedIn password is blank - you need to set this in your app.config");
                Environment.Exit(-2);
            }

            using (IE browser = new IE(true))
            {
                Log(String.Format("Starting run at {0}", DateTime.Now));
                Random rnd = new Random();
                browser.WaitForComplete();

                browser.GoTo("http://www.linkedin.com/");
                browser.WaitForComplete();
                Thread.Sleep(rnd.Next(WaitTimeShortest, WaitTimeLongest));
                if (browser.TextField("session_key-login").Exists == true)
                {
                    browser.TextField("session_key-login").Value = linkedInUserName;
                    Thread.Sleep(rnd.Next(WaitTimeShortest, WaitTimeLongest));
                    browser.TextField("session_password-login").Value = linkedInPassword;
                    Thread.Sleep(rnd.Next(WaitTimeShortest, WaitTimeLongest));
                    browser.Button("signin").Click();
                    Thread.Sleep(rnd.Next(WaitTimeShortest, WaitTimeLongest));
                }

                // TODO verify that we are logged in here
                
                // navigate to your list of connections
                browser.GoTo("http://www.linkedin.com/connections?trk=hb_tab_connections");
                // TODO verify that we navigated to the list of connections here
                Thread.Sleep(rnd.Next(WaitTimeShortest, WaitTimeLongest));
                List<ListItem> userIDs = browser.ListItems.Filter(Find.ById(new Regex(@"\d{1,10}"))).ToList();
                if (userIDs.Count == 0)
                {
                    Log("No user IDs found. Either you don't have any connections or something changed on LinkedIn.");
                    return;
                }

                // select a random connection to endorse
                int selectedIndex = rnd.Next(userIDs.Count);
                browser.GoTo("http://www.linkedin.com/profile/view?id=" + userIDs[selectedIndex].Id);
                Thread.Sleep(rnd.Next(WaitTimeShortest, WaitTimeLongest));
                // TODO verify that we are on the profile page here
                Button first = browser.Buttons.Filter(Find.ById("endorse-submit")).First();
                if ((first != null) && (first.Enabled == true))
                {
                    // TODO output the user's name by here
                    Log("Endorsing user " + userIDs[selectedIndex].Id);
                    first.Click();
                }

                Thread.Sleep(rnd.Next(WaitTimeShortest, WaitTimeLongest));
            }

        }
    }
}
