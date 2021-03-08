using ServiceRegister;
using System;
using TestServiceLibrary.Documents;

namespace TestServiceLibrary.GenericService
{
    [TransientService]
    public class DocumentOpertation<T> : IDocumentOpertation<T> where T : IDocument
    {
        public T Find(Guid id)
        {
            var obj = Activator.CreateInstance<T>();

            obj.Id = id;

            return obj;
        }
    }
}
