using bs.DataTable.Dtos;

namespace bs.DataTable.Interfaces
{
    public interface IPageRequest
    {
        Column[] Columns { get; set; }
        int Draw { get; set; }
        int Length { get; set; }
        int Start { get; set; }
    }
}