using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Cryptography;

namespace BustadirForm.BLL
{
    public class TransactionHandler
    {
        /**
        * calculateMac
        * Calculates the MAC key from a Dictionary<string, string> and a secret key
        * @param params_dict The Dictionary<string, string> object containing all keys and their values for MAC calculation
        * @param K_hexEnc String containing the hex encoded secret key from DIBS Admin
        * @return String containig the hex encoded MAC key calculated
        **/
        public static string CalculateMac(string msg, string K_hexEnc)
        {
            ////Create the message for MAC calculation sorted by the key
            //var keys = params_dict.Keys.ToList();
            //keys.Sort(StringComparer.Ordinal);
            //string msg = "";
            //foreach (var key in keys)
            //{
            //    if (key != keys[0]) msg += "&";
            //    msg += key + "=" + params_dict[key];
            //}

            //Decoding the secret Hex encoded key and getting the bytes for MAC calculation
            var K_bytes = new byte[K_hexEnc.Length / 2];
            for (int i = 0; i < K_bytes.Length; i++)
            {
                K_bytes[i] = byte.Parse(K_hexEnc.Substring(i * 2, 2), NumberStyles.HexNumber);
            }

            //Getting bytes from message
            var encoding = new UTF8Encoding();
            byte[] msg_bytes = encoding.GetBytes(msg);

            //Calculate MAC key
            var hash = new HMACSHA256(K_bytes);
            byte[] mac_bytes = hash.ComputeHash(msg_bytes);
            string mac = BitConverter.ToString(mac_bytes).Replace("-", "").ToLower();

            return mac;
        }
    }
}