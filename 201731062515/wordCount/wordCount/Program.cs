using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace wordCount
{
    public class Program
    {
        static void Main(string[] args)
        {
            if(args.Length==0)
            {
                Console.WriteLine("请加入参数，eg:wordCount.exe input.txt");
                return;
            }


            string text;
            if(OpenFile(args[0],out text))
            {
                Analyze(text);
            }
            else
            {
                Console.WriteLine("打开文件失败！");
            }
        }



        public static void Analyze(string text)
        {
            var words = CountWord(text);
            var list = words.ToList();
            list.Sort((x, y) =>
            {
                if (x.Value == y.Value)
                    return x.Key.CompareTo(y.Key);
                return -x.Value.CompareTo(y.Value);
            });

            Console.WriteLine("characters: {0}", CountChar(text));
            Console.WriteLine("words: {0}", words.Count);
            Console.WriteLine("lines: {0}", CountLine(text));
            for (int i = 0; i < 10; i++)
            {
                if(list.Count>=i+1)
                    Console.WriteLine("{0}: {1}", list[i].Key,list[i].Value);
            }

        }




        public static Dictionary<string,int> CountWord(string text)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            string starter = "";
            foreach (var item in text)
            {
                if (char.IsNumber(item))
                {
                    if (starter.Length >= 4)
                    {
                        starter += item;
                        continue;
                    }

                }
                else if (char.IsLetter(item))
                {
                    var letter = char.ToLower(item);
                    starter += letter;
                    continue;
                }
                else if (starter.Length == 0)
                    continue;

                if (dict.ContainsKey(starter))
                    dict[starter]++;
                else
                    dict.Add(starter, 1);
                starter = "";
            }

            return dict;
        }


        public static int CountChar(string text)
        {
            int counter = 0;
            foreach (var item in text)
            {
                if (item < 128 && item >= 0)
                    counter++;
            }
            return counter;
        }

        public static int CountLine(string text)
        {
            int counter = 0;
            bool skip = true;
            foreach (var item in text)
            {
                if (item == '\n')
                {
                    if (!skip)
                        counter++;
                    skip = true;
                }
                else
                    skip = false;
                    
            }
            return counter;
        }




        public static bool OpenFile(string path, out string text)
        {
            try
            {
                text = System.IO.File.ReadAllText(path);
                return true;
            }
            catch (Exception e)
            {
                text = "";
                return false;
            }
        }






    }
}