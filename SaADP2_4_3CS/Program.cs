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
        private const int n = 2 * m; //Допустимое количесвто элементов хеш таблицы
        private static int amount = 0;

        static void Main(string[] args)
        {
            string[] keys = { "Алексей", "Константин", "Евгений", "Ислам", "Данис", "Павел", "Егор", "Кирилл" };
            Array[] hashTable = new Array[m];
        }

        public static int Hash(string key)
        {
            int code = 0;
            for (int i = 0; i < key.Length; i++)
            {
                code += (int)key[i];
            }
            return code % m;
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

        public static void Print(Array[] hashTable)
        {
            Node pCurrent;
            if(amount != 0)
            {
                for (int i = 0; i < m; i++)
                {
                    if (hashTable[i] != null)
                    {
                        Console.Write($" | {i} - {hashTable[i].Key}");
                        pCurrent = hashTable[i].Begin;
                        while (pCurrent != null)
                        {
                            Console.Write($" | {i} - {pCurrent.Key}");
                            pCurrent = pCurrent.Next;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Таблица пуста.");
            }
            
        }

        public static void CreateHashTable(Array[] hashTable, string[] keys)
        {
            int hash;
            int compares = 0;
            int delta = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                hash = Hash(keys[i]);
                Add(hashTable, hash, keys[i], ref delta);
                compares += delta;
            }
            Console.WriteLine($"Количество сравнений при добавлении {keys.Length} элементов: {compares}");
        }
        public static int Input()
        {
            string strInput; bool stop = false;
            int number = -1;
            while (!stop)
            {
                try
                {
                    strInput = Console.ReadLine();
                    number = int.Parse(strInput); stop = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Не верный ввод.");
                    stop = false;
                    break;
                }
            }
            return number;
        }
        public static void CaseAdd(Array[] hashTable)
        {
            if (amount != n)
            {
                int compares = 0;
                Console.Write("введите ключ: "); string key = Console.ReadLine();
                int hash = Hash(key);
                Add(hashTable, hash, key, ref compares);
                Console.WriteLine($"Количество сравнений: {compares}");
            }
            else
            {
                Console.WriteLine("Таблица заполнена.");
            }
        }
        public static void CaseDelete(Array[] hashTable)
        {
            int compares = 0;
            Console.Write("Введите удаляемый ключ: "); string removeable = Console.ReadLine();
            int hash = Hash(removeable);
            bool check = Delete(hashTable, hash, removeable, ref compares);
            if (check)
            {
                Console.WriteLine($"Элемент был удален. КОличесвто сравнений: {compares}");
            }
            else
            {
                Console.WriteLine($"Такого элемента нет. Количество сравнений: {compares}");
            }
        }

        public static void CaseSearch(Array[] hashTable)
        {
            if(!(amount == 0))
            {
                int compares = 0;
                Console.Write("Введите ключ для поиска: "); string key = Console.ReadLine();
                int hash = Hash(key);
                int index = Search(hashTable, hash, key, ref compares);
                if (index == -1) { Console.WriteLine($"Такого ключа нет. Количество сравнений: {compares}"); }
                else
                    Console.WriteLine($"Ключ {key} в хеш таблице имеет место {index}. Количесвто сравнений: {compares}");
            }
            else
                Console.WriteLine("Хеш таблица пуста.");
        }

        public static void ClearMemory(Array[] hashTable, ref string[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i] = null;
            }
            keys = null;
            for (int i = 0; i < m; i++)
            {
                Node pCurrent = hashTable[i].Begin;
                Node pTemp;
                while(pCurrent != null)
                {
                    pTemp = pCurrent;
                    pCurrent = pCurrent.Next;
                    pTemp.Key = null;
                    pTemp.Next = null;
                }
                hashTable[i].Begin = null;
                hashTable[i].End = null;
                hashTable[i].Key = null;
            }
            hashTable = null;
        }
        public static void PrintMenu()
        {
            Console.WriteLine(
                 "1. Заполнить хеш таблицу.\n"
               + "2. Добавить ключ.\n"
               + "3. Удалить ключ.\n"
               + "4. Вывести хеш таблицу на экран.\n"
               + "5. Найти ключ в хеш таблице.\n"
               + "6. Завершить работу.");
        }

        public static void Interface(Array[] hashTable, string[] keys)
        {
            bool stop = false; PrintMenu();
            while(!stop)
            {
                switch (Input())
                {
                    case 0:
                        PrintMenu();
                        break;
                    case 1:
                        CreateHashTable(hashTable, keys);
                        break;
                    case 2:
                        CaseAdd(hashTable);
                        break;
                    case 3:
                        CaseDelete(hashTable);
                        break;
                    case 4:
                        Print(hashTable);
                        break;
                    case 5:
                        CaseSearch(hashTable);
                        break;
                    case 6:
                        ClearMemory(hashTable, ref keys);
                        stop = true;
                        break;

                    default:
                        Console.WriteLine("Такого пункта меню нет.");
                        break;
                }
            }
        }
    }
}
