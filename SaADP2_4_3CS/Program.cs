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

        private const int _m = 10;  //Размер массива
        private const int _n = 2 * _m; //Допустимое количество элементов хеш таблицы
        private static int _amount = 0;

        static void Main(string[] args)
        
        {
            string[] keys = { "Алексей", "Константин", "Евгений", "Ислам", "Данис", "Павел", "Егор", "Кирилл" };
            Array[] hashTable = new Array[_m];
            Interface(hashTable, keys);
        }

        public static int Hash(string key)
        {
            int code = 0;
            for (int i = 0; i < key.Length; i++)
            {
                code += (int)key[i];
            }
            return code % _m;
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
                _amount++;
                return;
            }
            compares++;
            if(hashTable[hash].Key == key) 
            {
                Console.WriteLine("Такой ключ уже есть.");
                return; 
            }
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
                    Node pCurrent = hashTable[hash].Begin;
                    while(pCurrent != null)
                    {
                        if (pCurrent.Key == key)
                        {
                            Console.WriteLine("Такой ключ уже есть.");
                            return;
                        }
                        pCurrent = pCurrent.Next;
                    }
                    hashTable[hash].End.Next = item;
                    hashTable[hash].End = item;
                }
                _amount++;
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
            if(hashTable[hash] == null)
            {
                return false;
            }
            Node pTemp;
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
                    pTemp = hashTable[hash].Begin;
                    hashTable[hash].Key = pTemp.Key;
                    hashTable[hash].Begin = pTemp.Next;
                    pTemp.Key = null;
                    pTemp.Next = null;
                }
                return true;
            }

            if (hashTable[hash].Begin != null) 
            {
                if (hashTable[hash].Begin.Key == key)
                {
                    pTemp = hashTable[hash].Begin;
                    hashTable[hash].Begin = hashTable[hash].Begin.Next;
                    pTemp.Key = null;
                    pTemp.Next = null;
                    pTemp = null;
                    return true;
                }
            }
            Node pCurrent = hashTable[hash].Begin;
            Node pPrev = pCurrent;
            while (pCurrent != null)
            {
                if (pCurrent.Key == key)
                {
                    pPrev.Next = pCurrent.Next;
                    pTemp = pCurrent;
                    pTemp.Key = null;
                    pTemp = null;
                    return true;
                }
                pPrev = pCurrent;
                pCurrent = pCurrent.Next;
            }
            return false;
        }

        public static void Print(Array[] hashTable)
        {
            Node pCurrent;
            if(_amount != 0)
            {
                for (int i = 0; i < _m; i++)
                {
                    if (hashTable[i] != null)
                    {
                        Console.Write($"\n {i}  - {hashTable[i].Key}");
                        pCurrent = hashTable[i].Begin;
                        while (pCurrent != null)
                        {
                            Console.Write($" , {pCurrent.Key}");
                            pCurrent = pCurrent.Next;
                        }
                        Console.WriteLine();
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
            if (_amount != _n)
            {
                int compares = 0;
                Console.Write("Введите ключ: "); string key = Console.ReadLine();
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
                Console.WriteLine($"Элемент был удален. Количество сравнений: {compares}");
            }
            else
            {
                Console.WriteLine($"Такого элемента нет. Количество сравнений: {compares}");
            }
        }

        public static void CaseSearch(Array[] hashTable)
        {
            if(!(_amount == 0))
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
            for (int i = 0; i < _m; i++)
            {
                if(hashTable[i] == null) { return; }
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
                Console.WriteLine("0. Меню.");
            }
        }
    }
}
