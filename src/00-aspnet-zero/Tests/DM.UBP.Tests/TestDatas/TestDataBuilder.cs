using EntityFramework.DynamicFilters;
using DM.UBP.EntityFramework;

namespace DM.UBP.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly UBPDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(UBPDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
