using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Interfaces;
using LibrarySystem.Models;

namespace LibrarySystem.Interfaces
{
    public interface ISearchable
    {
        bool Matches(string searchTerm);
    }
}