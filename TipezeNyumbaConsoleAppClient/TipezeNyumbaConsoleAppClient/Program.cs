using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipezeNyumbaConsoleAppClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TipezeNyumbaClientService.TipezeNyumbaServiceClient client = new TipezeNyumbaClientService.TipezeNyumbaServiceClient("BasicHttpBinding_ITipezeNyumbaService");
            //string message = client.getMessage();
            /*TipezeNyumbaClientService.ApplicationUser getUserProfile = client.GetUserProfile(5);
            Console.WriteLine("User Profile\n");
            Console.WriteLine("First Name: " + getUserProfile.firstName+"\n");
            Console.WriteLine("Last Name: " + getUserProfile.lastName + "\n");
            Console.WriteLine("Email: " + getUserProfile.email + "\n");
            Console.ReadLine();*/
            Console.WriteLine("Welcome to Tipeze Nyumba Service, please enter your user credentials to Login\n");
            /*Console.Write("Phone Number: ");
            string userPhoneNumber = Console.ReadLine();
            Console.Write("\nPassword: ");

            string userPassword = Console.ReadLine();
            string getTokenFromService = client.GetAuntheticationToken(userPhoneNumber, userPassword);
            if (getTokenFromService.StartsWith("AUTH:"))
            {
                Console.WriteLine("Authentication Token generated is " + getTokenFromService);
                TipezeNyumbaClientService.AddOrUpdateUser UserProfile = client.GetUserProfile(6, getTokenFromService);
                Console.WriteLine("Your requested the following User Profile");
                Console.WriteLine("First Name: " + UserProfile.firstName);
                Console.WriteLine("Last Name:  " + UserProfile.lastName);
                Console.WriteLine("Email:      " + UserProfile.email);
            }
            else
            {
                Console.WriteLine("Failed to get token because: " + getTokenFromService);
            }
            */
            DateTime date = new DateTime(2019, 1, 1);
            var Houses = client.GetAllHousesByDateHouseAvailable(date);
            foreach(var house in Houses)
            {
                Console.WriteLine("Price: " + house.price);
                Console.WriteLine("Description: " + house.description);
                Console.WriteLine("Self-contained: " + house.selfContained);
                Console.WriteLine("Master Bedroom Ensuit: " + house.masterBedroomEnsuite);
                Console.WriteLine("Number of Bedrooms: " + house.bedrooms);
                Console.WriteLine("Date house will be available: " + house.dateHouseWillBeAvailable.ToShortDateString());
                Console.WriteLine("Fence Type: " + house.fenceType);
                Console.WriteLine("Number of garages: " + house.numberOfGarages);
                Console.WriteLine("Date Uploaded: " + house.dateUploaded.ToShortDateString());
                Console.WriteLine("Payment Mode: " + house.modeOfPayment);
            }
            //Console.WriteLine("Authentication of token from service is " + getTokenFromService);
            Console.ReadLine();

            /*TipezeNyumbaClientService.ApplicationUser newUser = new TipezeNyumbaClientService.ApplicationUser();
            newUser.firstName = "Tiwonge";
            newUser.lastName = "Lwara";
            newUser.email = "tlwara@nitel.mw";
            newUser.dateTimeCreated = DateTime.Now;
            newUser.userType = 1;
            newUser.subscriptionType = 1;
            newUser.password = "1234";
            newUser.accountState = 1;
            bool CheckUserAdded = client.AddANewUser(newUser);
            if(CheckUserAdded == true)
            {
                Console.WriteLine("User successfully added\n");                
            }
            else
            {
                Console.WriteLine("User not added\n");
            }
            Console.WriteLine(message);
            Console.ReadLine();*/
        }
    }
}
