using System.Collections.Generic;
using DM.UBP.Auditing.Dto;
using DM.UBP.Dto;

namespace DM.UBP.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
