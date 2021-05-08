using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Saber_Interactive_test.Model
{
    class ListRandom
    {
        #region Переменные
        public ListNode Head;
        public ListNode Tail;
        public int Count;
        #endregion

        #region ListRandom - Пустой конструктор
        /// <summary>
        /// Пустой конструктор
        /// </summary>
        internal ListRandom()
        {

        }
        #endregion

        #region ListRandom - Инициализация класса, с конструктором начальных значений
        /// <summary>
        /// Конструктор начальных значений
        /// </summary>
        /// <param name="_head">Голова</param>
        /// <param name="_tail">Хвост</param>
        /// <param name="_count">Количество элементов</param>
        internal ListRandom(ListNode _head, ListNode _tail, int _count)
        {
            Head = _head;
            Tail = _tail;
            Count = _count;
        }
        #endregion

        #region Serialize - Сериализация
        /// <summary>
        /// Сериализация
        /// </summary>
        /// <param name="s">Лучше FileStream</param>
        /// <returns>Возвращяем сгенерированные данные после сериализации</returns>
        public ObservableCollection<ListNode> Serialize(Stream s)
        {
            ObservableCollection<ListNode> _collection = new ObservableCollection<ListNode>();
            ListNode temp = new ListNode();
            temp = Head;

            //Заполняем коллекцию из списка
            do
            {
                _collection.Add(temp);
                temp = temp.Next;
            } while (temp != null);

            //Запись в файл данных
            using (StreamWriter w = new StreamWriter(s))
                foreach (ListNode n in _collection)
                    w.WriteLine(n.Data.ToString() + ";" + _collection.IndexOf(n.Random).ToString());

            return _collection;
        }
        #endregion

        #region Deserialize - Десериализация
        /// <summary>
        /// Десериализация
        /// </summary>
        /// <param name="s">Лучше FileStream</param>
        /// <returns>Возвращяем десериализированные данные</returns>
        public ObservableCollection<ListNode> Deserialize(Stream s)
        {
            ObservableCollection<ListNode> _collection = new ObservableCollection<ListNode>();
            ListNode temp = new ListNode();
            Count = 0;
            Head = temp;
            string line;

            //Считывание с файла и запись в коллекцию
            try
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!line.Equals(""))
                        {
                            Count++;
                            temp.Data = line;
                            ListNode next = new ListNode();
                            temp.Next = next;
                            _collection.Add(temp);
                            next.Previous = temp;
                            temp = next;
                        }
                    }
                }
                if(Count == 0)
                {
                    return null;
                }
                //Создание Tail
                Tail = temp.Previous;
                Tail.Next = null;

                //Десериализация Return
                foreach (ListNode n in _collection)
                {
                    n.Random = _collection[Convert.ToInt32(n.Data.Split(';')[1])];
                    n.Data = n.Data.Split(';')[0];
                }
                return _collection;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Неизвестная ошибка\n\n" + exp, "Ошибка!");
                return null;
            }
        }
        #endregion
    }
}
