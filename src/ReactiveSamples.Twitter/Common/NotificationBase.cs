using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReactiveSamples.Twitter.Common
{
    /// <summary>
    /// Classic base class for all classes that want to use <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class NotificationBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
