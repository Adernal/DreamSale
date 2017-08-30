using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Media;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Pictures")]
    public partial class PictureController : ApiControllerBase
    {
        #region Fields
        private readonly IPictureService _pictureService;
        #endregion

        #region Ctor
        public PictureController(IBaseService baseService, ILogger logger, IWebHelper webHelper, IPictureService pictureService)
            : base(baseService, logger, webHelper)
        {
            this._pictureService = pictureService;
        }
        #endregion

        #region Methods
        [Route("upload")]
        public HttpResponseMessage AsyncUpload(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                //we process it distinct ways based on a browser
                //find more info here http://stackoverflow.com/questions/4884920/mvc3-valums-ajax-file-upload
                Stream stream = null;
                var fileName = "";
                var contentType = "";
                if (string.IsNullOrEmpty(HttpContext.Current.Request["qqfile"]))
                {
                    // IE
                    var httpPostedFile = HttpContext.Current.Request.Files[0];
                    if (httpPostedFile == null)
                        throw new ArgumentException("No file uploaded");
                    stream = httpPostedFile.InputStream;
                    fileName = Path.GetFileName(httpPostedFile.FileName);
                    contentType = httpPostedFile.ContentType;
                }
                else
                {
                    //Webkit, Mozilla
                    stream = HttpContext.Current.Request.InputStream;
                    fileName = HttpContext.Current.Request["qqfile"];
                }

                var fileBinary = new byte[stream.Length];
                stream.Read(fileBinary, 0, fileBinary.Length);

                var fileExtension = Path.GetExtension(fileName);
                if (!String.IsNullOrEmpty(fileExtension))
                    fileExtension = fileExtension.ToLowerInvariant();
                //contentType is not always available 
                //that's why we manually update it here
                //http://www.sfsu.edu/training/mimetype.htm
                if (String.IsNullOrEmpty(contentType))
                {
                    switch (fileExtension)
                    {
                        case ".bmp":
                            contentType = MimeTypes.ImageBmp;
                            break;
                        case ".gif":
                            contentType = MimeTypes.ImageGif;
                            break;
                        case ".jpeg":
                        case ".jpg":
                        case ".jpe":
                        case ".jfif":
                        case ".pjpeg":
                        case ".pjp":
                            contentType = MimeTypes.ImageJpeg;
                            break;
                        case ".png":
                            contentType = MimeTypes.ImagePng;
                            break;
                        case ".tiff":
                        case ".tif":
                            contentType = MimeTypes.ImageTiff;
                            break;
                        default:
                            break;
                    }
                }

                var picture = _pictureService.InsertPicture(fileBinary, contentType, null);
                _baseService.Commit();
                //when returning JSON the mime-type must be set to text/plain
                //otherwise some browsers will pop-up a "Save As" dialog.
                response = request.CreateResponse(HttpStatusCode.Created, new
                {
                    success = true,
                    pictureId = picture.Id,
                    imageUrl = _pictureService.GetPictureUrl(picture, 100),
                    MimeType = MimeTypes.TextPlain
                });
                return response;

            });
        }
        #endregion
    }
}
