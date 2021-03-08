using ServiceRegister;
using System;
using TestServiceLibrary.Documents;

namespace TestServiceLibrary.GenericService
{
    [SingletonService]
    public class TestDocumentOpertation : IDocumentOpertation<TestDocumentTwo>
    {
        public TestDocumentOpertation()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; }

        public TestDocumentTwo Find(Guid id)
        {
            var obj = new TestDocumentTwo
            {
                Id = id
            };

            return obj;
        }
    }
}
