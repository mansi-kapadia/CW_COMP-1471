using CW_COMP_1471.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public interface IPlayService
    {
        IEnumerable<Play> GetAllPlays();

        Play? GetById(int id);

        void Add(Play play);

        void Update(Play play);

        void DeletePlay(int id);

        Play GetPlayDetails(int PlayId);

    }
}
