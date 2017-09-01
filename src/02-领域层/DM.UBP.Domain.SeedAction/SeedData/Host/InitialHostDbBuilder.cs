using DM.UBP.EF;

namespace DM.UBP.Domain.SeedAction.SeedData.Host
{
    public class InitialHostDbBuilder
    {
        private readonly UbpDbContext _context;

        public InitialHostDbBuilder(UbpDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
