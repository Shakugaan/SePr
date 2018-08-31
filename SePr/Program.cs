using System;
using System.Xml;

namespace SePr
{
    class Program
    {



        static void Main(string[] args)
        {
            //startup
            int i = 0;
            int temp = 0;
            XmlDocument trans = new XmlDocument();
            XmlDocument part = new XmlDocument();
            part.Load("Piramida.xml");
            XmlNode pir = part.GetElementsByTagName("Piramida").Item(0);
            trans.Load("Przelewy.xml");
            XmlNode tran = trans.GetElementsByTagName("Przelewy").Item(0);

            //Couting nodes, Creating the Tab
            Count(pir, ref i);
            Employee[] List = new Employee[i];

            //Filling the Tab
            InsertVal(pir,tran,ref List,ref temp);

            //Result
            for (int j = 0; j < i; j++)
                List[j].Show();

            Console.ReadLine();
        }

        static void InsertVal(XmlNode Pir,XmlNode Tra,ref Employee[] List,ref int temp)
        {
            if (!Pir.Name.Equals("Piramida"))
            {
                //Level counter
                int level = 1;
                if (temp > 0)
                {
                   for(int i=0;i<temp;i++)
                    {
                        if (List[i].id == Pir.ParentNode.Attributes["id"].Value)
                        {
                            level = List[i].level + 1;
                        }
                        
                    }
                }

                //Filing list with base inf.
                List[temp] = new Employee(Pir.Attributes["id"].Value, level, Pir.ChildNodes.Count);

                //Counting comm. done by id
                int X = 0;
                CountComm(Tra, Pir.Attributes["id"].Value,ref X);
                //X=100;//Test

                //Filling list with comm.
                if (X != 0) 
                {
                    XmlNode temporary = Pir;
                    while (temporary.Attributes["id"].Value != "1") 
                    {
                        string parid = temporary.ParentNode.Attributes["id"].Value; 
                        for (int i = 0; i < temp; i++) 
                        {
                            if (List[i].id == parid)
                            {
                                double A = Math.Pow(2,List[i].level);
                                if (Pir.ParentNode.Attributes["id"].Value == temporary.ParentNode.Attributes["id"].Value)
                                { A = A / 2; }
                                List[i].AddComm(X/A);         
                            }
                        }
                        temporary = temporary.ParentNode;
                    }
                }

               //List[temp].Show();//debugDisplay
               temp++;
            }
            foreach (XmlNode n in Pir.ChildNodes)
            {
                InsertVal(n,Tra,ref List,ref temp);
            } 
        }
        static void CountComm(XmlNode Tra, string id,ref int val)
        {
            if (!Tra.Name.Equals("Przelewy"))
            {
                if (Tra.Name.Equals("Przelew") && Tra.Attributes["id"].Value == id)
                {
                    foreach(XmlNode n in Tra.ChildNodes)
                    {
                        val += int.Parse(n.Attributes["kwota"].Value);
                    }
                }
            }
            foreach (XmlNode n in Tra.ChildNodes)
            {
                CountComm(n,id,ref val);
            }
        }

        static void Count(XmlNode node,ref int value)
        {
            if (!node.Name.Equals("Piramida"))
            {
                value++;
            }
            foreach (XmlNode n in node.ChildNodes)
            {
                Count(n,ref value);
            }
        }
    }
}
