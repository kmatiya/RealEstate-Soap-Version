using System;
using System.Security.Cryptography;
using System.Text;
using TipezeNyumbaService.Models;

namespace Generic_Repository_and_Unit_of_Work.Models
{
    public class HashPassword
    {
        private string saltValue;
        public string Salt
        {
            set
            {

            }
            get
            {
                return saltValue;
            }
        }

        public HashPassword()
        {
            saltValue = this.GetRandomSalt();
        }
        protected String GetRandomSalt()
        {
            var f = new byte[24];
            RNGCryptoServiceProvider gt = new RNGCryptoServiceProvider();
            gt.GetBytes(f);
            return Convert.ToBase64String(f);
        }
        public Boolean IsPasswordCorrect(User me, String passd)
        {
            string passwordSave = GenerateHashedPassword(passd, me.passwordSalt);
            if (passwordSave == me.password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string CreateHashedPassword(string password)
        {
            string fullpassword = password + Salt;
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = Encoding.ASCII.GetBytes(fullpassword);
            byte[] resultBytes = sha.ComputeHash(dataBytes);
            return Convert.ToBase64String(resultBytes);
        }
        private string GenerateHashedPassword(string thepassword, string thesalt)
        {
            string fullpassword = thepassword + thesalt;
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = Encoding.ASCII.GetBytes(fullpassword);
            byte[] resultBytes = sha.ComputeHash(dataBytes);
            return Convert.ToBase64String(resultBytes);
        }
    }
}
