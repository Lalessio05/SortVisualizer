using SaleWPF;
using SaleWPF.FrameWork;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
namespace SortVisualizer
{


    public class MainViewModel : BaseNotification, INotifyPropertyChanged
    {
        public ICommand SortCommand { get; set; }
        public MainViewModel()
        {
            // Inizializza la collezione di oggetti con i dati che devono essere visualizzati
            Items = new ObservableCollection<MyDataObject>();
            int n = 380;
            for (int i = 0; i < n; i++)
            {
                Items.Add(new MyDataObject(new Random().Next(10, 300), "Black", 380/ n));
            }


            SortCommand = new RelayCommand(Sort);
 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private ObservableCollection<MyDataObject> _items;
        public ObservableCollection<MyDataObject> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public async void Sort()
        {
            bool flag = true;
            int count = 0;
            for (int i = 1; (i <= (Items.Count - 1)) && flag; i++)
            {
                flag = false;
                for (int j = 0; j < (Items.Count - 1); j++)
                {
                    count = count + 1;
                    Items[j].Colore = "Green"; 
                    Items[j + 1].Colore = "Red"; 
                    if (Items[j + 1].Altezza > Items[j].Altezza)
                    {
                        //await Task.Delay(1);
                        var temp = Items[j];
                        Items[j] = Items[j + 1];
                        Items[j + 1] = temp;
                        flag = true;
                    }
                    Items[j].Colore = "Black"; // entrambi gli elementi tornano neri dopo lo scambio
                    Items[j + 1].Colore = "Black";
                }
            }
#if DEBUG
            foreach (var item in Items) { item.Colore = "Green"; }
#endif

            Items[0].Colore = "Green"; // l'elemento più piccolo rimane verde
        }







        public class MyDataObject : BaseNotification
        {
            private string _colore;
            public int Altezza { get; set; }
            public string Colore
            {
                get { return _colore; }
                set
                {
                    if (_colore != value)
                    {
                        _colore = value;
                        OnPropertyChanged(nameof(Colore));
                    }
                }
            }
            public int AltezzaElemento { get; set; }

            public MyDataObject(int altezza, string colore, int altezzaElemento)
            {
                Altezza = altezza;
                Colore = colore;
                AltezzaElemento = altezzaElemento;
            }
        }
    }
}