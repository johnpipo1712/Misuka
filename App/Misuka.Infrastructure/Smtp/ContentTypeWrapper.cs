using System;
using System.Net.Mime;

namespace Misuka.Infrastructure.Smtp
{
    [Serializable]
    internal class ContentTypeWrapper
    {
        String _boundary;
        String _charSet;
        String _mediaType;
        String _name;
        CollectionWrapper _parameters;

        internal static ContentTypeWrapper GetSerializeableContentType(ContentType ct)
        {
            if (ct == null)
                return null;

            ContentTypeWrapper sct = new ContentTypeWrapper();

            sct._boundary = ct.Boundary;
            sct._charSet = ct.CharSet;
            sct._mediaType = ct.MediaType;
            sct._name = ct.Name;
            sct._parameters = CollectionWrapper.GetSerializeableCollection(ct.Parameters);

            return sct;
        }

        internal ContentType GetContentType()
        {

            ContentType sct = new ContentType();

            sct.Boundary = _boundary;
            sct.CharSet = _charSet;
            sct.MediaType = _mediaType;
            sct.Name = _name;

            _parameters.SetColletion(sct.Parameters);

            return sct;
        }
    }
}