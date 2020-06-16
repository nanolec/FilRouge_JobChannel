using IHM_Mobile.Core.Models;
using IHM_Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHM_Mobile.ViewModels
{
    public class DetailOffreViewModel : Observable
    {
        public OffreM offre {get; set; }

        public void Refresh()
        {
            this.OnPropertyChanged("offre");
        }
    }
}
