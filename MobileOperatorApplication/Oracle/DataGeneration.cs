﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Oracle
{
    public static class DataGeneration
    {
        public static void GeneratePosts(OracleProvider oracleProvider)
        {
            oracleProvider.InsertPost("Senior manager", "First");
            oracleProvider.InsertPost("Cashier", "Second");
            oracleProvider.InsertPost("Manager", "Third");
        }

        private static string GetRandomPassportNumber(Random rand)
        {
            string alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string nums = "0123456789";

            string result = "";
            result += alph[rand.Next(0, alph.Length)];
            result += alph[rand.Next(0, alph.Length)];
            result += nums[rand.Next(0, nums.Length)];
            result += nums[rand.Next(0, nums.Length)];
            result += nums[rand.Next(0, nums.Length)];
            result += nums[rand.Next(0, nums.Length)];
            result += nums[rand.Next(0, nums.Length)];
            result += nums[rand.Next(0, nums.Length)];
            result += nums[rand.Next(0, nums.Length)];

            return result;
        }

        public static void GenerateClients(OracleProvider oracleProvider, int count)
        {
            string firstnames_path = @"E:\Study\BD_Course_Work\DataForGeneration\firstnames.txt";
            string lastnames_path = @"E:\Study\BD_Course_Work\DataForGeneration\lastnames.txt";
            string firstnames_string = "";
            string lastnames_string = "";
            using (StreamReader sr = new StreamReader(firstnames_path))
            {
                firstnames_string = sr.ReadToEnd();
            }
            using (StreamReader sr = new StreamReader(lastnames_path))
            {
                lastnames_string = sr.ReadToEnd();
            }
            firstnames_string = firstnames_string.Replace("\r\n", ";");
            lastnames_string = lastnames_string.Replace("\n", ";");
            List<string> firstnames_list = new List<string>(firstnames_string.Split(';'));
            List<string> lastnames_list = new List<string>(lastnames_string.Split(';'));

            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                string firstname = firstnames_list[rand.Next(0, firstnames_list.Count)];
                string lastname = lastnames_list[rand.Next(0, lastnames_list.Count)];
                oracleProvider.InsertClient(firstname + " " + lastname, GetRandomPassportNumber(rand));
            }
        }
    }
}
