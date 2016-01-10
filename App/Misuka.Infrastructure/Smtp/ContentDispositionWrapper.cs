using System;
using System.Net.Mime;

namespace Misuka.Infrastructure.Smtp
{
    [Serializable]
    internal class ContentDispositionWrapper
    {
        DateTime _creationDate;
        String _dispositionType;
        String _fileName;
        Boolean _inline;
        DateTime _modificationDate;
        //Temporry remove this property because having culture issue in ContentDisposition when running in Net 4.0 (see details at http://connect.microsoft.com/VisualStudio/feedback/details/674627/culture-bug-in-contentdisposition)
        //CollectionWrapper _parameters; 
        DateTime _readDate;
        long _size;

        internal static ContentDispositionWrapper GetSerializeableContentDisposition(ContentDisposition cd)
        {
            if (cd == null)
                return null;

            ContentDispositionWrapper scd = new ContentDispositionWrapper();
            scd._creationDate = cd.CreationDate;
            scd._dispositionType = cd.DispositionType;
            scd._fileName = cd.FileName;
            scd._inline = cd.Inline;
            scd._modificationDate = cd.ModificationDate;
          //  scd._parameters = CollectionWrapper.GetSerializeableCollection(cd.Parameters);            
            scd._readDate = cd.ReadDate;
            scd._size = cd.Size;

            return scd;
        }

        internal void SetContentDisposition(ContentDisposition scd)
        {
            scd.CreationDate = _creationDate;
            scd.DispositionType = _dispositionType;
            scd.FileName = _fileName;
            scd.Inline = _inline;
            if (_modificationDate <= DateTime.MinValue)
                _modificationDate = _creationDate;
            scd.ModificationDate = _modificationDate;
            
            //_parameters.SetColletion(scd.Parameters);

            if (_readDate <= DateTime.MinValue)
                _readDate = _creationDate;
            scd.ReadDate = _readDate;
            scd.Size = _size;
        }
    }
}