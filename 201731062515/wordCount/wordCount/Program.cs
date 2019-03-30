using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace wordCount
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path=null;
            string output=null;
            int split = 1;
            int count = 10;

            for (int i = 0; i < args.Length; i += 2)
            {
                if (!(args.Length > i + 1))
                    break;
                if (args[i] == "-i")
                    path = args[i + 1];
                else if (args[i] == "-m")
                {
                    if (!int.TryParse(args[i + 1], out split))
                        split = 1;
                }
                else if (args[i] == "-n")
                {
                    if (!int.TryParse(args[i + 1], out count))
                        count = 10;
                }
                else if (args[i] == "-o")
                {
                    output = args[i + 1];
                }
            }




            if (string.IsNullOrEmpty(path)|| string.IsNullOrEmpty(output))
            {
                Console.WriteLine("请加入输入与输出的参数");
                return;
            }


            string text;
            if(OpenFile(path, out text))
            {
                Analyze(text,split,count,output);
            }
            else
            {
                Console.WriteLine("打开文件失败！");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">用于分析的文本</param>
        /// <param name="split">分割数目</param>
        /// <param name="count">输出单词数量</param>
        public static void Analyze(string text,int split=1,int count=10,string output="output.txt")
        {
            var words = CountWord(text,split);
            var list = words.ToList();
            list.Sort((x, y) =>
            {
                if (x.Value == y.Value)
                    return x.Key.CompareTo(y.Key);
                return -x.Value.CompareTo(y.Value);
            });

            StreamWriter sw = new StreamWriter(output);
            try
            {
                Console.SetOut(sw);

                Console.WriteLine("characters: {0}", CountChar(text));
                Console.WriteLine("words: {0}", words.Count);
                Console.WriteLine("lines: {0}", CountLine(text));
                for (int i = 0; i < count; i++)
                {
                    if (list.Count >= i + 1)
                        Console.WriteLine("{0}: {1}", list[i].Key, list[i].Value);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("输出文件错误！");
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }




        public static Dictionary<string,int> CountWord(string text,int splite=1)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            int nowsplite = 0;
            string starter = "";
            bool isChecked= false;
            foreach (var item in text)
            {
                if (char.IsNumber(item))
                {
                    if (starter.Length >= 4)
                    {
                        starter += item;
                        isChecked = true;
                        continue;
                    }

                }
                else if (char.IsLetter(item))
                {
                    var letter = char.ToLower(item);
                    starter += letter;
                    isChecked = true;
                    continue;
                }
                else if (starter.Length == 0)
                    continue;

                if(isChecked)
                { isChecked = false; nowsplite++; }
                    
                if (nowsplite <= splite)
                {
                    starter += item;
                    continue;
                }
                    
                nowsplite = 0;

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