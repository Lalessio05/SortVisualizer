using SaleWPF;
using SaleWPF.FrameWork;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
namespace SortVisualizer
{


    public class MainViewModel : BaseNotification, INotifyPropertyChanged
    {
        public ICommand SortCommand { get; set; }
        public MainViewModel()
        {
            // Inizializza la collezione di oggetti con i dati che devono essere visualizzati
            Items = new ObservableCollection<MyDataObject>();
            for (int i = 0; i < 20; i++) { 
                Items.Add(new MyDataObject((i+1)*10,"Black"));
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
                    if (Items[j + 1].Altezza > Items[j].Altezza)
                    {
                        await Task.Delay(40);
                        Items[j].Colore = "Green";
                        var temp = Items[j];
                        Items[j] = Items[j + 1];
                        Items[j + 1] = temp;
                        flag = true;
                        Items = Items;
                        Items[j].Colore = "Black";
                    }
                }
            }
        }







        public class MyDataObject
        {
            public int Altezza { get; set; }
            public string Colore { get; set; }
            public MyDataObject(int altezza, string colore) {
                Altezza = altezza;
                Colore = colore;
            }   
        }
    }
}