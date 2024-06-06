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
using NoteBaseLogicInterface;
using NoteBaseLogicTests.TestDALs;

namespace NoteBaseLogic.Tests
{
    [TestClass()]
    public class NoteProcessorTests
    {
        [TestMethod()]
        public void CreateTest_Succeed()
        {
            //arrange
            INoteProcessor noteProcessor = Factory.CreateNoteProcessor();
            string title = "School";
            string text = "Ik zit op #Fontys in #Eindhoven";
            Guid categoryId = Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9");
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

            //act
            Note actual = noteProcessor.Create(title, text, categoryId, personId);

            //assert
            Note expected = new(Guid.Parse("555660fe-82c2-42ac-88e7-887102331de3"), "School", "Ik zit op #Fontys in #Eindhoven", Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9"), Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));
            expected.tagList.Add(new(Guid.Parse("74f05b9d-da24-42a1-8c09-2f3e9b014c93"), "fontys"));
            expected.tagList.Add(new(Guid.Parse("82c14bf7-53a4-4587-8eab-0aa59b0a48c9"), "eindhoven"));

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.PersonId, actual.PersonId);
        }

        [TestMethod()]
        public void CreateTest_Succeed_NoTags()
        {
            //arrange
            INoteProcessor noteProcessor = Factory.CreateNoteProcessor();
            string title = "Gaming";
            string text = "Ik ga zaterdag gamen";
            Guid categoryId = Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9");
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

            //act
            Note actual = noteProcessor.Create(title, text, categoryId, personId);

            //assert
            Note expected = new(Guid.Parse("f8486f1a-6fd4-47ed-b4b0-3804f7fadce1"), "Gaming", "Ik ga zaterdag gamen", Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9"), Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.PersonId, actual.PersonId);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), 
            "No valid category was given")]
        public void CreateTest_Failed_NoCategory()
        {
            //arrange
            INoteProcessor noteProcessor = Factory.CreateNoteProcessor();
            string title = "Huiswerk";
            string text = "Ik heb #programeer huiswerk";
            Guid categoryId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

            //act
            noteProcessor.Create(title, text, categoryId, personId);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "Title can't be empty")]
        public void CreateTest_Failed_NoTitle()
        {
            //arrange
            INoteProcessor noteProcessor = Factory.CreateNoteProcessor();
            string title = "";
            string text = "Ik heb #programeer huiswerk";
            Guid categoryId = Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9");
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

            //act
            noteProcessor.Create(title, text, categoryId, personId);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "Text can't be empty")]
        public void CreateTest_Failed_NoText()
        {
            //arrange
            INoteProcessor noteProcessor = Factory.CreateNoteProcessor();
            string title = "Huiswerk";
            string text = "";
            Guid categoryId = Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9");
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

            //act
            noteProcessor.Create(title, text, categoryId, personId);
        }

        /* [TestMethod()]
        public void AddTagsTest_Succeed()
        {
            
            //Arrage
            Note note = new(1, "test", "Dit is een #Test voor mijn #Tag selector en het toevoegen van een #Note", 1);

            List<Tag> expected = new() { new Tag(0, "test"), new Tag(1, "tag") , new Tag(2, "note") };
            //Act
            note.AddTags();

            //Assert
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Title, note.TagList[i].Title);
            }
            
        }*/
    }
}