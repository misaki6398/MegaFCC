using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using MegaTFLT.Models.MegaEcm.Models;

namespace MegaTFLT.Utilitys
{
    public abstract class BaseMessagePaser
    {
        public TfMessageModel TfMessageModel { get; protected set; }
        public Dictionary<string, List<ScreeningInputTagModel>> mxMessages { get; protected set; }

        public virtual bool ReadFromFile(string filePath)
        {
            bool isSuccess = false;
            Console.WriteLine("-------------------------");
            Console.WriteLine(value: $"ReadFromFile:{filePath}");
            Console.WriteLine("-------------------------");
            try
            {
                string mxText = File.ReadAllText(filePath);
                //Console.WriteLine($"{mxText}");
                isSuccess = this.ReadFromText(mxText);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("DirectoryNotFoundException");
                Console.WriteLine(ex.Message, ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.ToString());
            }
            finally
            {

            }
            return isSuccess;
        }
        public abstract bool ReadFromText(string mxText);
    }
}