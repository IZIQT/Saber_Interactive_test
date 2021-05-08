using Saber_Interactive_test.Common.MVVM;
using Saber_Interactive_test.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Saber_Interactive_test.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        #region Переменные
        internal static Random rand = new Random();

        private string listText;
        public string ListText
        {
            get => listText;
            set
            {
                listText = value;
                OnPropertyChanged(nameof(ListText));
            }
        }

        private ListRandom doublyLinkedList;
        public ListRandom DoublyLinkedList
        {
            get => doublyLinkedList;
            set
            {
                doublyLinkedList = value;
                OnPropertyChanged(nameof(DoublyLinkedList));
            }
        }

        private ObservableCollection<ListNode> collectionListNode;
        public ObservableCollection<ListNode> CollectionListNode
        {
            get => collectionListNode;
            set
            {
                collectionListNode = value;
                OnPropertyChanged(nameof(CollectionListNode));
            }
        }

        public ICommand SerializeCommand { get; set; }
        public ICommand DeserializeCommand { get; set; }
        #endregion

        #region Конструктор
        internal MainWindowViewModel()
        {
            DoublyLinkedList = new ListRandom();
            SerializeCommand = new RelayCommand(SerializeCommandExecute);
            DeserializeCommand = new RelayCommand(DeserializeCommandExecute);
        }
        #endregion

        #region Процедуры

        private void SerializeCommandExecute(object obj)
        {
            int length = rand.Next(5, 25);

            //инициализация head и заполнение случайной датой
            ListNode head = new ListNode();
            head.Data = head.GetRandomData();
            //инициализация tail
            ListNode tail = head;

            //заполнение списка
            for (int i = 1; i < length; i++)
                tail = tail.addNode(tail);

            //привязываем случайные элементы
            head = head.randomNode(head, tail, length);

            doublyLinkedList = new ListRandom(head, tail, length);

            if (doublyLinkedList.Count != 0)
            {
                using (FileStream fs = new FileStream("dat.xml", FileMode.Create))
                {
                    CollectionListNode = doublyLinkedList.Serialize(fs);
                }
            }
            else
            {
                MessageBox.Show("Заполните коллекци перед сериализацией","Ошибка!");
            }
        }

        private void DeserializeCommandExecute(object obj)
        {
            try
            {
                if (File.Exists("dat.xml"))
                {
                    using (FileStream fs = new FileStream("dat.xml", FileMode.Open))
                    {
                        CollectionListNode = doublyLinkedList.Deserialize(fs);
                        if(CollectionListNode == null)
                        {
                            MessageBox.Show("Файл данных пуст\n\n", "Ошибка!");
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Неизвестная ошибка\n\n" + exp, "Ошибка!");
            }
        }
        #endregion

    }
}
