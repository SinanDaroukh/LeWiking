using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiking
{
    public interface IDataManager
    {
        IEnumerable<Viking> LesVikings { get; }
        Viking AddV(Viking viking);
        Viking RemoveV(Viking viking);
        void UpdateV(Viking viking1, Viking viking2, ObservableCollection<Viking> LesVikings);
    }
}
