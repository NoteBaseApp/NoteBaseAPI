using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;
using NoteBaseLogicFactory;
using NoteBaseInterface;

namespace NoteBaseLogic.Tests
{
    [TestClass()]
    public class TagProcessorTests
    {
        /* private static Response<Tag> ExpectedRes1()
         {
             Response<Tag> result = new(true, "");
             result.AddItem(new Tag(1, "a"));

             return result;
         }

         private static DALResponse<TagDTO> DALRes1()
         {
             DALResponse<TagDTO> result = new(true, "");
             result.AddItem(new TagDTO(1, "a"));

             return result;
         }
         static IEnumerable<object[]> GetTest1
         {
             get
             {
                 return new[]
                 {
                 new object[]
                 {
                     ExpectedRes1(),
                     DALRes1(),
                     "JoeyJoeyRemmers@gmail.com"
                 }
             };
             }
         }

         [DataTestMethod()]
         [DynamicData(nameof(GetTest1))]
         public void Get_All_Test(Response<Tag> _expected, DALResponse<TagDTO> _Args, string _userMail)
         {
             //Arrange
             var serviceMock = new Mock<IDAL<TagDTO>>();
             serviceMock
             .Setup(m => m.Get(_userMail))//the expected method called
             .Returns(_Args)//If called as expected what result to return
             .Verifiable();//expected service behavior can be verified

             IProcessor<Tag> processor = ProcessorFactory.CreateTagProcessor(serviceMock.Object);

             //Act
             Response<Tag> actual = processor.Get(_userMail);

             //Assert
             for (int i = 0; i < _expected.Data.Count; i++)
             {
                 Assert.AreEqual(_expected.Data[i].ID, actual.Data[i].ID);
                 Assert.AreEqual(_expected.Data[i].Title, actual.Data[i].Title);
             }
         } */
    }
}