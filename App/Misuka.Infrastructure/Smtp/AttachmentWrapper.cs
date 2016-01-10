using System;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Misuka.Infrastructure.Smtp
{
    [Serializable]
    internal class AttachmentWrapper
    {
        String _contentId;
        ContentDispositionWrapper _contentDisposition;
        ContentTypeWrapper _contentType;
        Stream _contentStream;
        System.Net.Mime.TransferEncoding _transferEncoding;
        String _name;
        Encoding _nameEncoding;

        internal static AttachmentWrapper GetSerializeableAttachment(Attachment att)
        {
            if (att == null)
                return null;

            AttachmentWrapper saa = new AttachmentWrapper();
            saa._contentId = att.ContentId;
            if (att.ContentStream != null)
            {
                byte[] bytes = new byte[att.ContentStream.Length];
                att.ContentStream.Read(bytes, 0, bytes.Length);

                saa._contentStream = new MemoryStream(bytes);
            }
            saa._name = att.Name;

            //Temporarily remove below codes because having issue when sending email with attachment at weekend (if server is Norway culture)
            //saa._contentDisposition = ContentDispositionWrapper.GetSerializeableContentDisposition(att.ContentDisposition);
            //saa._contentType = ContentTypeWrapper.GetSerializeableContentType(att.ContentType);
            //saa._transferEncoding = att.TransferEncoding;
            //saa._nameEncoding = att.NameEncoding;
            return saa;
        }

        internal Attachment GetAttachment()
        {
            Attachment saa = new Attachment(_contentStream, _name);
            saa.ContentId = _contentId;
            saa.Name = _name;

            //Temporarily remove below codes because having issue when sending email with attachment at weekend (if server is Norway culture)

            //this._contentDisposition.SetContentDisposition(saa.ContentDisposition);
            //saa.ContentType = _contentType.GetContentType();
            //saa.TransferEncoding = _transferEncoding;
            //saa.NameEncoding = _nameEncoding;

            return saa;
        }
    }
}