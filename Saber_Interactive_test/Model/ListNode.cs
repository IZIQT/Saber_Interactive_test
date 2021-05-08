
using Saber_Interactive_test.ViewModel;
using System;

namespace Saber_Interactive_test.Model
{
    class ListNode
    {
        #region Переменные
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random; // произвольный элемент внутри списка
        public string Data { get; set; }
        #endregion

        #region GetRandomData - Генерируем случайные данные
        /// <summary>
        /// Генерируем случайные данные
        /// </summary>
        /// <returns>Вызврат случайных данных</returns>
        internal string GetRandomData()
        {
            string result="";
            int j = MainWindowViewModel.rand.Next(0, 50);
            for (int i = 0; i < j; i++)
            {
                result += (char)MainWindowViewModel.rand.Next(0x0061, 0x007A);
            }
            return result;
        }
        #endregion

        #region addNode - Добавление нового элемента
        /// <summary>
        /// Добавление нового элемента
        /// </summary>
        /// <param name="prev">Нужен tail</param>
        /// <returns>Вызвращяем хвост</returns>
        internal ListNode addNode(ListNode prev)
        {
            ListNode result = new ListNode();
            result.Previous = prev;
            result.Next = null;
            result.Data = GetRandomData();
            prev.Next = result;
            return result;
        }
        #endregion

        #region randomNode - Создаем случаеные привязки для Random
        /// <summary>
        /// Создаем случаеные привязки для Random
        /// </summary>
        /// <param name="_head"></param>
        /// <param name="_tail"></param>
        /// <param name="_length"></param>
        /// <returns></returns>
        internal ListNode randomNode(ListNode _head, ListNode _tail, int _length)
        {
            //преобразование из двусвязного списка в циклический двусвязый,
            //что бы обойтись без if
            ListNode result = _head;
            result.Previous = _tail;
            _tail.Next = result;
            _head.Previous = _tail;
            _tail.Next = _head;

            //привязываем каждому элементу случайный элемент в random
            for (int j = 0; j < _length; j++)
            {
                int k = MainWindowViewModel.rand.Next(0, _length);
                int i = 0;

                while (i < k)
                {
                    result = result.Next;
                    i++;
                }
                _head.Random = result;
                _head = _head.Next;
            }
            //обратное преобразование
            _head.Previous = null;
            _tail.Next = null;
            return _head;
        }
        #endregion
    }
}
