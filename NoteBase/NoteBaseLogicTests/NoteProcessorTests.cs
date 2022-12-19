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
            Note note = new(0, "School", "Ik zit op #Fontys in #Eindhoven", 1);

            //act
            Note actual = noteProcessor.Create(note);

            //assert
            Note expected = new(20, "School", "Ik zit op #Fontys in #Eindhoven", 1);
            expected.TryAddTag(new(11, "fontys"));
            expected.TryAddTag(new(12, "eindhoven"));

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.PersonId, actual.PersonId);
        }

        [TestMethod()]
        public void CreateTest_Succeed_NoTags()
        {
            //arrange
            INoteProcessor noteProcessor = Factory.CreateNoteProcessor();
            Note note = new(0, "Gaming", "Ik ga zaterdag gamen", 1);

            //act
            Note actual = noteProcessor.Create(note);

            //assert
            Note expected = new(21, "Gaming", "Ik ga zaterdag gamen", 1);

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
            Note note = new(0, "Huiswerk", "Ik heb #programeer huiswerk", 0);

            //act
            noteProcessor.Create(note);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "Title can't be empty")]
        public void CreateTest_Failed_NoTitle()
        {
            //arrange
            INoteProcessor noteProcessor = Factory.CreateNoteProcessor();
            Note note = new(0, "", "Ik heb #programeer huiswerk", 1);

            //act
            noteProcessor.Create(note);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "Text can't be empty")]
        public void CreateTest_Failed_NoText()
        {
            //arrange
            INoteProcessor noteProcessor = Factory.CreateNoteProcessor();
            Note note = new(0, "Huiswerk", "", 1);

            //act
            noteProcessor.Create(note);
        }

        [TestMethod()]
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
            
        }
    }
}