using GApp.Entities;
using System.Collections.Generic;

namespace TestData
{
    public interface IEntityTestData<T> where T : BaseEntity
    {
        List<T> Get_TestData();
    }
}