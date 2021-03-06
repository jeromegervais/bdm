﻿using BDM.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Data.Client.Contracts
{
    public interface IBlaguesService
    {
        Task<Dictionary<Order, List<Blague>>> GetBlagues();

        Task<List<Category>> GetCategories();

        Task<Dictionary<Order, List<Blague>>> GetBlaguesForCategory(int categoryId);

        Task<bool> Vote(int blagueId, bool like);

        Task<bool> Submit(string blague, string pseudo, string email);

        Task<List<Blague>> Search(string searchWord);
    }
}
