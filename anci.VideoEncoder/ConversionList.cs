using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anci.VideoEncoder
{


    public class ConversionList : IEnumerable
    {
        public delegate void dListChanged(object sender);
        public event dListChanged ListChanged;

        public ObservableCollection<ConversionItem> Items = new ObservableCollection<ConversionItem>();

        public IEnumerable<ConversionItem> PendingOrFailed
        {
            get
            {
                return Items.Where(x => x.Success != true);
            }
        }

        public ConversionList()
        {
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ListChanged?.Invoke(this);
        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

       /* internal void Add(string source, string destination, string parameters)
        {
            Items.Add(new ConversionItem()
            {
                SourceFile = source,
                DestinationFile = destination,
                CommandLine = parameters
            });
        }*/

        internal void Add(ConversionItem conversionItem)
        {
            Items.Add(conversionItem);
        }

        internal void Clear()
        {
            Items.Clear();
        }

        internal void Updated()
        {
            ListChanged?.Invoke(this);
        }
    }
}
