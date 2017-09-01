using System.Drawing.Imaging;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using DM.UBP.MultiTenancy;
using DM.UBP.Net.MimeTypes;
using DM.UBP.Storage;
using DM.UBP.Web.Helpers;
using DM.UBP.Web.MultiTenancy;

namespace DM.UBP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class TenantCustomizationController : UBPControllerBase
    {
        private readonly TenantManager _tenantManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ITenancyNameFinder _tenancyNameFinder;

        public TenantCustomizationController(
            IAppFolders appFolders,
            TenantManager tenantManager,
            IBinaryObjectManager binaryObjectManager,
            ITenancyNameFinder tenancyNameFinder)
        {
            _tenantManager = tenantManager;
            _binaryObjectManager = binaryObjectManager;
            _tenancyNameFinder = tenancyNameFinder;
        }

        [HttpPost]
        public async Task<JsonResult> UploadLogo()
        {
            try
            {
                if (Request.Files.Count <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                var file = Request.Files[0];

                if (file.ContentLength > 30720) //30KB
                {
                    throw new UserFriendlyException(L("File_SizeLimit_Error"));
                }

                var fileBytes = file.InputStream.GetAllBytes();

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new UserFriendlyException("File_Invalid_Type_Error");
                }

                var logoObject = new BinaryObject(AbpSession.GetTenantId(), fileBytes);
                await _binaryObjectManager.SaveAsync(logoObject);

                var tenant = await _tenantManager.GetByIdAsync(AbpSession.GetTenantId());
                tenant.LogoId = logoObject.Id;
                tenant.LogoFileType = file.ContentType;

                return Json(new AjaxResponse(new { id = logoObject.Id, fileType = tenant.LogoFileType }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploadCustomCss()
        {
            try
            {
                if (Request.Files.Count <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                var file = Request.Files[0];

                if (file.ContentLength > 1048576) //1MB
                {
                    throw new UserFriendlyException(L("File_SizeLimit_Error"));
                }

                var fileBytes = file.InputStream.GetAllBytes();

                var cssFileObject = new BinaryObject(AbpSession.GetTenantId(), fileBytes);
                await _binaryObjectManager.SaveAsync(cssFileObject);

                var tenant = await _tenantManager.GetByIdAsync(AbpSession.GetTenantId());
                tenant.CustomCssId = cssFileObject.Id;

                return Json(new AjaxResponse(new { id = cssFileObject.Id }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> GetLogo()
        {
            var tenancyName = _tenancyNameFinder.GetCurrentTenancyNameOrNull();
            Tenant tenant = null;

            if (!string.IsNullOrEmpty(tenancyName))
            {
                using (CurrentUnitOfWork.SetTenantId(null))
                {
                    tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
                }
            }
            else if (AbpSession.TenantId.HasValue)
            {
                tenant = await _tenantManager.GetByIdAsync(AbpSession.TenantId.Value);
            }

            if (tenant == null || !tenant.HasLogo())
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                var logoObject = await _binaryObjectManager.GetOrNullAsync(tenant.LogoId.Value);
                if (logoObject == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                return File(logoObject.Bytes, tenant.LogoFileType);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> GetCustomCss()
        {
            var tenancyName = _tenancyNameFinder.GetCurrentTenancyNameOrNull();
            Tenant tenant = null;

            if (!string.IsNullOrEmpty(tenancyName))
            {
                using (CurrentUnitOfWork.SetTenantId(null))
                {
                    tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
                }
            }
            else if (AbpSession.TenantId.HasValue)
            {
                tenant = await _tenantManager.GetByIdAsync(AbpSession.TenantId.Value);
            }

            if (tenant == null || !tenant.CustomCssId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                var cssFileObject = await _binaryObjectManager.GetOrNullAsync(tenant.CustomCssId.Value);
                if (cssFileObject == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                return File(cssFileObject.Bytes, MimeTypeNames.TextCss);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> GetTenantLogo(int? tenantId)
        {
            var defaultLogo = "/Common/Images/logo.png";

            if (!tenantId.HasValue)
            {
                return File(defaultLogo, MimeTypeNames.ImagePng);
            }

            var tenant = await _tenantManager.GetByIdAsync(tenantId.Value);
            if (!tenant.HasLogo())
            {
                return File(defaultLogo, MimeTypeNames.ImagePng);
            }

            using (CurrentUnitOfWork.SetTenantId(tenantId.Value))
            {
                var logoObject = await _binaryObjectManager.GetOrNullAsync(tenant.LogoId.Value);
                if (logoObject == null)
                {
                    return File(defaultLogo, MimeTypeNames.ImagePng);
                }

                return File(logoObject.Bytes, tenant.LogoFileType);
            }
        }
    }
}