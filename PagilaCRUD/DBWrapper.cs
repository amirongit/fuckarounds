using PagilaCRUD.DBModels;


namespace PagilaCRUD.DBWrapper
{
    public class DBAccessLayer
    {
        public static IEnumerable<Staff> GetStaffsPaginated(PaginationInfo pagination)
        {
            var context = new PagilaContext();
            return context.Staff.AsQueryable().Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
        }
    }

    public class PaginationInfo
    {
        public byte PageNumber { get; set; }
        public byte PageSize { get; set; }
    }
}
