using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NoteBaseDALInterface.Models;
using NoteBaseDALInterface;
using NoteBaseLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteBaseDAL;
using NoteBaseLogicInterface.Models;
using NoteBaseInterface;
using NoteBaseLogicFactory;

namespace NoteBaseLogic.Tests
{
    [TestClass()]
    public class NoteProcessorTests
    {
        /* [TestMethod()]
        public void CreateTest()
        {
            //Arrange
            NoteDTO note = new(1, "test", "Dit is een #Test voor mijn #Tag selector en het toevoegen van een #Note", new(1, "TestCategory", 1));
            DALResponse<NoteDTO> NoteDALResponse = new(200, "");


            TagDTO testTag = new(1, "test");
            TagDTO tagTag = new(2, "tag");
            TagDTO NoteTag = new(3, "note");
            DALResponse<TagDTO> TagDALResponse = new(200, "");

            var tagDALMock = new Mock<TagDAL>();
            tagDALMock
            .Setup(m => m.Create(testTag))//the expected method called
            .Returns(TagDALResponse)//If called as expected what result to return
            .Verifiable();//expected service behavior can be verified

            tagDALMock
            .Setup(m => m.Create(tagTag))//the expected method called
            .Returns(TagDALResponse)//If called as expected what result to return
            .Verifiable();//expected service behavior can be verified

            var NoteDALMock = new Mock<NoteDAL>();
            NoteDALMock
            .Setup(m => m.Create(note))//the expected method called
            .Returns(NoteDALResponse)//If called as expected what result to return
            .Verifiable();//expected service behavior can be verified

            NoteDALMock
            .Setup(m => m.Create(note))//the expected method called
            .Returns(NoteDALResponse)//If called as expected what result to return
            .Verifiable();//expected service behavior can be verified

            //Act


            //Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTagsTest()
        {
            
            //Arrage
            Note note = new(1, "test", "Dit is een #Test voor mijn #Tag selector en het toevoegen van een #Note", new(1, "TestCategory"));
            NoteProcessor processor = ProcessorFactory.CreateNoteProcessor("");

            List<Tag> expected = new() { new Tag(0, "test"), new Tag(1, "tag") , new Tag(2, "note") };
            //Act
            Note actual = processor.AddTags(note);

            //Assert
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Title, actual.TagList[i].Title);
            }
            
        } */
    }
}