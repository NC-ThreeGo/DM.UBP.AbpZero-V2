using System.Collections.Generic;
using DM.UBP.Authorization.Users.Dto;
using DM.UBP.Dto;

namespace DM.UBP.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}