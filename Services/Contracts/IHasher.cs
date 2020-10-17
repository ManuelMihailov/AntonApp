using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface IHasher
    {
        string Hash(string password);

        bool Verify(string password, string hash);
    }
}