using System;

namespace SaADP2_4_3CS
{
    public class Array
    {
        public string Key { get; set; }
        public Node Begin { get; set; }
        public Node End { get; set; }

    }

    public class Node
    {
        public string Key { get; set; }
        public Node Next { get; set; }
    }

    class Program
    {

        private const int m = 10;  //Размер массива
        private static int numbOfItems = 0;

        static void Main(string[] args)
        {
            string[] keys = { "Алексей", "Константин", "Евгений", "Ислам", "Данис", "Павел", "Егор", "Кирилл" };
            Array[] hashTable = new Array[m];
        }

        public static void Add(Array[] hashTable, int hash, string key, ref int compares)
        {
            compares++;
            if(hashTable[hash] == null)
            {
                Array cell = new Array();
                cell.Key = key;
                cell.Begin = cell.End = null;
                hashTable[hash] = cell;
            }
            compares++;
            if(hashTable[hash].Key == key) { return; }
            else
            {
                Node item = new Node();
                item.Key = key;
                item.Next = null;
                if(hashTable[hash].Begin == null)
                {
                    hashTable[hash].Begin = item;
                    hashTable[hash].End = item;
                }
                else
                {
                    hashTable[hash].Begin.Next = item;
                    hashTable[hash].End = item;
                }
            }
        }

        public static int Search(Array[] hashTable, int hash, string key, ref int compares)
        {
            compares++;
            if(hashTable[hash] == null) { return -1; }
            else
            {
                compares++;
                if (hashTable[hash].Key == key) { return hash; }
                else
                {
                    Node pCurrent = hashTable[hash].Begin;
                    while(pCurrent != null)
                    {
                        compares++;
                        if(pCurrent.Key == key) { return hash; }
                        pCurrent = pCurrent.Next;
                    }
                    return -1;
                }
            }
        }

        public static bool Delete(Array[] hashTable, int hash, string key, ref int compares)
        {
            compares++;
            if(hashTable[hash].Key == key)
            {
                compares++;
                if(hashTable[hash].Begin == null)
                {
                    hashTable[hash].Key = null;
                    hashTable[hash] = null;
                }
                else
                {
                    Node pTemp = hashTable[hash].Begin;
                    hashTable[hash].Key = pTemp.Key;
                    hashTable[hash].Begin = pTemp.Next;
                    pTemp.Key = null;
                    pTemp.Next = null;
                }
                return true;
            }
            else
            {
                Node pCurrent = hashTable[hash].Begin;
                Node pPrev = pCurrent;
                while (pCurrent != null)
                {
                    if(pCurrent.Key == key)
                    {
                        pPrev.Next = pCurrent.Next;
                        pCurrent.Key = null;
                        pCurrent.Next = null;
                        return true;
                    }
                    pPrev = pCurrent;
                    pCurrent = pCurrent.Next;
                }
                return false;
            }
        }

    }
}
