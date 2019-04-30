using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace mapper
{
    public class Mapper
    {
        string path = "Big Black.data";
        List <Queue> QList;
        int ThreadNum = 8;
        
        public Mapper(){
            for(int i=0;i<ThreadNum;i++){
                QList.Add(new Queue());
            }
        }
        public void DoWork()
        {
                using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        GetEmptiestQueue().Enqueue(line);
                    }
                    foreach (var q in QList) {
                        Console.WriteLine(q.Count);
                    }
                }
        }

        Queue GetEmptiestQueue()
        {
            Queue min = QList.FirstOrDefault();
            foreach (var element in QList)
            {
                if (min.Count < element.Count){
                    min = element;
                }
            }
            return min;
        }
        
    }
}