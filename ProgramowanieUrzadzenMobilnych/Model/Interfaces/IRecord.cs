using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Interfaces
{
    public interface IRecord
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
    }
}
