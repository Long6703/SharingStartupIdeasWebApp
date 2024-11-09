﻿using Microsoft.EntityFrameworkCore;
using SSI.Models;

namespace SSI.Services.IService
{
    public interface IAdminService
    {
        IEnumerable<Models.User> GetAllUsers();
        IEnumerable<Models.User> GetFounders();
        IEnumerable<Models.User> GetInvestors();
        Models.Category GetCategory(int id);
        Models.User GetUser(int id);
        void LockAccount(int id);
        void UnlockAccount(int id);

        int CountNoSuccesInvest(int investorId);

        int CountNoRejecrInvest(int investorId);
        decimal SumAmountInvest(int investorId);

    }
}
