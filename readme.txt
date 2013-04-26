* Welcome
Thanks for downloading LinkedIn Endorser.

Visit https://bitbucket.org/JustinLloyd/linkedin-endorser/overview for more information
and to download the latest version.

* License
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
    
    
* About
A very small .NET application that endorses random connections in your LinkedIn
network.

I quickly wrote this one afternoon after realising that I didn't want to play
whack-a-mole on endorsements every time I logged in to LinkedIn. The other thing
I realised was that if I were to set this software up to run on an automated schedule
then it would repeatedly give people a gentle reminder that I am in their social
graph. Nothing too overt, but LinkedIn will send a "You've been endorsed by so-and-so!"
whenever you first endorse someone.

As I maintain a tight rein on my LinkedIn social graph of generally only people
I have directly worked with or trust, endorsing someone isn't really an issue.
That said, I have thought of a way to "tag" people in LinkedIn that I should endorse
and then have this software automatically do that.

Was it a bad idea? Oh hell yes! :)

The random delays in the code are to make the navigation feel a little more "human."

    
* Pre-requisites
.NET 4.0 - but it will probably work fine on 2.0 as well if you want to try it
WatiN

* Using LinkedIn Endorser
1. Open up the Visual Studio 2010 solution.
2. Use Nuget to install Watin. You will probably want the latest stable version.
3. Edit the two constants for user name and password in program.cs
4. Compile and run to verify that it works and endorses someone in your network.
5. Configure Task Scheduler to run it once every 12 hours and your machine is idle.

* Support
Absolutely none provided.

* Possible Tweaks
1. Use WatiN to hide the Internet Explorer window
2. Use a different method to navigate LinkedIn, e.g. WebRequest
3. Only endorse people in your network with a specific tag
4. Configurable username and password passed on commandline
5. Logging
6. Exception handling and robustness

Copyright Justin Lloyd 2012
