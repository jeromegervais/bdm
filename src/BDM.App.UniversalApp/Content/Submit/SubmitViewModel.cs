using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.Data.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Content.Submit
{
    public class SubmitViewModel : BasePageViewModel
    {
        private IBlaguesService _blaguesService;

        public SubmitViewModel(IBlaguesService blaguesService)
        {
            _blaguesService = blaguesService;
        }

        public string Email { get; set; }
        public string Pseudo { get; set; }
        public string Blague { get; set; }

        public async Task<bool> Submit()
        {
            //_blaguesService.
            return await Task.FromResult(true);
        }
    }
}
