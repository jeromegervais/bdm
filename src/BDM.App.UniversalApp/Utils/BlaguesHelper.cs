using BDM.App.UniversalApp.ViewModels;
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

        private Dictionary<Order, List<BlagueVM>> _blagues;

        private Dictionary<int, Dictionary<Order, List<BlagueVM>>> _blaguesByCategory;

        private List<int> _votedBlagues;

        private List<Category> _categories;

        public BlaguesHelper(IBlaguesService blaguesService)
        {
            _blaguesService = blaguesService;

            _blaguesByCategory = new Dictionary<int, Dictionary<Order, List<BlagueVM>>>();
            _votedBlagues = new List<int>();
        }

        public async Task LoadBlagues()
        {
            _blagues = CastToVM(await _blaguesService.GetBlagues());
            _blaguesByCategory.Clear();
        }

        public bool HasBeenLoaded => _blagues != null;

        public List<BlagueVM> GetBlagues(Order order)
        {
            List<BlagueVM> list;
            return _blagues.TryGetValue(order, out list) ? list : new List<BlagueVM>();
        }

        public async Task<List<Category>> GetCategories()
        {
            if (_categories == null || !_categories.Any())
            {
                try
                {
                    _categories = await _blaguesService.GetCategories();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
            return _categories;
        }

        public async Task<Dictionary<Order, List<BlagueVM>>> GetBlaguesForCategory(int categoryId)
        {
            Dictionary<Order, List<BlagueVM>> result;
            if (!_blaguesByCategory.TryGetValue(categoryId, out result))
            {
                try
                {
                    result = _blaguesByCategory[categoryId] = CastToVM(await _blaguesService.GetBlaguesForCategory(categoryId));
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
            return result ?? new Dictionary<Order, List<BlagueVM>>();
        }

        public async Task<bool> Vote(int blagueId, bool like)
        {
            if (!_votedBlagues.Contains(blagueId))
            {
                _votedBlagues.Add(blagueId);
                try
                {
                    await _blaguesService.Vote(blagueId, like);
                    return true;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
            return false;
        }

        public async Task<List<BlagueVM>> Search(string searchWord)
        {
            try
            {
                List<Blague> blagues = await _blaguesService.Search(searchWord);

                if (blagues != null && blagues.Any())
                {
                    return blagues.Select(b => new BlagueVM(b)).ToList();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return new List<BlagueVM>();
        }

        public Dictionary<Order, List<BlagueVM>> CastToVM(Dictionary<Order, List<Blague>> dico)
        {
            return dico.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(b => new BlagueVM(b)).ToList());
        }

        private void HandleException(Exception ex)
        {
            // TODO faire quelque chose des exceptions....
        }
    }
}
