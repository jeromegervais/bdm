using BDM.Common.Model;
using BDM.Data.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Utils
{
    public sealed class BlaguesHelper
    {
        private IBlaguesService _blaguesService;

        private Dictionary<Order, List<Blague>> _blagues;

        private List<Category> _categories;

        public BlaguesHelper(IBlaguesService blaguesService)
        {
            _blaguesService = blaguesService;
        }

        public async Task LoadBlagues()
        {
            _blagues = await _blaguesService.GetBlagues();
        }

        public bool HasBeenLoaded => _blagues != null;

        public List<Blague> GetBlagues(Order order)
        {
            List<Blague> list;
            return _blagues.TryGetValue(order, out list) ? list : new List<Blague>();
        }

        public async Task<List<Category>> GetCategories()
        {
            if (_categories == null || !_categories.Any())
            {
                _categories = await _blaguesService.GetCategories();
            }
            return _categories;
        }
    }
}
